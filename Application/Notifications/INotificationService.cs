using Core.Entities.Notifications;

namespace Application.Notifications
{
    public interface INotificationService
    {
        Task SendAsync(NotificationMessage message);
    }
}
