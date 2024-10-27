using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Interfaces;

public interface IUserSettingsRepository
{
    Task<UserSettings?> AddUserSettingsAsync(UserSettings? userSettings);
    Task<UserSettings?> GetUserSettingsByIdAsync(Guid userId);
    Task<bool> UpdateUserSettingsAsync(UserSettings? userSettings);
    Task<bool> DeleteUserSettingsAsync(Guid userId);
}