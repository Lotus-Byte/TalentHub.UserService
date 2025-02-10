using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.UserService.Api.Abstractions;
using TalentHub.UserService.Api.Models.Employer;
using TalentHub.UserService.Api.Models.Notification;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Employer;

namespace TalentHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly INotificationProducer _producer;
    private readonly IEmployerService _service;
    
    public EmployerController(IMapper mapper, INotificationProducer producer, IEmployerService service)
    {
        _mapper = mapper;
        _producer = producer;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployerAsync([FromBody] CreateEmployerModel createEmployerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<CreateEmployerDto>(createEmployerModel);
        
        if (user is null) return BadRequest("Incorrect data");

        var result = await _service.CreateEmployerAsync(user);

        _producer.SendAsync(new NotificationMessageModel
        {
            // TODO: CREATE NEW MQ MESSAGE
        });
        
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployerAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var employerDto = await _service.GetEmployerByIdAsync(id);
        
        if (employerDto is null) return NotFound($"Employer with '{id}' id not found");
        
        return Ok(_mapper.Map<EmployerModel>(employerDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployerAsync([FromBody] UpdateEmployerModel updateEmployerModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<UpdateEmployerDto>(updateEmployerModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.UpdateEmployerAsync(user));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployerAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _service.DeleteEmployerAsync(id);
        
        return result ? NoContent() : NotFound();
    }
}