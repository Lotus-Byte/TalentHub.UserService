using TalentHub.UserService.Api.Models.UserSettings;

namespace TalentHub.UserService.Api.Models.Notification;

public class NotificationMessageModel
{
    public Guid UserId { get; init; }
    public NotificationModel Notification { get; init; }
    public UserNotificationSettingsModel UserSettings { get; init; }
    public DateTime Ts { get; init; }
}