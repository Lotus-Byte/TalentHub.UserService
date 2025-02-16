using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Infrastructure.Abstractions.Repositories;

public interface IStaffRepository
{
    Task<Staff?> AddStaffAsync(Staff? user);
    Task<Staff?> GetStaffByIdAsync(Guid userId);
    Task<bool> UpdateStaffAsync(Staff? user);
    Task<bool> DeleteStaffAsync(Guid userId);
}