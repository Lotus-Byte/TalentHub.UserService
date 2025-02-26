using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface INotificationEventFactory
{
    NotificationEvent Create(Guid userId, Notification notification);
}