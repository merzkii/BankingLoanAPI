using Application.DTO.Loan;
using Application.DTO.Repayment;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanResponseDto> CreateLoanAsync(int userId, LoanRequestDto loanRequest);
        Task<LoanResponseDto> ApproveLoanAsync(int loanId, CancellationToken cancellationToken);
        Task<LoanResponseDto> RejectLoanAsync(int loanId, string reason, CancellationToken cancellationToken);
        Task<RepaymentResponseDto> MakeRepaymentAsync(int loanId, MakeRepaymentDto dto);
        Task<LoanRepaymentSummaryDto> GetRepaymentsAsync(int loanId);
    }
}
