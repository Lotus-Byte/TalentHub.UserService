using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;

namespace TalentHub.UserService.Infrastructure.Models.Notification;

public class NotificationEvent : IDomainEvent
{
    public Guid UserId { get; set; }
    public Notification Notification { get; set; }
    public DateTimeOffset Ts { get; set; }
}