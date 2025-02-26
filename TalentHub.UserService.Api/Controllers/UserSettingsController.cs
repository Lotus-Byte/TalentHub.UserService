using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserSettingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserSettingsService _service;
    
    public UserSettingsController(IMapper mapper, IUserSettingsService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserSettingsAsync([FromBody] CreateUserSettingsModel createUserSettingsModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var settings = _mapper.Map<CreateUserSettingsDto>(createUserSettingsModel);
        
        if (settings is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.CreateUserSettingsAsync(settings));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserSettingsAsync(Guid id)
    { 
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userSettingsDto = await _service.GetUserSettingsByIdAsync(id);
        
        if (userSettingsDto is null) return NotFound($"Configurations for user '{id}' not found");
        
        return Ok(_mapper.Map<UserNotificationSettingsModel>(userSettingsDto.NotificationSettings));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUserSettingsAsync([FromBody] UpdateUserSettingsModel updateUserSettingsModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var userSettings = _mapper.Map<UpdateUserSettingsDto>(updateUserSettingsModel);
        
        if (userSettings is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.UpdateUserSettingsAsync(userSettings));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUserSettingsAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _service.DeleteUserSettingsAsync(id);
        
        return result ? NoContent() : NotFound();
    }
}