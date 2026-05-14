using Core.Enums;

namespace Application.DTO.AdminUser
{
    public record AdminUserRequestDTO
    {
        public string firstName { get; init; } = string.Empty;
        public string lastName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public AdminRoles Role { get; init; }
    }
}
