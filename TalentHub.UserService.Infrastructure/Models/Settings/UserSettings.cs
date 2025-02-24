using System.Text.Json.Serialization;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class UserSettings
{
    public Guid UserSettingsId { get; set; }
    public Guid UserId { get; set; }
    public UserNotificationSettings NotificationSettings { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public bool Deleted { get; set; }
    
    [JsonIgnore]
    public User User { get; set; }
}