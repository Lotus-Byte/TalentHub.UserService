using System.Collections.Concurrent;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Abstractions.Repositories;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Repositories;

namespace TalentHub.UserService.Infrastructure.Providers;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext _context;
    private readonly IEventHandler<NotificationEvent> _eventHandler;
    
    private readonly ConcurrentBag<NotificationEvent> _domainEvents = [];

    private IEmployerRepository? _employers;
    private IPersonRepository? _persons;
    private IStaffRepository? _staffs;
    private IUserSettingsRepository? _userSettings;

    public UnitOfWork(UserDbContext context, IEventHandler<NotificationEvent> eventHandler)
    {
        _context = context;
        _eventHandler = eventHandler;
    }
    
    public IEmployerRepository Employers => _employers ??= new EmployerRepository(_context);
    public IPersonRepository Persons => _persons ??= new PersonRepository(_context);
    public IStaffRepository Staffs => _staffs ??= new StaffRepository(_context);
    public IUserSettingsRepository UserSettings => _userSettings ??= new UserSettingsRepository(_context);
    
    public void AddDomainEvent(NotificationEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public async Task CommitChangesAsync()
    {
        await _context.SaveChangesAsync();
        
        foreach (var domainEvent in _domainEvents)
        {
            await _eventHandler.HandleAsync(domainEvent);
        }

        _domainEvents.Clear();
    }

    public void Dispose() => _context.Dispose();
}