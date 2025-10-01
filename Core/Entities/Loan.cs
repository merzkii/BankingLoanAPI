using Core.Enums;

namespace Core.Entities
{
    public class Loan
    {

        public int LoanId { get; set; }
        public int UserId { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; }
        public LoanStatus Status { get; set; }
        //public User User { get; set; }
        public int? ApprovedById { get; set; }
        public AdminUsers? ApprovedBy { get; set; }
        public int? RejectedById { get; set; }
        public AdminUsers? RejectedBy { get; set; }
    }
}
