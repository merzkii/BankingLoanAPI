using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.DeleteLoan
{
    public class DeleteLoanCommand : IRequest<int>
    {
        public int LoanId { get; set; }
       
    }
    
}
