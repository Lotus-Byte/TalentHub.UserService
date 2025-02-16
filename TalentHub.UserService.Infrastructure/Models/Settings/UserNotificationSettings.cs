namespace TalentHub.UserService.Infrastructure.Models.Settings;

public class UserNotificationSettings
{
    public Guid Id { get; set; }
    public EmailNotificationSettings Email { get; set; }
    public PushNotificationSettings Push { get; set; }
    
    
    public UserSettings UserSettings { get; set; }
}