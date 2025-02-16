using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.UserService.Api.Abstractions;
using TalentHub.UserService.Api.Models.Employer;
using TalentHub.UserService.Api.Models.Notification;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Employer;

namespace TalentHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly INotificationProducer _producer;
    private readonly INotificationMessageModelFactory _messageFactory;
    private readonly IEmployerService _userService;
    private readonly IUserSettingsService _userSettingsService;
    
    public EmployerController(
        IMapper mapper, 
        INotificationProducer producer, 
        INotificationMessageModelFactory messageFactory, 
        IEmployerService userService,
        IUserSettingsService userSettingsService)
    {
        _mapper = mapper;
        _producer = producer;
        _messageFactory = messageFactory;
        _userService = userService;
        _userSettingsService = userSettingsService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployerAsync([FromBody] CreateEmployerModel createEmployerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<CreateEmployerDto>(createEmployerModel);
        
        if (user is null) return BadRequest("Incorrect data");

        var userId = await _userService.CreateEmployerAsync(user);
        
        var message = _messageFactory.Create(
            userId, 
            createEmployerModel.UserSettings,
            new NotificationModel
            {
                Title = "New employer created",
                Content = $"New employer '{userId}' created"
            });

        await _producer.SendAsync(message);
        
        return Ok(userId);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployerAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var employerDto = await _userService.GetEmployerByIdAsync(id);
        
        if (employerDto is null) return NotFound($"Employer with '{id}' id not found");
        
        return Ok(_mapper.Map<EmployerModel>(employerDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployerAsync([FromBody] UpdateEmployerModel updateEmployerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<UpdateEmployerDto>(updateEmployerModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _userService.UpdateEmployerAsync(user));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployerAsync(Guid userId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var settings = _mapper.Map<UserSettingsModel>(await _userSettingsService.GetUserSettingsByIdAsync(userId));
        var result = await _userService.DeleteEmployerAsync(userId);
        
        var message = _messageFactory.Create(
            userId, 
            settings.NotificationSettings,
            new NotificationModel
            {
                Title = "Employer deleted",
                Content = $"Employer '{userId}' deleted"
            });

        await _producer.SendAsync(message);
        
        return result ? NoContent() : NotFound();
    }
}