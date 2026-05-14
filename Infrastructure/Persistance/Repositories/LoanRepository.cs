using Application.DTO.Loan;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            try
            {
                await _context.Loans.AddAsync(loan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if(loan is null)
            {
                throw new KeyNotFoundException($"Loan with {id} not found");
            }
            _context.Loans.Remove(loan);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.AsNoTracking().ToListAsync();
        }

        public async Task<(List<Loan> Items, int TotalCount)> GetPagedAsync(LoanQueryParameters parameters)
        {
            var query = _context.Loans.AsNoTracking().AsQueryable();

            if(parameters.Status.HasValue)
            {
                query = query.Where(loan => loan.Status == parameters.Status.Value);
            }

            if(parameters.LoanType.HasValue)
            {
                query = query.Where(loan => loan.LoanType == parameters.LoanType.Value);
            }

            if (parameters.UserId.HasValue)
            {
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

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await _context.Loans.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.LoanId == id);
            if (loan == null)
            {
                throw new KeyNotFoundException($"Loan with ID {id} not found.");
            }
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
        {
            await _context.Loans.AsNoTracking().Include(x => x.User).ToListAsync();
            return await _context.Loans
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await Task.CompletedTask;
        }
    }

}
