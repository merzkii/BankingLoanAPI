using MediatR;

namespace Application.Features.Auth.Commands.Revoke
{
    public record RevokeTokenCommand: IRequest<Unit>
    {
        public string RefreshToken { get; init; } = string.Empty;
    }
}
