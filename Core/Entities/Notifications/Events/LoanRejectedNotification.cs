namespace Core.Entities.Notifications.Events
{
    public class LoanRejectedNotification
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string LoanReference { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
