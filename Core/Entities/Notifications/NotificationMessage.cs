using Core.Enums;

namespace Core.Entities.Notifications
{
    public class NotificationMessage
    {
        public string To { get; set; } = string.Empty;
        public NotificationChannel Channel { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? TemplateName { get; set; }
        public Dictionary<string, string>? TemplateData { get; set; } = new();
        public string Locale { get; set; } = "en-US";
        public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;
    }
}
