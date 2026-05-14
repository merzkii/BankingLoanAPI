using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Reject
{
    public record RejectLoanCommand : IRequest<LoanResponseDto>
    {
        public int LoanId { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}
