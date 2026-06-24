using Application.DTO.Repayment;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Repayments.Commands.MakeRepayments
{
    public class MakeRepaymentCommandHandler : IRequestHandler<MakeRepaymentCommand, RepaymentResponseDto>
    {
        private readonly ILoanService _loanService;
        public MakeRepaymentCommandHandler(ILoanService loanService)
        {
            _loanService = loanService;
        }
        public async Task<RepaymentResponseDto> Handle(MakeRepaymentCommand request, CancellationToken cancellationToken)
        {
           return await _loanService.MakeRepaymentAsync(request.LoanId, new MakeRepaymentDto
           {
               Amount = request.Amount,
               Notes = request.Notes
           });
        }
    }
}
