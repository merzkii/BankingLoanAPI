using Core.Entities.Notifications;
using Core.Enums;

namespace Application.Notifications
{
    public interface INotificationProvider
    {
        bool Supports(NotificationChannel channel);
        Task SendAsync(NotificationMessage message);
    }
}
