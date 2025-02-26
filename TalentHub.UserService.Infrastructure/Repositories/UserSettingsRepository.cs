using Microsoft.EntityFrameworkCore;
using TalentHub.UserService.Infrastructure.Abstractions.Repositories;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Infrastructure.Repositories;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly UserDbContext _context;

    public UserSettingsRepository(UserDbContext context) => _context = context;

    // TODO: обсудить с командой связь с юзером по внешнему ключу 
    public async Task<UserSettings?> AddUserSettingsAsync(UserSettings? userSettings)
    {
        await _context.UserSettings.AddAsync(userSettings);
        
        return userSettings;
    }
    
    public async Task<UserSettings?> GetUserSettingsByUserIdAsync(Guid userId)
    {
        return await _context.UserSettings
            .Include(us => us.NotificationSettings)
            .ThenInclude(uns => uns.Email)
            .Include(us => us.NotificationSettings)
            .ThenInclude(uns => uns.Push)
            .SingleOrDefaultAsync(us => us.UserId == userId);
    }

    public async Task<bool> UpdateUserSettingsAsync(UserSettings userSettings)
    {
        var existingSettings = await _context.UserSettings.SingleOrDefaultAsync(c => 
            c.UserId == userSettings.UserId);
        
        if (existingSettings == null) return false;
        
        var entry = _context.Entry(existingSettings);
        entry.CurrentValues.SetValues(userSettings);
        
        return true;
    }

    public async Task<bool> DeleteUserSettingsAsync(Guid userId)
    {
        var existingSettings = await _context.UserSettings.FindAsync(userId);

        if (existingSettings == null) return false;

        existingSettings.Deleted = true;
        
        return true;
    }
}