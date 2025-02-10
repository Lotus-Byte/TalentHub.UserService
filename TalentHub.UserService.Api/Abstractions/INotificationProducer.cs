using TalentHub.UserService.Api.Models.Notification;

namespace TalentHub.UserService.Api.Abstractions;

public interface INotificationProducer
{
    Task SendAsync(NotificationMessageModel message);
}