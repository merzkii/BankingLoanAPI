using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Repayment
{
    public record MakeRepaymentDto
    {
        public decimal Amount { get; init; }
        public string? Notes { get; init; }
    }
}
