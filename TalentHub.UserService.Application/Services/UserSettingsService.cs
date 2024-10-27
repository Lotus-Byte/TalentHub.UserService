using AutoMapper;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Application.Interfaces;
using TalentHub.UserService.Infrastructure.Interfaces;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IMapper _mapper;
    private readonly IUserSettingsRepository _repository;

    public UserSettingsService(IMapper mapper, IUserSettingsRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<Guid> CreateUserSettingsAsync(CreateUserSettingsDto createUserSettingsDto)
    {
        var user = _mapper.Map<CreateUserSettingsDto, UserSettings>(createUserSettingsDto); 
        
        var createdUserSettings = await _repository.AddUserSettingsAsync(user);
        
        return createdUserSettings.UserId;
    }
    
    public async Task<UserSettingsDto?> GetUserSettingsByIdAsync(Guid userId)
    {
        var userSettings = await _repository.GetUserSettingsByIdAsync(userId);
        
        if (userSettings == null) return null;
        
        var userSettingsDto = _mapper.Map<UserSettings, UserSettingsDto>(userSettings);
        
        return userSettingsDto;
    }

    public async Task<bool> UpdateUserSettingsAsync(UpdateUserSettingsDto updateUserSettingsDto)
    {
        var userSettings = await _repository.GetUserSettingsByIdAsync(updateUserSettingsDto.UserId);
        
        if (userSettings is null) return false;
        
        userSettings = _mapper.Map<UpdateUserSettingsDto, UserSettings>(updateUserSettingsDto);
        
        await _repository.UpdateUserSettingsAsync(userSettings);
        
        return true;
    }

    public async Task<bool> DeleteUserSettingsAsync(Guid userId)
    {
        return await _repository.DeleteUserSettingsAsync(userId);
    }
}