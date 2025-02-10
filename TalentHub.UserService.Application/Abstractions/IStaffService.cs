using TalentHub.UserService.Application.DTO.Staff;

namespace TalentHub.UserService.Application.Abstractions;

public interface IStaffService
{
    Task<Guid> CreateStaffAsync(CreateStaffDto user);
    Task<StaffDto?> GetStaffByIdAsync(Guid userId);
    Task<bool> UpdateStaffAsync(UpdateStaffDto user);
    Task<bool> DeleteStaffAsync(Guid userId);
}