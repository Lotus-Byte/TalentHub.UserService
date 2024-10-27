using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TalentHub.UserService.Api.Models.Person;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Application.Interfaces;

namespace TalentHub.UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersonService _service;
    
    public PersonController(IMapper mapper, IPersonService service)
    {
        _mapper = mapper;
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePersonAsync([FromBody] CreatePersonModel createPersonModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<CreatePersonDto>(createPersonModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.CreatePersonAsync(user));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var personDto = await _service.GetPersonByIdAsync(id);
        
        if (personDto is null) return NotFound($"Person with '{id}' id not found");
        
        return Ok(_mapper.Map<PersonModel>(personDto));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdatePersonAsync([FromBody] UpdatePersonModel updatePersonModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var user = _mapper.Map<UpdatePersonDto>(updatePersonModel);
        
        if (user is null) return BadRequest("Incorrect data");
        
        return Ok(await _service.UpdatePersonAsync(user));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePersonAsync(Guid id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
            
        var result = await _service.DeletePersonAsync(id);
        
        return result ? NoContent() : NotFound();
    }
}