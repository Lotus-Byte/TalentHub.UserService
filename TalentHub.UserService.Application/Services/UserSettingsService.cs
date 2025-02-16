using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;

namespace TalentHub.UserService.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationEventFactory _eventFactory;
    private readonly IMapper _mapper;

    public UserSettingsService(IUnitOfWork unitOfWork, INotificationEventFactory eventFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventFactory = eventFactory;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreateUserSettingsAsync(CreateUserSettingsDto createUserSettingsDto)
    {
        var userSettings = _mapper.Map<CreateUserSettingsDto, UserSettings>(createUserSettingsDto); 
        
        await _unitOfWork.UserSettings.AddUserSettingsAsync(userSettings);
        
        var notificationEvent = _eventFactory.Create(
            userSettings.UserId,
            userSettings.NotificationSettings,
            new Notification
            {
                Title = "Notification settings created",
                Content = $"'{userSettings.UserId}' user notification settings have been created",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);

        await _unitOfWork.CommitChangesAsync();
        
        return userSettings.UserId;
    }
    
    public async Task<UserSettingsDto?> GetUserSettingsByIdAsync(Guid userId)
    {
        var userSettings = await _unitOfWork.UserSettings.GetUserSettingsByIdAsync(userId);
        
        if (userSettings == null) return null;
        
        var userSettingsDto = _mapper.Map<UserSettings, UserSettingsDto>(userSettings);
        
        return userSettingsDto;
    }

    public async Task<bool> UpdateUserSettingsAsync(UpdateUserSettingsDto updateUserSettingsDto)
    {
        var userSettings = await _unitOfWork.UserSettings.GetUserSettingsByIdAsync(updateUserSettingsDto.UserId);
        
        if (userSettings is null) return false;
        
        userSettings = _mapper.Map<UpdateUserSettingsDto, UserSettings>(updateUserSettingsDto);
        
        await _unitOfWork.UserSettings.UpdateUserSettingsAsync(userSettings);
        
        var notificationEvent = _eventFactory.Create(
            userSettings.UserId,
            userSettings.NotificationSettings,
            new Notification
            {
                Title = "Notification settings updated",
                Content = $"'{userSettings.UserId}' user notification settings have been updated",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);

        await _unitOfWork.CommitChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteUserSettingsAsync(Guid userId)
    {
        // TODO: PROCESS THE NULL CASE
        var settings = await _unitOfWork.UserSettings.GetUserSettingsByIdAsync(userId);
        
        var result =  await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        
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