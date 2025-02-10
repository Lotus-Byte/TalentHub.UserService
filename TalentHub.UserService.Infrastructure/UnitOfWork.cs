using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Repositories;

namespace TalentHub.UserService.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext _context;
    private IEmployerRepository _employers;
    private IPersonRepository _persons;
    private IStaffRepository _staffs;
    private IUserSettingsRepository _userSettings;

    public UnitOfWork(UserDbContext context) => _context = context;

    public IEmployerRepository Employers => _employers ??= new EmployerRepository(_context);
    public IPersonRepository Persons => _persons ??= new PersonRepository(_context);
    public IStaffRepository Staffs => _staffs ??= new StaffRepository(_context);
    public IUserSettingsRepository UserSettings => _userSettings ??= new UserSettingsRepository(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}