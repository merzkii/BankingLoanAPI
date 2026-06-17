using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Repayment
{
    public record RepaymentResponseDto
    {
        public int Id { get; init; }
        public int LoanId { get; init; }
        public decimal Amount { get; init; }
        public DateTime PaidAt { get; init; }
        public string? Notes { get; init; }
        public decimal RemainingBalanceBefore { get; init; }
        public decimal RemainingBalanceAfter { get; init; }
    }
}
