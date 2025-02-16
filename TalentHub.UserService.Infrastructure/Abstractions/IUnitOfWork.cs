using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Abstractions.Repositories;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IEmployerRepository Employers { get; }
    IPersonRepository Persons { get; }
    IStaffRepository Staffs { get; }
    IUserSettingsRepository UserSettings { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    Task CommitChangesAsync();
}