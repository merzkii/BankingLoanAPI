using Core.Entities.Auth;

namespace Application.Interfaces.ForAuth
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task UpdateAsync(RefreshToken token);
        Task SaveChangesAsync();
    }
}
