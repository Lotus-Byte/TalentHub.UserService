using AutoMapper;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Api.Mapping;

public class UserSettingsMappingProfile : Profile
{
    public UserSettingsMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettingsModel>();
        CreateMap<UserSettingsModel, UserSettingsDto>();
        
        CreateMap<CreateUserSettingsModel, CreateUserSettingsDto>();
        CreateMap<UpdateUserSettingsModel, UpdateUserSettingsDto>();
        
        CreateMap<UserNotificationSettingsModel, UserNotificationSettingsDto>()
            .ForMember(dest => dest.Email, opt => 
                opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => 
                opt.MapFrom(src => src.Push));
        
        CreateMap<UserNotificationSettingsDto, UserNotificationSettingsModel>()
            .ForMember(dest => dest.Email, opt => 
                opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => 
                opt.MapFrom(src => src.Push));

        CreateMap<EmailNotificationSettingsModel, EmailNotificationSettingsDto>();
        CreateMap<EmailNotificationSettingsDto, EmailNotificationSettingsModel>();
        
        CreateMap<PushNotificationSettingsModel, PushNotificationSettingsDto>();
        CreateMap<PushNotificationSettingsDto, PushNotificationSettingsModel>();
    }
}