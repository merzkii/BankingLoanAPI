using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Reject
{
    public class RejectLoanCommand : IRequest<LoanResponseDto>
    {
        public int LoanId { get; set; }
        public string Reason { get; set; }
    }
    
    
}
