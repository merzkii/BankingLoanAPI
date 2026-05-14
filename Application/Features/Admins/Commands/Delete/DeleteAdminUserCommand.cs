using MediatR;

namespace Application.Features.Admins.Commands.Delete
{
    public record DeleteAdminUserCommand : IRequest<int>
    {
        public int AdminUserId { get; init; }
    }

}
