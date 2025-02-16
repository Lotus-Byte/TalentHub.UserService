using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Infrastructure.Providers;

public class NotificationEventFactory : INotificationEventFactory
{
    public NotificationEvent Create(Guid userId, UserNotificationSettings userSettings, Notification notification)
    {
        return new NotificationEvent
        {
            UserId = userId,
            UserSettings = userSettings,
            Notification = notification,
            Ts = DateTime.Now
        };
    }
}