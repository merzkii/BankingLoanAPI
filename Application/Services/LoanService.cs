using Application.DTO.Loan;
using Application.DTO.Repayment;
using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using Core.Entities.Loans;
using Core.Enums;
using Core.Interfaces;
using MapsterMapper;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepaymentRepository _loanRepaymentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public LoanService(
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            ILoanRepaymentRepository loanRepaymentRepository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _loanRepaymentRepository = loanRepaymentRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }


        public async Task<LoanResponseDto> CreateLoanAsync(int userId, LoanRequestDto request)
        {
            if (!_currentUserService.IsUser)
                throw new UnauthorizedAccessException("Only regular users can request a loan.");

            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException("User not found.");

            if (user.IsBlocked)
                throw new UnauthorizedAccessException("You are not allowed to request a loan.");

            var limits = LoanRules.AmountLimits[user.UserType][request.LoanType];
            if (request.Amount < limits.Min || request.Amount > limits.Max)
                throw new BusinessRuleException(
                    $"Amount for {request.LoanType} as {user.UserType} must be between {limits.Min} and {limits.Max}.");

            var maxPeriod = LoanRules.MaxPeriodMonths[user.UserType][request.LoanType];
            if (request.Period > maxPeriod)
                throw new BusinessRuleException(
                    $"Max period for {request.LoanType} as {user.UserType} is {maxPeriod} months.");

            var loan = new Loan
            {
                UserId = userId,
                Amount = request.Amount,
                Currency = request.Currency,
                Period = request.Period,
                LoanType = request.LoanType,
                Purpose = request.Purpose,
                Status = LoanStatus.InProcess,
                SubmittedAt = DateTime.UtcNow,
                Reference = $"LN-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}"
            };

            loan.CalculateFinancials(user.UserType);

            if (user.MonthlyIncome <= 0)
                throw new BusinessRuleException("Monthly income must be greater than zero to apply for a loan.");

            var maxDti = LoanRules.MaxDebtToIncomeRatio[user.UserType];
            var dti = loan.MonthlyPayment / user.MonthlyIncome;
            if (dti > maxDti)
                throw new BusinessRuleException(
                    $"Monthly payment ({loan.MonthlyPayment:C}) exceeds 40% of your monthly income ({user.MonthlyIncome:C}).");

            await _loanRepository.AddLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<LoanResponseDto> ApproveLoanAsync(int loanId, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAdmin && !_currentUserService.IsAccountant)
                throw new UnauthorizedAccessException("Only admins or accountants can approve loans.");

            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
                ?? throw new NotFoundException("Loan not found.");

            if (loan.Status != LoanStatus.InProcess)
                throw new BusinessRuleException(
                    $"Cannot approve a loan with status '{loan.Status}'. Only InProcess loans can be approved.");

            loan.StatusHistory.Add(new LoanStatusHistory
            {
                FromStatus = loan.Status,
                ToStatus = LoanStatus.Approved,
                ChangedAt = DateTime.UtcNow,
                ChangedByAdminId = _currentUserService.UserId
            });

            loan.Status = LoanStatus.Approved;
            loan.ApprovedById = _currentUserService.UserId;
            loan.ApprovedAt = DateTime.UtcNow;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<LoanResponseDto> RejectLoanAsync(int loanId, string reason, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAdmin && !_currentUserService.IsAccountant)
                throw new UnauthorizedAccessException("Only admins or accountants can reject loans.");

            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
                ?? throw new NotFoundException("Loan not found.");

            if (loan.Status != LoanStatus.InProcess)
                throw new BusinessRuleException(
                    $"Cannot reject a loan with status '{loan.Status}'. Only InProcess loans can be rejected.");

            loan.StatusHistory.Add(new LoanStatusHistory
            {
                FromStatus = loan.Status,
                ToStatus = LoanStatus.Rejected,
                Note = reason,
                ChangedAt = DateTime.UtcNow,
                ChangedByAdminId = _currentUserService.UserId
            });

            loan.Status = LoanStatus.Rejected;
            loan.RejectedById = _currentUserService.UserId;
            loan.RejectedAt = DateTime.UtcNow;
            loan.RejectionReason = reason;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<RepaymentResponseDto> MakeRepaymentAsync(int loanId, MakeRepaymentDto dto)
        {
            if (!_currentUserService.IsAdmin && !_currentUserService.IsAccountant)
            {
                throw new UnauthorizedAccessException("Only admins or accountants can record payments.");
            }

            var loan = await _loanRepository.GetLoanByIdAsync(loanId);

            if (loan == null)
            {
                throw new NotFoundException("Loan not found.");
            }

            if (loan.Status != LoanStatus.Approved)
            {
                throw new BusinessRuleException("Payments can only be made on approved loans.");
            }

            if (dto.Amount <= 0)
            {
                throw new BusinessRuleException("Payment amount must be greater than zero.");
            }

            if (dto.Amount > loan.RemainingBalance)
            {
                throw new BusinessRuleException($"Payment amount {dto.Amount:C} exceeds remaining balance {loan.RemainingBalance:C}.");
            }

            var balanceBefore = loan.RemainingBalance;
            var balanceAfter = balanceBefore - dto.Amount;

            var repayment = new LoanRepayment
            {
                LoanId = loanId,
                Amount = dto.Amount,
                PaidAt = DateTime.UtcNow,
                Notes = dto.Notes ?? string.Empty,
                RemainingBalanceBefore = balanceBefore,
                RemainingBalanceAfter = balanceAfter
            };

            await _loanRepaymentRepository.AddAsync(repayment);

            if (balanceAfter <= 0)
            {
                loan.StatusHistory.Add(new LoanStatusHistory
                {
                    FromStatus = loan.Status,
                    ToStatus = LoanStatus.Completed,
                    ChangedAt = DateTime.UtcNow,
                    ChangedByAdminId = _currentUserService.UserId
                });
                loan.Status = LoanStatus.Completed;
                await _loanRepository.UpdateLoanAsync(loan);
            }

            await _loanRepaymentRepository.SaveChangesAsync();

            return _mapper.Map<RepaymentResponseDto>(repayment);
        }

        public async Task<LoanRepaymentSummaryDto> GetRepaymentsAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId);

            if (loan == null)
            {
                throw new NotFoundException("Loan not found.");
            }

            var repayments = await _loanRepaymentRepository.GetByLoanIdAsync(loanId);

            return new LoanRepaymentSummaryDto
            {
                TotalRepayment = loan.TotalRepayment,
                TotalPaid = loan.TotalPaid,
                RemainingBalance = loan.RemainingBalance,
                IsFullyPaid = loan.IsFullyPaid,
                Repayments = _mapper.Map<List<RepaymentResponseDto>>(repayments)
            };
        }
    }
}

