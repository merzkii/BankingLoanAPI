using Core.Entities.Notifications;
using Core.Entities.Notifications.Events;
using Core.Enums;

namespace Application.Notifications.Factories
{
    public static class LoanNotificationFactory
    {
        public static NotificationMessage ForApproval(LoanApprovedNotification data)
        {
            return new()
            {
                To = data.Email,
                Channel = NotificationChannel.Email,
                Subject = $"Your loan {data.LoanReference} has been approved!",
                TemplateName = "loan-approved",

                TemplateData = new()
                {
                    ["FullName"] = data.FullName,
                    ["Amount"] = data.ApprovedAmount.ToString("C"),
                    ["MonthlyPayment"] = data.MonthlyPayment.ToString("C"),
                    ["TermMonths"] = data.TermMonths.ToString("C"),
                    ["Reference"] = data.LoanReference,
                },
            };
        }

        public static NotificationMessage ForRejection(LoanRejectedNotification data)
        {
            return new()
            {
                To = data.Email,
                Channel = NotificationChannel.Email,
                Subject = $"Update on your loan application {data.LoanReference}",
                TemplateName = "loan-rejected",
                TemplateData = new()
                {
                    ["FullName"] = data.FullName,
                    ["Reference"] = data.LoanReference,
                    ["Reason"] = data.Reason ?? "Not specified"
                },
            };
        }
    }
}
