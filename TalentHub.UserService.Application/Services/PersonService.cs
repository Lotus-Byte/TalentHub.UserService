using AutoMapper;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Application.Interfaces;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreatePersonAsync(CreatePersonDto createPersonDto)
    {
        var person = _mapper.Map<CreatePersonDto, Person>(createPersonDto); 
        
        await _repository.AddPersonAsync(person);
        
        return person.UserId;
    }

    public async Task<PersonDto?> GetPersonByIdAsync(Guid userId)
    {
        var person = await _repository.GetPersonByIdAsync(userId);
        
        if (person == null) return null;
        
        var personDto = _mapper.Map<Person, PersonDto>(person);
        
        return personDto;
    }

    public async Task<bool> UpdatePersonAsync(UpdatePersonDto updatePersonDto)
    {
        var person = await _repository.GetPersonByIdAsync(updatePersonDto.UserId);
        
        if (person == null) return false;
        
        person = _mapper.Map<UpdatePersonDto, Person>(updatePersonDto);
        
        await _repository.UpdatePersonAsync(person);
        
        return true;
    }

    public async Task<bool> DeletePersonAsync(Guid userId)
    {
        return await _repository.DeletePersonAsync(userId);
    }
}