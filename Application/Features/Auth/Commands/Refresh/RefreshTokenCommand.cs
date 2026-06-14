using Application.DTO.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.Refresh
{
    public record RefreshTokenCommand: IRequest<LoginResponseDto>
    {
        public string RefreshToken { get; init; } = string.Empty;
    }
}
