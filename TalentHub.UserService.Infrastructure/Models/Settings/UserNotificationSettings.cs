using System.Text.Json.Serialization;

namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class UserNotificationSettings
{
    public Guid Id { get; set; }
    public Guid UserSettingsId { get; set; }
    public EmailNotificationSettings Email { get; set; }
    public PushNotificationSettings Push { get; set; }
    
    [JsonIgnore]
    public UserSettings UserSettings { get; set; }
}