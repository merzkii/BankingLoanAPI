using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Update
{
    public record UpdateLoanCommand : IRequest<LoanResponseDto>
    {
        public int LoanId { get; init; }
        public LoanRequestDto LoanData { get; set; } = new();

        public UpdateLoanCommand() { }

        public UpdateLoanCommand(int loanId, LoanRequestDto loanData)
        {
            LoanId = loanId;
            LoanData = loanData;
        }
    }
}
