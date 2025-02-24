using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.Providers;

public class NotificationEventFactory : INotificationEventFactory
{
    public NotificationEvent Create(Guid userId, Notification notification)
    {
        return new NotificationEvent
        {
            UserId = userId,
            Notification = notification,
            Ts = DateTimeOffset.Now
        };
    }
}