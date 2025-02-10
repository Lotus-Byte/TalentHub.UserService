namespace TalentHub.UserService.Api.Models.UserSettings;

public class EmailNotificationSettingsModel
{
    public bool Enabled { get; set; }
    public string Address { get; set; }
}