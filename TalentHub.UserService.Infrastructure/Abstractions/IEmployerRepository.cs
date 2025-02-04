using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Abstractions;

public interface IEmployerRepository
{
    Task<Employer?> AddEmployerAsync(Employer? user);
    Task<Employer?> GetEmployerByIdAsync(Guid userId);
    Task<bool> UpdateEmployerAsync(Employer? user);
    Task<bool> DeleteEmployerAsync(Guid userId);
}