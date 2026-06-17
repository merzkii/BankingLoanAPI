using Application.DTO.Repayment;
using MediatR;

namespace Application.Features.Repayments.Queries
{
    public record GetRepaymentsQuery(
        int loanId) : IRequest<LoanRepaymentSummaryDto>;
}
