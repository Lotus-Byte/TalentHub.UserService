namespace TalentHub.UserService.Application.DTO.UserSettings;

public class UserNotificationSettingsDto
{
    public PushNotificationSettingsDto Push { get; set; }
    public EmailNotificationSettingsDto Email { get; set; }
}