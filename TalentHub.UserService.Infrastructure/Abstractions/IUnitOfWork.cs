namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface IUnitOfWork
{
    IEmployerRepository Employers { get; }
    IPersonRepository Persons { get; }
    IStaffRepository Staffs { get; }
    IUserSettingsRepository UserSettings { get; }
    Task SaveChangesAsync();
}