using Application.DTO.User;
using Application.Features.Users.Commands.Block;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCommand { UserId = id });
            return NoContent();
        }

        [HttpPut("block/{id}")]
        [Authorize(Roles = "Accountant")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _mediator.Send(new BlockUserCommand { UserId = id });
            return NoContent();
        }

        
    }

}

