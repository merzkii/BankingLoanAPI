using Application.Notifications;
using Core.Entities.Notifications;
using Core.Enums;
using Resend;
using System.Collections.Generic;

namespace Infrastructure.Notifications.Providers
{
    public class EmailNotificationProvider : INotificationProvider
    {
        private readonly IResend _resend;
        private readonly EmailProviderOptions _options;

        public EmailNotificationProvider(IResend resend, EmailProviderOptions options)
        {
            _resend = resend;
            _options = options;
        }

        public bool Supports(NotificationChannel channel) =>
            channel == NotificationChannel.Email;

        public async Task SendAsync(NotificationMessage message)
        {
            var html = BuildHtml(message);

            var email = new EmailMessage
            {
                From = _options.FromAddress,
                To = message.To,
                Subject = message.Subject,
                HtmlBody = html,
            };

            await _resend.EmailSendAsync(email);
        }

        private string BuildHtml(NotificationMessage message)
        {
            var templatePath = Path.Combine(
                AppContext.BaseDirectory,
                "Templates", $"{message.TemplateName}.html");

            var html = File.ReadAllText(templatePath);

            foreach (var (key, value) in message.TemplateData)
            {
                html = html.Replace($"{{{{{key}}}}}", value);
            }

            return html;
        }
    }
}
