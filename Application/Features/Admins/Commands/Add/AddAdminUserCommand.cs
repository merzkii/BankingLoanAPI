using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Commands.Add
{
    public record AddAdminUserCommand : IRequest<AdminUserResponseDTO>
    {
        public AdminUserRequestDTO AdminUserData { get; init; } = new();

    }
}
