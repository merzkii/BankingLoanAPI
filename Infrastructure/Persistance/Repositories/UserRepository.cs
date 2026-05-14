using Application.DTO.User;
using Application.Exceptions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly HashSet<string> AllowedSortColumns = new(StringComparer.OrdinalIgnoreCase)
        {
            "id",
            "firstname",
            "lastname",
            "username",
            "email",
            "age",
            "monthlyincome",
            "usertype",
            "isblocked"
        };
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(User user)
        {
            if (user is null)
            {
                throw new ValidationException("User data is required.");
            }

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to add user with username {Username}", user.Username);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            ValidateId(id, nameof(id));

            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync() => await _context.Users
            .AsNoTracking()
            .Include(x => x.Loans)
            .ToListAsync();

        public async Task<(List<User> Items, int TotalCount)> GetPagedAsync(UserQueryParameters parameters)
        {
            ValidatePagedParameters(parameters);

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

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Email is required.");
            }

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            ValidateId(id, nameof(id));

            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Loans)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ValidationException("Username is required.");
            }

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task UpdateAsync(User user)
        {
            if (user is null)
            {
                throw new ValidationException("User data is required.");
            }

            ValidateId(user.UserId, nameof(user.UserId));
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
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

        private static void ValidateId(int id, string parameterName)
        {
            if (id <= 0)
            {
                throw new ValidationException($"{parameterName} must be a positive integer.");
            }
        }

        private static void ValidatePagedParameters(UserQueryParameters parameters)
        {
            if (parameters is null)
            {
                throw new ValidationException("User query parameters are required.");
            }

            if (parameters.Search is { Length: > 100 })
            {
                throw new ValidationException("Search cannot exceed 100 characters.");
            }

            ValidateSortDirection(parameters.SortDirection);
            ValidateSortColumn(parameters.SortBy);
        }

        private static void ValidateSortDirection(string? sortDirection)
        {
            if (!string.IsNullOrWhiteSpace(sortDirection)
                && !string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidationException("Sort direction must be 'asc' or 'desc'.");
            }
        }

        private static void ValidateSortColumn(string? sortBy)
        {
            if (!string.IsNullOrWhiteSpace(sortBy) && !AllowedSortColumns.Contains(sortBy.Trim()))
            {
                throw new ValidationException($"Unsupported user sort column '{sortBy}'.");
            }
        }
    }
}

