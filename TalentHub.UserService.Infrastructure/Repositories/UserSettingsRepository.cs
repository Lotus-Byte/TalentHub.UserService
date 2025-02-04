using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Repositories;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly UserDbContext _context;

    public UserSettingsRepository(UserDbContext context) => _context = context;

    // TODO: обсудить с командой связь с юзером по внешнему ключу 
    public async Task<UserSettings?> AddUserSettingsAsync(UserSettings? userSettings)
    {
        await _context.UserSettings.AddAsync(userSettings);
        await _context.SaveChangesAsync();
        
        return userSettings;
    }
    
    public async Task<UserSettings?> GetUserSettingsByIdAsync(Guid userId)
    {
        return await _context.UserSettings.FindAsync(userId);
    }

    public async Task<bool> UpdateUserSettingsAsync(UserSettings userSettings)
    {
        var existingSettings = await _context.UserSettings.FindAsync(userSettings.UserId);
        
        if (existingSettings == null) return false;
        
        var entry = _context.Entry(existingSettings);
        entry.CurrentValues.SetValues(userSettings);
        
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteUserSettingsAsync(Guid userId)
    {
        var existingSettings = await _context.UserSettings.FindAsync(userId);

        if (existingSettings == null) return false;

        existingSettings.Deleted = true;

        await _context.SaveChangesAsync();

        return true;
    }
}