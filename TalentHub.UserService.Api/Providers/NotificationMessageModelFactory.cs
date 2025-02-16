using TalentHub.UserService.Api.Abstractions;
using TalentHub.UserService.Api.Models.Notification;
using TalentHub.UserService.Api.Models.UserSettings;

namespace TalentHub.UserService.Api.Providers;

public class NotificationMessageModelFactory : INotificationMessageModelFactory
{
    public NotificationMessageModel Create(Guid userId, UserNotificationSettingsModel userSettings, NotificationModel notification)
    {
        return new NotificationMessageModel
        {
            UserId = userId,
            UserSettings = userSettings,
            Notification = notification
        };
    }
}