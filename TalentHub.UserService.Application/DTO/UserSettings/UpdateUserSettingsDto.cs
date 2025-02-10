using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Application.DTO.UserSettings;

public class UpdateUserSettingsDto
{
    [Required]
    public Guid UserId { get; set; }
    
    public UserNotificationSettingsDto UserSettings { get; init; }
}