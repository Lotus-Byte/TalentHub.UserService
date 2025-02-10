using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Staff;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateStaffAsync(CreateStaffDto createStaffDto)
    {
        var staff = _mapper.Map<CreateStaffDto, Staff>(createStaffDto); 
        
        await _unitOfWork.Staffs.AddStaffAsync(staff);
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            NotificationSettings = staff.UserSettings.NotificationSettings
        });
        
        return staff.UserId;
    }

    public async Task<StaffDto?> GetStaffByIdAsync(Guid userId)
    {
        var staff = await _unitOfWork.Staffs.GetStaffByIdAsync(userId);
        
        if (staff == null) return null;
        
        var staffDto = _mapper.Map<Staff, StaffDto>(staff);
        
        return staffDto;
    }

    public async Task<bool> UpdateStaffAsync(UpdateStaffDto updateStaffDto)
    {
        var staff = await _unitOfWork.Staffs.GetStaffByIdAsync(updateStaffDto.UserId);
        
        if (staff == null) return false;
        
        staff = _mapper.Map<UpdateStaffDto, Staff>(updateStaffDto);
        
        await _unitOfWork.Staffs.UpdateStaffAsync(staff);
        
        return true;
    }

    public async Task<bool> DeleteStaffAsync(Guid userId)
    {
        var result =  await _unitOfWork.Staffs.DeleteStaffAsync(userId);
        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        await _unitOfWork.Staffs.DeleteStaffAsync(userId);

        return result;
    }
}