using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Api.Models.UserSettings;

public class UpdateUserSettingsModel
{
    [Required]
    public Guid UserId { get; set; }
    
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaEmail { get; set; }
}