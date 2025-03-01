using Microsoft.Extensions.DependencyInjection;
using TalentHub.UserService.Api.Configurations;

namespace TalentHub.UserService.Api.Extensions;

public static class OptionsRegistrarExtension
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.AddOptions<ApplicationConfiguration>()
            .BindConfiguration(nameof(ApplicationConfiguration));
        
        services.AddOptions<RabbitMqConfiguration>()
            .BindConfiguration(nameof(RabbitMqConfiguration));
        
        return services;
    }
}