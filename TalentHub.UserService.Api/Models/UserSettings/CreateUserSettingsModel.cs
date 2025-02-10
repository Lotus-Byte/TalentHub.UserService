namespace TalentHub.UserService.Api.Models.UserSettings;

public class CreateUserSettingsModel
{
    public Guid UserId { get; set; }
    public UserNotificationSettingsModel NotificationSettings { get; set; }
}