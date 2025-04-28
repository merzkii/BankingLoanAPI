using Application.DTO.Auth;
using MediatR;

namespace Application.Features.Auth
{
   public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto LoginRequest { get; set; }

        public LoginUserCommand(LoginRequestDto loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }
}
