using Application.DTO.Common;
using Core.Enums;

namespace Application.DTO.Loan
{
    public record LoanQueryParameters : PagedQueryParameters
    {
        public LoanStatus? Status { get; set; }
        public LoanType? LoanType { get; set; }
        public int? UserId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
