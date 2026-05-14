using Application.DTO.Auth;
using Core.Enums;
using MediatR;

namespace Application.Features.Admins.Commands.Create
{
    public record CreateAdminCommand : IRequest<LoginResponseDto>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public AdminRoles Role { get; set; }
    }

}
