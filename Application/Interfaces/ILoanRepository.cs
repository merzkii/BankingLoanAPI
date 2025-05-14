using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> GetLoanByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(int id);
        Task SaveChangesAsync();
    }
}
