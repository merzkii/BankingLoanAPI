using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Approve
{
    public record ApproveLoanCommand(int LoanId) : IRequest<LoanResponseDto>;
}
