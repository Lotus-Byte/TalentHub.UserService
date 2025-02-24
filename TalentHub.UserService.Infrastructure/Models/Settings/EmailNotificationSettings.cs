using System.Text.Json.Serialization;

namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class EmailNotificationSettings
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
    public string Address { get; set; }
    
    [JsonIgnore]
    public UserNotificationSettings UserNotificationSettings { get; set; }
}