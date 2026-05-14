using Application.DTO.Auth;
using MediatR;

namespace Application.Features.Auth
{
   public record LoginUserCommand : IRequest<LoginResponseDto>
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
