using System.Text.Json.Serialization;

namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class PushNotificationSettings
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
    
    [JsonIgnore]
    public UserNotificationSettings UserNotificationSettings { get; set; }
}