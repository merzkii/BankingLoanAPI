using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Loan
{
    public class LoanResponseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
        public LoanType LoanType { get; set; }
        public LoanStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
