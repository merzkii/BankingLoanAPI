using Application.DTO.AdminUser;
using MediatR;

namespace Application.Features.Admins.Commands.Add
{
    public record AddAdminUserCommand : IRequest<AdminUserResponseDto>
    {
        public AdminUserRequestDto AdminUserData { get; init; } = new();

    }
}
