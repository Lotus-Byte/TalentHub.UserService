using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.UserService.Api.Models.Staff;
using TalentHub.UserService.Application.DTO.Staff;
using TalentHub.UserService.Application.Interfaces;

namespace TalentHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStaffService _service;
    
    public StaffController(IMapper mapper, IStaffService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStaffAsync([FromBody] CreateStaffModel createStaffModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<CreateStaffDto>(createStaffModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.CreateStaffAsync(_mapper.Map<CreateStaffDto>(user)));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStaffAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var staffDto = await _service.GetStaffByIdAsync(id);
        
        if (staffDto is null) return NotFound($"Staff with '{id}' id not found");
        
        return Ok(_mapper.Map<StaffModel>(staffDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStaffAsync([FromBody] UpdateStaffModel updateStaffModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<UpdateStaffDto>(updateStaffModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.UpdateStaffAsync(user));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteStaffAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _service.DeleteStaffAsync(id);
        
        return result ? NoContent() : NotFound();
    }
}