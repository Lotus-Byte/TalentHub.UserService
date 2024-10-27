namespace TalentHub.UserService.Api.Models.UserSettings;

public class CreateUserSettingsModel
{
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaEmail { get; set; }
}