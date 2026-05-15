using Core.Enums;

namespace Core.Entities
{
    public class LoanStatusHistory
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan? Loan { get; set; }
        public LoanStatus FromStatus { get; set; }
        public LoanStatus ToStatus { get; set; }
        public string? Note { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedByAdminId { get; set; }
        public AdminUsers? ChangedBy { get; set; }
    }
}
