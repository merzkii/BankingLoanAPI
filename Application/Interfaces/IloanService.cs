using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    internal interface IloanService
    {
        Task<Loan> GetLoanByIdAsync(int loanId);        
        Task UpdateLoanAsync(int loanId, Loan loan); 
        Task DeleteLoanAsync(int loanId);                  
        Task BlockLoanAsync(int loanId, TimeSpan duration);
    }
}
