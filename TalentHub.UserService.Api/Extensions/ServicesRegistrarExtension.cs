using Microsoft.Extensions.DependencyInjection;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.Services;

namespace TalentHub.UserService.Api.Extensions;

public static class ServicesRegistrarExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployerService, EmployerService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        
        return services;
    }
}