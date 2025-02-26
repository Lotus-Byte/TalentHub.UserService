using TalentHub.UserService.Infrastructure.Abstractions.Repositories;
using TalentHub.UserService.Infrastructure.Models.Notification;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IEmployerRepository Employers { get; }
    IPersonRepository Persons { get; }
    IStaffRepository Staffs { get; }
    IUserSettingsRepository UserSettings { get; }
    void AddDomainEvent(NotificationEvent domainEvent);
    Task CommitChangesAsync();
}