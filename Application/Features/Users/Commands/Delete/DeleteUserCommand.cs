using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand : IRequest<Unit>
    {
        public int UserId { get; init; }
    }
   
}
