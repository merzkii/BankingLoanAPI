using MediatR;

namespace Application.Features.Users.Commands.Block
{
    public record BlockUserCommand : IRequest<Unit>
    {
        public int UserId { get; init; }
        
    }
}
