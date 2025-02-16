using MassTransit;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.EventHandlers;

public class NotificationEventHandler : IEventHandler<NotificationEvent>
{
    private readonly IBus _bus;
    
    public NotificationEventHandler(IBus bus) => _bus = bus;

    public async Task HandleAsync(NotificationEvent notificationEvent) => await _bus.Publish(notificationEvent);
}