using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Repayment
{
    public record LoanRepaymentSummaryDto
    {
        public decimal TotalRepayment { get; init; }
        public decimal TotalPaid { get; init; }
        public decimal RemainingBalance { get; init; }
        public bool IsFullyPaid { get; init; }
        public List<RepaymentResponseDto> Repayments { get; init; } = new();
    }
}
