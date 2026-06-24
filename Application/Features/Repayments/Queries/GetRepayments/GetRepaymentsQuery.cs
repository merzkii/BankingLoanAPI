using Application.DTO.Repayment;
using MediatR;

namespace Application.Features.Repayments.Queries.GetRepayments
{
    public record GetRepaymentsQuery(
        int loanId) : IRequest<LoanRepaymentSummaryDto>;
}
