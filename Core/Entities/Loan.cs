using Core.Enums;

namespace Core.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public int Period { get; set; }
        public LoanStatus Status { get; set; }
        public User? User { get; set; }
        public int? ApprovedById { get; set; }
        public AdminUsers? ApprovedBy { get; set; }
        public int? RejectedById { get; set; }
        public AdminUsers? RejectedBy { get; set; }
        public string? Purpose { get; set; }
        public string? RejectionReason { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal TotalRepayment { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public List<LoanStatusHistory> StatusHistory { get; set; } = new List<LoanStatusHistory>();

        public void CalculateFinancials()
        {
            InterestRate = LoanRules.GetInterestRate(LoanType);
            decimal monthlyRate = InterestRate / 100 / 12;
            MonthlyPayment = monthlyRate == 0
                ? Amount / Period
                : Amount * monthlyRate / (1 - (decimal)Math.Pow((double)(1 + monthlyRate), -Period));
            TotalRepayment = MonthlyPayment * Period;
        }
    }
}
