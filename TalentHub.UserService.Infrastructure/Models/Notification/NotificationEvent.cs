using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Infrastructure.Models.Notification;

public class NotificationEvent : IDomainEvent
{
    public Guid UserId { get; set; }
    public Notification Notification { get; set; }
    public UserNotificationSettings UserSettings { get; set; }
    public DateTime Ts { get; set; }
}