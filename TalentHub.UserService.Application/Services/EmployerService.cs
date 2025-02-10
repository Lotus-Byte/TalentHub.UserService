using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Employer;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class EmployerService : IEmployerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public EmployerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateEmployerAsync(CreateEmployerDto createEmployerDto)
    {
        var employer = _mapper.Map<CreateEmployerDto, Employer>(createEmployerDto);

        await _unitOfWork.Employers.AddEmployerAsync(employer);
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            NotificationSettings = employer.UserSettings.NotificationSettings
        });

    await _unitOfWork.SaveChangesAsync();
        
        return employer.UserId;
    }

    public async Task<EmployerDto?> GetEmployerByIdAsync(Guid userId)
    {
        var employer = await _unitOfWork.Employers.GetEmployerByIdAsync(userId);
        
        if (employer == null) return null;
        
        var employerDto = _mapper.Map<Employer, EmployerDto>(employer);
        
        return employerDto;
    }
    
    public async Task<bool> UpdateEmployerAsync(UpdateEmployerDto updateEmployerDto)
    {
        var employer = await _unitOfWork.Employers.GetEmployerByIdAsync(updateEmployerDto.UserId);
        
        if (employer == null) return false;
        
        employer = _mapper.Map<UpdateEmployerDto, Employer>(updateEmployerDto);
        
        await _unitOfWork.Employers.UpdateEmployerAsync(employer);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeleteEmployerAsync(Guid userId)
    {
        var result = await _unitOfWork.Employers.DeleteEmployerAsync(userId);
        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        await _unitOfWork.SaveChangesAsync();

        return result;
    }
}