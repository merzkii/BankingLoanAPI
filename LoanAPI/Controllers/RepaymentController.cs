using Application.DTO.Repayment;
using Application.Features.Repayments.Commands.MakeRepayments;
using Application.Features.Repayments.Queries.GetRepayments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers
{
    [Route("api/loans")]
    [ApiController]
    [Authorize]
    public class RepaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RepaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{loanId}/repayments")]
        public async Task<IActionResult> MakeRepayment(
            int loanId,
            [FromBody] MakeRepaymentDto dto)
        {
            var result = await _mediator.Send(new MakeRepaymentCommand(
                loanId,
                dto.Amount,
                dto.Notes));

            return Ok(result);
        }

        [HttpGet("{loanId}/repayments")]
        public async Task<IActionResult> GetRepayments(int loanId)
        {
            var result = await _mediator.Send(new GetRepaymentsQuery(loanId));
            return Ok(result);
        }
    }
}
