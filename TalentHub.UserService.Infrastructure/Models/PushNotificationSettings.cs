namespace TalentHub.UserService.Infrastructure.Models;

public class PushNotificationSettings
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
    
    
    public UserNotificationSettings UserNotificationSettings { get; set; }
}