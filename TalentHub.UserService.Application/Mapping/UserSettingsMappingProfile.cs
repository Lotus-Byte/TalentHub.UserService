using AutoMapper;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Mapping;

public class UserSettingsMappingProfile : Profile
{
    public UserSettingsMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettings>();
        
        CreateMap<CreateUserSettingsDto, UserSettings>()
            .ForMember(d => d.UserSettingsId, map => map.Ignore())
            .ForMember(d => d.UserId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
        
        CreateMap<UpdateUserSettingsDto, UserSettings>()
            .ForMember(d => d.UserSettingsId, map => map.Ignore())
            .ForMember(d => d.UserId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}