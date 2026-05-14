using Application.DTO.Loan;
using Application.Exceptions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private static readonly HashSet<string> AllowedSortColumns = new(StringComparer.OrdinalIgnoreCase)
        {
            "id",
            "amount",
            "currency",
            "period",
            "loantype",
            "status",
            "userid"

        };
        private readonly AppDbContext _context;
        private readonly ILogger<LoanRepository> _logger;

        public LoanRepository(AppDbContext context, ILogger<LoanRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            if (loan is null)
            {
                throw new ValidationException("Loan data is required.");
            }
            try
            {
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to add loan for user with ID {UserId}.", loan.UserId);
                throw;
            }
        }

        public async Task DeleteLoanAsync(int id)
        {
            ValidateId(id, nameof(id));

            var loan = await _context.Loans.FindAsync(id);
            if (loan is null)
            {
                throw new NotFoundException($"Loan with {id} not found");
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Loan> Items, int TotalCount)> GetPagedAsync(LoanQueryParameters parameters)
        {
            ValidatePagedParameters(parameters);

            var query = _context.Loans.AsNoTracking().AsQueryable();

            if (parameters.Status.HasValue)
            {
                query = query.Where(loan => loan.Status == parameters.Status.Value);
            }

            if (parameters.LoanType.HasValue)
            {
                query = query.Where(loan => loan.LoanType == parameters.LoanType.Value);
            }

            if (parameters.UserId.HasValue)
            {
                ValidateId(parameters.UserId.Value, nameof(parameters.UserId));
                query = query.Where(loan => loan.UserId == parameters.UserId.Value);
            }

            if (parameters.MinAmount.HasValue)
            {
                query = query.Where(loan => loan.Amount >= parameters.MinAmount.Value);
            }

            if (parameters.MaxAmount.HasValue)
            {
                query = query.Where(loan => loan.Amount <= parameters.MaxAmount.Value);
            }

            query = ApplySorting(query, parameters.SortBy, parameters.SortDescending);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            ValidateId(id, nameof(id));

            var loan = await _context.Loans
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.LoanId == id);

            if (loan == null)
            {
                throw new NotFoundException($"Loan with ID {id} not found.");
            }

            return loan;
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
        {
            ValidateId(userId, nameof(userId));

            return await _context.Loans
                .AsNoTracking()
                .Include(x => x.User)
      .Where(x => x.UserId == userId)
      .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateLoanAsync(Loan loan)
        {
            if (loan is null)
            {
                throw new ValidationException("Loan data is required.");
            }

            ValidateId(loan.LoanId, nameof(loan.LoanId));
            _context.Loans.Update(loan);
            return Task.CompletedTask;
        }

        private static IQueryable<Loan> ApplySorting(IQueryable<Loan> query, string? sortBy, bool descending)
        {
            var normalizedSortBy = sortBy?.Trim().ToLowerInvariant();

            return normalizedSortBy switch
            {
                "amount" => descending ? query.OrderByDescending(loan => loan.Amount) : query.OrderBy(loan => loan.Amount),
                "currency" => descending ? query.OrderByDescending(loan => loan.Currency) : query.OrderBy(loan => loan.Currency),
                "period" => descending ? query.OrderByDescending(loan => loan.Period) : query.OrderBy(loan => loan.Period),
                "loantype" => descending ? query.OrderByDescending(loan => loan.LoanType) : query.OrderBy(loan => loan.LoanType),
                "status" => descending ? query.OrderByDescending(loan => loan.Status) : query.OrderBy(loan => loan.Status),
                "userid" => descending ? query.OrderByDescending(loan => loan.UserId) : query.OrderBy(loan => loan.UserId),
                _ => descending ? query.OrderByDescending(loan => loan.LoanId) : query.OrderBy(loan => loan.LoanId),
            };
        }

        private static void ValidateId(int id, string parameterName)
        {
            if (id <= 0)
            {
                throw new ValidationException($"{parameterName} must be a positive integer.");
            }
        }

        private static void ValidatePagedParameters(LoanQueryParameters parameters)
        {
            if (parameters is null)
            {
                throw new ValidationException("Loan query parameters are required.");
            }

            if (parameters.MinAmount.HasValue && parameters.MinAmount.Value < 0)
            {
                throw new ValidationException("Minimum amount cannot be negative.");
            }

            if (parameters.MaxAmount.HasValue && parameters.MaxAmount.Value < 0)
            {
                throw new ValidationException("Maximum amount cannot be negative.");
            }

            if (parameters.MinAmount.HasValue && parameters.MaxAmount.HasValue && parameters.MinAmount.Value > parameters.MaxAmount.Value)
            {
                throw new ValidationException("Minimum amount cannot be greater than maximum amount.");
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
                throw new ValidationException($"Unsupported loan sort column '{sortBy}'.");
            }
        }
    }
}


