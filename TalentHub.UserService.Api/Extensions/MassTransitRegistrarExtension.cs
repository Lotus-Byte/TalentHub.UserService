using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TalentHub.UserService.Api.Configurations;
using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Api.Extensions;

public static class MassTransitRegistrarExtension
{
    public static IServiceCollection RegisterMassTransitProducer(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetService<IOptions<RabbitMqConfiguration>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(RabbitMqConfiguration)}' settings");

                var rabbitMqConfiguration = configuration.Value;
         
                cfg.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VirtualHost, h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });
        
                cfg.Message<NotificationEvent>(ct => 
                    ct.SetEntityName("notification_event"));
        
                cfg.Publish<NotificationEvent>(p =>
                {
                    p.ExchangeType = ExchangeType.Direct;
                });
        
                cfg.Send<NotificationEvent>(s => 
                    s.UseRoutingKeyFormatter(busCtx =>
                        rabbitMqConfiguration.QueueName));
            });
        });
        
        return services;
    }
}