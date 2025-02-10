namespace TalentHub.UserService.Api.Models.UserSettings;

public class UserNotificationSettingsModel
{
    public EmailNotificationSettingsModel Email { get; set; }
    public PushNotificationSettingsModel Push { get; set; }
}