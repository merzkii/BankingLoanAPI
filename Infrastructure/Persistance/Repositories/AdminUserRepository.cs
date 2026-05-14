using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminUserRepository> _logger;

        public AdminUserRepository(AppDbContext context, ILogger<AdminUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAsync(AdminUsers adminUser)
        {
            if (adminUser is null)
            {
                throw new ValidationException("Admin user data is required.");
            }
            try
            {
                await _context.AdminUsers.AddAsync(adminUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to add admin user with username {Username}", adminUser.Username);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            ValidateId(id, nameof(id));

            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser is null)
            {
                throw new NotFoundException($"Admin user with id {id} not found.");
            }

            _context.AdminUsers.Remove(adminUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AdminUsers>> GetAllAsync()
        {
            return await _context.AdminUsers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AdminUsers?> GetByIdAsync(int id)
        {
            ValidateId(id, nameof(id));

            return await _context.AdminUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(adminUser => adminUser.Id == id);
        }

        public async Task<AdminUsers?> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ValidationException("Username is required.");
            }

            return await _context.AdminUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(userName => userName.Username == username);
        }

        public async Task UpdateAsync(AdminUsers adminUser)
        {
            if (adminUser is null)
            {
                throw new ValidationException("Admin user data is required.");
            }

            ValidateId(adminUser.Id, nameof(adminUser.Id));
            _context.AdminUsers.Update(adminUser);
            await _context.SaveChangesAsync();
        }

        private static void ValidateId(int id, string parameterName)
        {
            if (id <= 0)
            {
                throw new ValidationException($"{parameterName} must be a greater than zero.");
            }
        }
    }
}
