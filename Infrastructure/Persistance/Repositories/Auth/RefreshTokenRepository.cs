using Application.Interfaces.ForAuth;
using Core.Entities.Auth;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories.Auth
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
         => await _context.RefreshTokens.AddAsync(token);

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
            => await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);

        public async Task UpdateAsync(RefreshToken token)
            => _context.RefreshTokens.Update(token);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
