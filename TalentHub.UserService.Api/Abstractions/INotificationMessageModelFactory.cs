using TalentHub.UserService.Api.Models.Notification;
using TalentHub.UserService.Api.Models.UserSettings;

namespace TalentHub.UserService.Api.Abstractions;

public interface INotificationMessageModelFactory
{
    NotificationMessageModel Create(Guid userId, UserNotificationSettingsModel userSettings, NotificationModel notification);
}