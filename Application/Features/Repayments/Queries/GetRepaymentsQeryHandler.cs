using Application.DTO.Repayment;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Repayments.Queries
{
    public class GetRepaymentsQeryHandler : IRequestHandler<GetRepaymentsQuery, LoanRepaymentSummaryDto>
    {
        private readonly ILoanService _loanService;

        public GetRepaymentsQeryHandler(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<LoanRepaymentSummaryDto> Handle(GetRepaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _loanService.GetRepaymentsAsync(request.loanId);
        }
    }
}
