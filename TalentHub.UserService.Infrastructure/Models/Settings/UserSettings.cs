using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class UserSettings
{
    public Guid UserSettingsId { get; set; }
    public Guid UserId { get; set; }
    public UserNotificationSettings NotificationSettings { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Deleted { get; set; }
    
    
    public User User { get; set; }
}