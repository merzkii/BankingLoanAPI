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
        public UpdateLoanHandler(ILoanRepository loanRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<LoanResponseDto> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

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
            if(!canManageAllLoans && loan.Status != LoanStatus.InProcess)
            {
                throw new ForbiddenException("Only in-process loans can be updated.");
            }
            loan.Amount = request.LoanData.Amount;
            loan.Currency = request.LoanData.Currency;
            loan.Period = request.LoanData.Period;
            loan.LoanType = request.LoanData.LoanType;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);

        }
    }
    
}
