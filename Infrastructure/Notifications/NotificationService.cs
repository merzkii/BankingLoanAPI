using Application.Notifications;
using Core.Entities.Notifications;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationProvider> _providers;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IEnumerable<INotificationProvider> providers, ILogger<NotificationService> logger)
        {
            _providers = providers;
            _logger = logger;
        }

        public async Task SendAsync(NotificationMessage message, CancellationToken cancellationToken)
        {
            var provider = _providers.FirstOrDefault(p => p.Supports(message.Channel));

            if (provider == null)
            {
                throw new InvalidOperationException($"No provider registered for channel {message.Channel}");
            }

            _logger.LogInformation(
                "Sending {Channel} notification to {To} via {Provider}",
                message.Channel, message.To, _providers.GetType().Name);

            await provider.SendAsync(message);
        }
    }
}
