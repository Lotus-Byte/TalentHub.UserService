using MassTransit;
using TalentHub.UserService.Api.Abstractions;
using TalentHub.UserService.Api.Models.Notification;

namespace TalentHub.UserService.Api.Producers;

public class NotificationProducer : INotificationProducer
{
    private readonly IBus _bus;

    public NotificationProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task SendAsync(NotificationMessageModel message)
    {
        await _bus.Publish(message);
    }
}