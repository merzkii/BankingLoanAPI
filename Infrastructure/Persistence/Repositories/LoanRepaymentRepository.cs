using Application.Interfaces;
using Core.Entities.Loans;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class LoanRepaymentRepository : ILoanRepaymentRepository
    {
        private readonly AppDbContext _context;

        public LoanRepaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LoanRepayment repayment)
        {
            await _context.LoanRepayments.AddAsync(repayment);
        }

        public async Task<List<LoanRepayment>> GetByLoanIdAsync(int loanId)
        {
            return await _context.LoanRepayments
                .Where(r => r.LoanId == loanId)
                .OrderBy(r => r.PaidAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
