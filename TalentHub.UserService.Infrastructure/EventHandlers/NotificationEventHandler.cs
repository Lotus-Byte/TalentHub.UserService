using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.EventHandlers;

public class NotificationEventHandler : IEventHandler<NotificationEvent>
{
    private readonly IBus _bus;
    private readonly ILogger<NotificationEventHandler> _logger;
    
    public NotificationEventHandler(IBus bus, ILogger<NotificationEventHandler> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task HandleAsync(NotificationEvent notificationEvent)
    {
        var json = JsonSerializer.Serialize(notificationEvent);
        
        await _bus.Publish(notificationEvent);
        
        _logger.LogInformation($"""
                                Notification event published: 
                                 {json}
                                """);
    }
}