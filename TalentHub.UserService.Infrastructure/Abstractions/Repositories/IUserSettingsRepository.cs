using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Infrastructure.Abstractions.Repositories;

public interface IUserSettingsRepository
{
    Task<UserSettings?> AddUserSettingsAsync(UserSettings? userSettings);
    Task<UserSettings?> GetUserSettingsByIdAsync(Guid userId);
    Task<bool> UpdateUserSettingsAsync(UserSettings? userSettings);
    Task<bool> DeleteUserSettingsAsync(Guid userId);
}