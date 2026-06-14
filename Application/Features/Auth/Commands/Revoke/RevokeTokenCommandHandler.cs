using Application.Interfaces.ForAuth;
using MediatR;

namespace Application.Features.Auth.Commands.Revoke
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly IAuthService _authService;

        public RevokeTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            await _authService.RevokeAsync(request.RefreshToken);
            return Unit.Value;
        }
    }
}
