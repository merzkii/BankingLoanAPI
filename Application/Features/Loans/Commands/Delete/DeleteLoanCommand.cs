using MediatR;

namespace Application.Features.Loans.Commands.DeleteLoan
{
    public record DeleteLoanCommand : IRequest<int>
    {
        public int LoanId { get; init; }

    }
}
