using Application.DTO.Repayment;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Repayments.Queries.GetRepayments
{
    public class GetRepaymentsQueryHandler : IRequestHandler<GetRepaymentsQuery, LoanRepaymentSummaryDto>
    {
        private readonly ILoanService _loanService;

        public GetRepaymentsQueryHandler(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public async Task<LoanRepaymentSummaryDto> Handle(GetRepaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _loanService.GetRepaymentsAsync(request.loanId);
        }
    }
}
