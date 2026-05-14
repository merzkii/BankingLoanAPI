using Core.Enums;

namespace Application.DTO.AdminUser
{
    public record AdminUserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Surname { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public AdminRoles Role { get; init; }
    }
}
