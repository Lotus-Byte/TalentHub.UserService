using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Interfaces;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly UserDbContext _context;
    
    public PersonRepository(UserDbContext context) => _context = context;
    
    public async Task<Person?> AddPersonAsync(Person? user)
    {
        await _context.Persons.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    public async Task<Person?> GetPersonByIdAsync(Guid userId)
    {
        return await _context.Persons.FindAsync(userId);
    }

    public async Task<bool> UpdatePersonAsync(Person? user)
    {
        var existingPerson = await _context.Persons.FindAsync(user.UserId);
        
        if (existingPerson is null) return false;

        var entry = _context.Entry(existingPerson);
        entry.CurrentValues.SetValues(user);

        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeletePersonAsync(Guid userId)
    {
        var user = await _context.Persons.FindAsync(userId);
        
        if (user is null) return false;
        
        user.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}