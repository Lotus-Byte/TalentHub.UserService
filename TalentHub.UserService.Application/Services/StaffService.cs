using AutoMapper;
using TalentHub.UserService.Application.DTO.Staff;
using TalentHub.UserService.Application.Interfaces;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class StaffService : IStaffService
{
    private readonly IMapper _mapper;
    private readonly IStaffRepository _repository;

    public StaffService(IStaffRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateStaffAsync(CreateStaffDto createStaffDto)
    {
        var staff = _mapper.Map<CreateStaffDto, Staff>(createStaffDto); 
        
        await _repository.AddStaffAsync(staff);
        
        return staff.UserId;
    }

    public async Task<StaffDto?> GetStaffByIdAsync(Guid userId)
    {
        var staff = await _repository.GetStaffByIdAsync(userId);
        
        if (staff == null) return null;
        
        var staffDto = _mapper.Map<Staff, StaffDto>(staff);
        
        return staffDto;
    }

    public async Task<bool> UpdateStaffAsync(UpdateStaffDto updateStaffDto)
    {
        var staff = await _repository.GetStaffByIdAsync(updateStaffDto.UserId);
        
        if (staff == null) return false;
        
        staff = _mapper.Map<UpdateStaffDto, Staff>(updateStaffDto);
        
        await _repository.UpdateStaffAsync(staff);
        
        return true;
    }

    public async Task<bool> DeleteStaffAsync(Guid userId)
    {
        var deleted =  await _repository.DeleteStaffAsync(userId);

        return deleted;
    }
}