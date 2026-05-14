using Core.Enums;

namespace Core.Entities
{
    public class AdminUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public AdminRoles Role { get; set; }
    }
}
