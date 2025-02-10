using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Api.Models.UserSettings;

public class UpdateUserSettingsModel
{
    [Required]
    public Guid UserId { get; set; }
    
    public UserNotificationSettingsModel NotificationSettings { get; set; }
}