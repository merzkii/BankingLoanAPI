using Application.DTO.User;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistance.Contexts;
using System.Data.Entity;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
        public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
         public async Task UpdateAsync(User user) => _context.Users.Update(user);
    }
    }
