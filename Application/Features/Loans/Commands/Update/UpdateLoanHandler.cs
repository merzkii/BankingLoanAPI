using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Loans.Commands.Update
{
    public class UpdateLoanHandler : IRequestHandler<UpdateLoanCommand, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public UpdateLoanHandler(ILoanRepository loanRepository,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IUserRepository userRepository)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<LoanResponseDto> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);

            if (loan == null)
            {
                throw new NotFoundException("Loan not found");
            }

            var canManageAllLoans = _currentUserService.IsAdmin || _currentUserService.IsAccountant;
            var ownsLoan = loan.UserId == _currentUserService.UserId;

            if (!canManageAllLoans && !ownsLoan)
            {
                throw new ForbiddenException("You do not have permission to update this loan.");
            }

            if (!canManageAllLoans && loan.Status != LoanStatus.InProcess)
            {
                throw new ForbiddenException("Only in-process loans can be updated.");
            }

            if (canManageAllLoans && loan.Status != LoanStatus.InProcess)
            {
                throw new BusinessRuleException("Cannot update a loan that has already been approved or rejected.");
            }
              
            loan.Amount = request.LoanData.Amount;
            loan.Currency = request.LoanData.Currency;
            loan.Period = request.LoanData.Period;
            loan.LoanType = request.LoanData.LoanType;

            var user = await _userRepository.GetByIdAsync(loan.UserId);

            if (user == null) 
            { 
                throw new NotFoundException("User not found");
            }

            loan.CalculateFinancials(user.UserType);

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }
    }
}
