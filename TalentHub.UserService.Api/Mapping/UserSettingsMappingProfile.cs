using AutoMapper;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Api.Mapping;

public class UserSettingsMappingProfile : Profile
{
    public UserSettingsMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettingsModel>();
        CreateMap<CreateUserSettingsModel, CreateUserSettingsDto>();
        CreateMap<UpdateUserSettingsModel, UpdateUserSettingsDto>();
    }
}