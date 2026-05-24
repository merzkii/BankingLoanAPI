using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Reject
{
    public record RejectLoanCommand(int LoanId, string Reason) : IRequest<LoanResponseDto>;
}
