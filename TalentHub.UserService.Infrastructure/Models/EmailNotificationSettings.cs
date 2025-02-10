namespace TalentHub.UserService.Infrastructure.Models;

public class EmailNotificationSettings
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
    public string Address { get; set; }
    
    
    public UserNotificationSettings UserNotificationSettings { get; set; }
}