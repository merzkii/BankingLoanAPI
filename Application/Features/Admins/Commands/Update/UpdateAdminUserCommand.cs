using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Commands.Update
{
    public record UpdateAdminUserCommand: IRequest<AdminUserResponseDTO>
    {
        public int AdminUserId { get; init; }
        public AdminUserRequestDTO AdminUserData { get; init; } = new();

        public UpdateAdminUserCommand() { }

        public UpdateAdminUserCommand(int adminUserId, AdminUserRequestDTO adminUserData)
        {
            AdminUserId = adminUserId;
            AdminUserData = adminUserData;
        }
    }
}
