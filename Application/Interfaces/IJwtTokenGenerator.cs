using Core.Entities.Admins;
using Core.Entities.Users;

namespace Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateToken(AdminUsers adminUser);
    }
}
