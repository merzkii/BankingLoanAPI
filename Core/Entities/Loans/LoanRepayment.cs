namespace Core.Entities.Loans
{
    public class LoanRepayment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan? Loan { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string Notes { get; set; } = string.Empty;

        public decimal RemainingBalanceBefore { get; set; }
        public decimal RemainingBalanceAfter { get; set; }
    }
}
