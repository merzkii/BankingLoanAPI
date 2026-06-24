using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Commands.Update
{
    public record UpdateAdminUserCommand: IRequest<AdminUserResponseDto>
    {
        public int AdminUserId { get; init; }
        public AdminUserRequestDto AdminUserData { get; init; } = new();

        public UpdateAdminUserCommand() { }

        public UpdateAdminUserCommand(int adminUserId, AdminUserRequestDto adminUserData)
        {
            AdminUserId = adminUserId;
            AdminUserData = adminUserData;
        }
    }
}
