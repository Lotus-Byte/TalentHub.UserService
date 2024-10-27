namespace TalentHub.UserService.Application.DTO.UserSettings;

public class CreateUserSettingsDto
{
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaEmail { get; set; }
}