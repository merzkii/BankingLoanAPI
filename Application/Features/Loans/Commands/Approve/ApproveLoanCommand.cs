using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Approve
{
    public class ApproveLoanCommand:IRequest<LoanResponseDto>
    {
        public int LoanId { get; set; }
    }
    
}
