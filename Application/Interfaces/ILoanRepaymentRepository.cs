using Core.Entities.Loans;

namespace Application.Interfaces
{
    public interface ILoanRepaymentRepository
    {
        Task AddAsync(LoanRepayment repayment);
        Task<List<LoanRepayment>> GetByLoanIdAsync(int loanId);
        Task SaveChangesAsync();
    }
}
