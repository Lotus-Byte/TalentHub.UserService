using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Staff;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationEventFactory _eventFactory;
    private readonly IMapper _mapper;

    public StaffService(IUnitOfWork unitOfWork, INotificationEventFactory eventFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventFactory = eventFactory;
        _mapper = mapper;
    }

    public async Task<Guid> CreateStaffAsync(CreateStaffDto createStaffDto)
    {
        var staff = _mapper.Map<CreateStaffDto, Staff>(createStaffDto); 
        
        await _unitOfWork.Staffs.AddStaffAsync(staff);
        
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            UserId = staff.UserId,
            NotificationSettings = _mapper.Map<UserNotificationSettingsDto, UserNotificationSettings>(createStaffDto.UserSettings),
            Created = DateTime.Now,
            Updated = DateTime.Now
        });

        var notificationEvent = _eventFactory.Create(
            staff.UserId,
            staff.UserSettings.NotificationSettings,
            new Notification
            {
                Title = "Staff created",
                Content = $"New '{staff.UserId}' staff has been created",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);

        await _unitOfWork.CommitChangesAsync();
        
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
        
        var notificationEvent = _eventFactory.Create(
            staff.UserId,
            staff.UserSettings.NotificationSettings,
            new Notification
            {
                Title = "Staff updated",
                Content = $"'{staff.UserId}' staff information has been updated",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteStaffAsync(Guid userId)
    {
        // TODO: PROCESS THE NULL CASE
        var settings = await _unitOfWork.UserSettings.GetUserSettingsByIdAsync(userId);
        
        var result =  await _unitOfWork.Staffs.DeleteStaffAsync(userId);
        
        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        
        var notificationEvent = _eventFactory.Create(
            userId,
            settings.NotificationSettings,
            new Notification
            {
                Title = "Staff deleted",
                Content = $"'{userId}' staff and personal notification settings have been deleted",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();

        return result;
    }
}