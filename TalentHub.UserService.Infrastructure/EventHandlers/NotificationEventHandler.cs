using MassTransit;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;

namespace TalentHub.UserService.Infrastructure.EventHandlers;

public class NotificationEventHandler : IEventHandler<IDomainEvent>
{
    private readonly IBus _bus;
    
    public NotificationEventHandler(IBus bus) => _bus = bus;

    public async Task HandleAsync(IDomainEvent notificationEvent) => await _bus.Publish(notificationEvent);
}