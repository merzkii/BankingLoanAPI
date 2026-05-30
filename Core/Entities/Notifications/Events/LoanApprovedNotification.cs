namespace Core.Entities.Notifications.Events
{
    public class LoanApprovedNotification
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string LoanReference { get; set; } = string.Empty;
        public decimal ApprovedAmount { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int TermMonths { get; set; }
    }
}
