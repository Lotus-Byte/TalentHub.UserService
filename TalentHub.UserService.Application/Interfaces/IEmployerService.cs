using TalentHub.UserService.Application.DTO.Employer;

namespace TalentHub.UserService.Application.Interfaces;

public interface IEmployerService
{
    Task<Guid> CreateEmployerAsync(CreateEmployerDto user);
    Task<EmployerDto?> GetEmployerByIdAsync(Guid userId);
    Task<bool> UpdateEmployerAsync(UpdateEmployerDto user);
    Task<bool> DeleteEmployerAsync(Guid userId);
}