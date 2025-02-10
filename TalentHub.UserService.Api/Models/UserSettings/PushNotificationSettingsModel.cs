namespace TalentHub.UserService.Api.Models.UserSettings;

public class PushNotificationSettingsModel
{
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
}