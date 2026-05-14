using Application.DTO.User;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
        public async Task<List<User>> GetAllAsync() => await _context.Users.Include(x=>x.Loans).ToListAsync();

        public async Task<(List<User> Items, int TotalCount)> GetPagedAsync(UserQueryParameters parameters)
        {
            var query = _context.Users
                .AsNoTracking()
                .Include(user => user.Loans)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var search = parameters.Search.Trim();
                query = query.Where(user =>
                    user.FirstName.Contains(search) ||
                    user.LastName.Contains(search) ||
                    user.Username.Contains(search) ||
                    user.Email.Contains(search));
            }

            if (parameters.UserType.HasValue)
            {
                query = query.Where(user => user.UserType == parameters.UserType.Value);
            }

            if (parameters.IsBlocked.HasValue)
            {
                query = query.Where(user => user.IsBlocked == parameters.IsBlocked.Value);
            }

            query = ApplySorting(query, parameters.SortBy, parameters.SortDescending);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        private static IQueryable<User> ApplySorting(IQueryable<User> query, string? sortBy, bool descending)
        {
            var normalizedSortBy = sortBy?.Trim().ToLowerInvariant();

            return normalizedSortBy switch
            {
                "firstname" => descending ? query.OrderByDescending(user => user.FirstName) : query.OrderBy(user => user.FirstName),
                "lastname" => descending ? query.OrderByDescending(user => user.LastName) : query.OrderBy(user => user.LastName),
                "username" => descending ? query.OrderByDescending(user => user.Username) : query.OrderBy(user => user.Username),
                "email" => descending ? query.OrderByDescending(user => user.Email) : query.OrderBy(user => user.Email),
                "age" => descending ? query.OrderByDescending(user => user.Age) : query.OrderBy(user => user.Age),
                "monthlyincome" => descending ? query.OrderByDescending(user => user.MonthlyIncome) : query.OrderBy(user => user.MonthlyIncome),
                "usertype" => descending ? query.OrderByDescending(user => user.UserType) : query.OrderBy(user => user.UserType),
                "isblocked" => descending ? query.OrderByDescending(user => user.IsBlocked) : query.OrderBy(user => user.IsBlocked),
                _ => descending ? query.OrderByDescending(user => user.UserId) : query.OrderBy(user => user.UserId),
            };
        }


        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Loans).FirstOrDefaultAsync(u => u.UserId == id);
        } 
        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        public async Task UpdateAsync(User user) {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
