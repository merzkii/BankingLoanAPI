using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.CreateLoan
{
    public record CreateLoanCommand : IRequest<LoanResponseDto>
    {
        public LoanRequestDto LoanRequest { get; init; } = new();

        public CreateLoanCommand() { }

        public CreateLoanCommand(LoanRequestDto loanRequest)
        {
            LoanRequest = loanRequest;
        }
    }
}
