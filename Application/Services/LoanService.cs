using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public LoanService(
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
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

            var loan = new Loan
            {
                UserId = userId,
                Amount = request.Amount,
                Currency = request.Currency,
                Period = request.Period,
                LoanType = request.LoanType,
                Purpose = request.Purpose,
                Status = LoanStatus.InProcess,
                SubmittedAt = DateTime.UtcNow
            };

            loan.CalculateFinancials();

            if (user.MonthlyIncome <= 0)
                throw new BusinessRuleException("Monthly income must be greater than zero to apply for a loan.");

            var dti = loan.MonthlyPayment / user.MonthlyIncome;
            if (dti > LoanRules.MaxDebtToIncomeRatio)
                throw new BusinessRuleException(
                    $"Monthly payment ({loan.MonthlyPayment:C}) exceeds 40% of your monthly income ({user.MonthlyIncome:C}).");

            await _loanRepository.AddLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<LoanResponseDto> ApproveLoanAsync(int loanId)
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

        public async Task<LoanResponseDto> RejectLoanAsync(int loanId, string reason)
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
    }
}

