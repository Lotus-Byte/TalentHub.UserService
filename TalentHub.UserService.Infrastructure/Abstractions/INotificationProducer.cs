using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface INotificationProducer
{
    Task SendAsync(NotificationEvent @event);
}