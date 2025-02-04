using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly UserDbContext _context;
    
    public StaffRepository(UserDbContext context) => _context = context;
    
    public async Task<Staff?> AddStaffAsync(Staff? user)
    {
        await _context.Staff.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<Staff?> GetStaffByIdAsync(Guid userId)
    {
        return await _context.Staff.FindAsync(userId);
    }

    public async Task<bool> UpdateStaffAsync(Staff? user)
    {
        var existingStaff = await _context.Staff.FindAsync(user.UserId);
        
        if (existingStaff is null) return false;

        var entry = _context.Entry(existingStaff);
        entry.CurrentValues.SetValues(user);

        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeleteStaffAsync(Guid userId)
    {
        var user = await _context.Staff.FindAsync(userId);
        
        if (user is null) return false;
        
        user.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}