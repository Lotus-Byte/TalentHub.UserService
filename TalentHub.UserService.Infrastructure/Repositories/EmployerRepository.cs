using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Repositories;

public class EmployerRepository : IEmployerRepository
{
    private readonly UserDbContext _context;
    public EmployerRepository(UserDbContext context) => _context = context;
    
    public async Task<Employer?> AddEmployerAsync(Employer? user)
    {
        await _context.Employers.AddAsync(user);
        return user;
    }

    public async Task<Employer?> GetEmployerByIdAsync(Guid userId)
    {
        return await _context.Employers.FindAsync(userId);
    }

    public async Task<bool> UpdateEmployerAsync(Employer user)
    {
        var existingEmployer = await _context.Employers.FindAsync(user.UserId);
        
        if (existingEmployer is null)  return false;
        
        var entry = _context.Entry(existingEmployer);
        entry.CurrentValues.SetValues(user);

        return true;
    }
    
    public async Task<bool> DeleteEmployerAsync(Guid userId)
    {
        var user = await _context.Employers.FindAsync(userId);
        
        if (user is null) return false;
        
        user.Deleted = true;
        
        return true;
    }
}