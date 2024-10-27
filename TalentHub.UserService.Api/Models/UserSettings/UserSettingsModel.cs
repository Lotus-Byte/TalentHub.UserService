namespace TalentHub.UserService.Api.Models.UserSettings;

public class UserSettingsModel
{
    public Guid UserSettingsId { get; set; }
    public Guid UserId { get; set; }
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaEmail { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Deleted { get; set; }
}