using Application.DTO.Loan;
using Application.Exceptions;
using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Loans.Commands.Update
{
    public class UpdateLoanHandler : IRequestHandler<UpdateLoanCommand, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        public UpdateLoanHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            if (loan == null)
            {
                throw new NotFoundException("Loan not found");
            }
            loan.Amount = request.LoanData.Amount;
            loan.Currency = request.LoanData.Currency;
            loan.Period = request.LoanData.Period;
            loan.LoanType = request.LoanData.LoanType;

            await _loanRepository.UpdateLoanAsync(loan);
            return _mapper.Map<LoanResponseDto>(loan);

        }
    }
    
}
