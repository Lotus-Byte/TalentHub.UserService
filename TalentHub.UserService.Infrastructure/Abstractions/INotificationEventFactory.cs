using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface INotificationEventFactory
{
    NotificationEvent Create(Guid userId, UserNotificationSettings userSettings, Notification notification);
}