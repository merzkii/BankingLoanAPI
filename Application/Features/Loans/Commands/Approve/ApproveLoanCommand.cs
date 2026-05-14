using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Approve
{
    public record ApproveLoanCommand : IRequest<LoanResponseDto>
    {
        public int LoanId { get; init; }
    }
}
