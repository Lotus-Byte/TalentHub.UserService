using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TalentHub.UserService.Api.Configurations;
using TalentHub.UserService.Infrastructure.Data;

namespace TalentHub.UserService.Api.Extensions;

public static class DbContextRegistrarExtension
{
    public static IServiceCollection RegisterDbContext(this IServiceCollection services)
    {
        services.AddDbContext<UserDbContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<ApplicationConfiguration>>();
            options.EnableSensitiveDataLogging();
            options.UseNpgsql(settings.Value.ConnectionString);
        });
        
        return services;
    }
}