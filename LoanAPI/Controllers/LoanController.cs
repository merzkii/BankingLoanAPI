using Application.Features.Loans.Commands.Approve;
using Application.Features.Loans.Commands.CreateLoan;
using Application.Features.Loans.Commands.DeleteLoan;
using Application.Features.Loans.Commands.Reject;
using Application.Features.Loans.Commands.Update;
using Application.Features.Loans.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLoansQuery());
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLoanCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanCommand command)
        {
            if (id != command.LoanId)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLoanCommand { LoanId = id });
            return NoContent();
        }

        [HttpPost("approve/{loanId}")]
        [Authorize(Roles = "Accountant")]
        public async Task<IActionResult> ApproveLoan(int loanId)
        {
            var result = await _mediator.Send(new ApproveLoanCommand { LoanId = loanId });
            return Ok(result);
        }

        [HttpPost("reject/{loanId}")]
        [Authorize(Roles = "Accountant")]
        public async Task<IActionResult> RejectLoan(int loanId)
        {
            var result = await _mediator.Send(new RejectLoanCommand { LoanId = loanId });
            return Ok(result);
        }
    }
}
