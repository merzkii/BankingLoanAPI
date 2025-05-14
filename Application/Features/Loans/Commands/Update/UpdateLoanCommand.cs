using Application.DTO.Loan;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.Update
{
    public class UpdateLoanCommand : IRequest<LoanResponseDto>
    {
        public int LoanId { get; set; }
        public LoanRequestDto LoanData { get; set; }
        public UpdateLoanCommand(int loanId, LoanRequestDto loanData)
        {
            LoanId = loanId;
            LoanData = loanData;
        }
    }
  
}
