using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Application.Interfaces;

public interface IUserSettingsService
{
    Task<Guid> CreateUserSettingsAsync(CreateUserSettingsDto user);
    Task<UserSettingsDto?> GetUserSettingsByIdAsync(Guid userId);
    Task<bool> UpdateUserSettingsAsync(UpdateUserSettingsDto user);
    Task<bool> DeleteUserSettingsAsync(Guid userId);
}