using AutoMapper;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Api.Mapping;

public class UserSettingsMappingProfile : Profile
{
    // TODO: SORT OUT WITH THE ORDER OF MODELS IN PROFILE
    public UserSettingsMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettingsModel>();
        CreateMap<CreateUserSettingsModel, CreateUserSettingsDto>();
        CreateMap<UpdateUserSettingsModel, UpdateUserSettingsDto>();
        
        CreateMap<UserNotificationSettingsModel, UserNotificationSettingsDto>()
            .ForMember(dest => dest.Email, opt => 
                opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => 
                opt.MapFrom(src => src.Push));

        CreateMap<EmailNotificationSettingsModel, EmailNotificationSettingsDto>();
        
        CreateMap<PushNotificationSettingsModel, PushNotificationSettingsDto>();
    }
}