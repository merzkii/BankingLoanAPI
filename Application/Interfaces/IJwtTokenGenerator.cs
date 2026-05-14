using Core.Entities;

namespace Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateToken(AdminUsers adminUser);
    }
}
