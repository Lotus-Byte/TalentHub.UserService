using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Employer;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Services;

public class EmployerService : IEmployerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationEventFactory _eventFactory;
    private readonly IMapper _mapper;
    
    public EmployerService(IUnitOfWork unitOfWork, INotificationEventFactory eventFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventFactory = eventFactory;
        _mapper = mapper;
    }

    public async Task<Guid> CreateEmployerAsync(CreateEmployerDto createEmployerDto)
    {
        var employer = _mapper.Map<CreateEmployerDto, Employer>(createEmployerDto);

        await _unitOfWork.Employers.AddEmployerAsync(employer);
        
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            UserId = employer.UserId,
            NotificationSettings = _mapper.Map<UserNotificationSettingsDto, UserNotificationSettings>(createEmployerDto.UserSettings),
            Created = DateTimeOffset.Now,
            Updated = DateTimeOffset.Now
        });

        var notificationEvent = _eventFactory.Create(
            employer.UserId,
            new Notification
            {
                Title = "Employer created",
                Content = $"New '{employer.UserId}' employer has been created",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);

        await _unitOfWork.CommitChangesAsync();
        
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
        
        var notificationEvent = _eventFactory.Create(
            employer.UserId,
            new Notification
            {
                Title = "Employer updated",
                Content = $"'{employer.UserId}' employer information has been updated",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeleteEmployerAsync(Guid userId)
    {
        var result = await _unitOfWork.Employers.DeleteEmployerAsync(userId);

        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        
        var notificationEvent = _eventFactory.Create(
            userId,
            new Notification
            {
                Title = "Employer deleted",
                Content = $"'{userId}' employer and personal notification settings have been deleted",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();

        return result;
    }
}