using Application.Features.Admins.Commands.Create;
using Application.Features.Admins.Commands.Delete;
using Application.Features.Admins.Commands.Update;
using Application.Features.Admins.Queries.GetAll;
using Application.Features.Admins.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin(CreateAdminCommand request)
        {
            var result=await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddACountant(CreateAdminCommand request)
        {
            var result =await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]    
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAdminUserCommand { AdminUserId = id });
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateAdminUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAdminByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAdminsQuery());
            return Ok(result);
            
        }
    }
}
