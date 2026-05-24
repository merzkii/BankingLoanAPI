using MediatR;

namespace Application.Features.Loans.Commands.DeleteLoan
{
    public record DeleteLoanCommand(int LoanId) : IRequest<int>;
}
