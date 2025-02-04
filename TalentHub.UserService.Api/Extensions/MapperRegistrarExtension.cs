using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.UserService.Api.Mapping;

namespace TalentHub.UserService.Api.Extensions;

public static class MapperRegistrarExtension
{
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(
            new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<PersonMappingProfile>();
                    cfg.AddProfile<TalentHub.UserService.Application.Mapping.PersonMappingProfile>();

                    cfg.AddProfile<StaffMappingProfile>();
                    cfg.AddProfile<TalentHub.UserService.Application.Mapping.StaffMappingProfile>();

                    cfg.AddProfile<EmployerMappingProfile>();
                    cfg.AddProfile<TalentHub.UserService.Application.Mapping.EmployerMappingProfile>();

                    cfg.AddProfile<UserSettingsMappingProfile>();
                    cfg.AddProfile<TalentHub.UserService.Application.Mapping.UserSettingsMappingProfile>();
                })));
        
        return services;
    }
}