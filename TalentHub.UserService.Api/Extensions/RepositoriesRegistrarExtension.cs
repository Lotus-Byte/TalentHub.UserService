using Microsoft.Extensions.DependencyInjection;
using TalentHub.UserService.Infrastructure.Abstractions.Repositories;
using TalentHub.UserService.Infrastructure.Repositories;

namespace TalentHub.UserService.Api.Extensions;

public static class RepositoriesRegistrarExtension
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployerRepository, EmployerRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
        
        return services;
    }
}