using Application.DTO.Loan;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> GetLoanByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<(List<Loan> Items, int TotalCount)> GetPagedAsync(LoanQueryParameters parameters);
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(int id);
        Task SaveChangesAsync();
    }
}
