using TalentHub.UserService.Api.Models.UserSettings;

namespace TalentHub.UserService.Api.Models.Notification;

public class NotificationMessageModel
{
    public Guid UserId { get; set; }
    public NotificationModel Notification { get; set; }
    public UserNotificationSettingsModel UserSettings { get; set; }
    public DateTime Ts { get; set; }
}