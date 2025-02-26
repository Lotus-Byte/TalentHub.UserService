using AutoMapper;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Application.Mapping;

public class UserSettingsMappingProfile : Profile
{
    public UserSettingsMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettings>();
        
        CreateMap<UserSettings, UserSettingsDto>()
            .ForMember(d => d.Created, map => map.MapFrom(src => src.Created.DateTime))
            .ForMember(d => d.Updated, map => map.MapFrom(src => src.Updated.DateTime));
        
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
        
        CreateMap<UserNotificationSettingsDto, UserNotificationSettings>()
            .ForMember(dest => dest.Email, opt => 
                opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => 
                opt.MapFrom(src => src.Push));
        
        CreateMap<UserNotificationSettings, UserNotificationSettingsDto>()
            .ForMember(dest => dest.Email, opt => 
                opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => 
                opt.MapFrom(src => src.Push));

        CreateMap<EmailNotificationSettingsDto, EmailNotificationSettings>();
        CreateMap<EmailNotificationSettings, EmailNotificationSettingsDto>();
        
        CreateMap<PushNotificationSettingsDto, PushNotificationSettings>();
        CreateMap<PushNotificationSettings, PushNotificationSettingsDto>();
    }
}