using Application.DTO.Loan;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanResponseDto> ApproveLoanAsync(int loanId);
        Task<LoanResponseDto> RejectLoanAsync(int loanId);
    }
}
