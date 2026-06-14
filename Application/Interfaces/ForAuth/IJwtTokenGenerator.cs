using Core.Entities.Admins;
using Core.Entities.Users;

namespace Application.Interfaces.ForAuth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateToken(AdminUsers adminUser);
        string GenerateRefreshToken();
        string HashToken(string rawToken);
        
    }
}
