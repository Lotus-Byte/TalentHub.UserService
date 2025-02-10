namespace TalentHub.UserService.Application.DTO.UserSettings;

public class EmailNotificationSettingsDto
{
    public bool Enabled { get; set; }
    public string Address { get; set; }
}