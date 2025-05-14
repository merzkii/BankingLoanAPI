﻿using Application.DTO.Auth;
using Application.Features.Auth;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var command = new LoginUserCommand
            {
                Username = loginRequest.Username,
                Password = loginRequest.Password

            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
