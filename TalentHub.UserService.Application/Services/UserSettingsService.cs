using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _repository;
    private readonly IMapper _mapper;

    public UserSettingsService(IUserSettingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreateUserSettingsAsync(CreateUserSettingsDto createUserSettingsDto)
    {
        var userSettings = _mapper.Map<CreateUserSettingsDto, UserSettings>(createUserSettingsDto); 
        
        var createdUserSettings = await _repository.AddUserSettingsAsync(userSettings);
        
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