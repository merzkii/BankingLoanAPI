using Application.DTO.Auth;
using MediatR;

namespace Application.Features.Auth
{
   public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
