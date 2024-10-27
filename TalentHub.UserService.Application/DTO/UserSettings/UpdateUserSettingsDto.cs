using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Application.DTO.UserSettings;

public class UpdateUserSettingsDto
{
    [Required]
    public Guid UserId { get; set; }
    
    public bool NotifyViaPush { get; set; }
    public bool NotifyViaEmail { get; set; }
}