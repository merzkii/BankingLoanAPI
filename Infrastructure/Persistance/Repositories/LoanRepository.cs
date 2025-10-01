using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Loans.Remove(loan);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
           await _context.Loans./*Include(x => x.User).*/ToListAsync();
            return await _context.Loans.ToListAsync();  
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await _context.Loans
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
          _context.Loans.Include(x => x.User).ToListAsync();
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
