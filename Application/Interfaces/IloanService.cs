using Application.DTO.Loan;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanResponseDto> CreateLoanAsync(int userId, LoanRequestDto loanRequest);
        Task<LoanResponseDto> ApproveLoanAsync(int loanId);
        Task<LoanResponseDto> RejectLoanAsync(int loanId, string reason);
    }
}
