namespace TalentHub.UserService.Application.DTO.UserSettings;

public class PushNotificationSettingsDto
{
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
}