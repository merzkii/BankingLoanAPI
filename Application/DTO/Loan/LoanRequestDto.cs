using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Loan
{
    public class LoanRequestDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } 
        public int Period { get; set; } 
        public LoanType LoanType { get; set; } 
    }
}
