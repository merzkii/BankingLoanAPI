using Application.DTO.Loan;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<LoanResponseDto>
    {
       public LoanRequestDto LoanRequest { get; set; }


        public CreateLoanCommand(LoanRequestDto loanRequest)
        {
            LoanRequest = loanRequest;
        }
    }
}
