using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreatePersonAsync(CreatePersonDto createPersonDto)
    {
        var person = _mapper.Map<CreatePersonDto, Person>(createPersonDto); 
        
        await _unitOfWork.Persons.AddPersonAsync(person);
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            NotificationSettings = person.UserSettings.NotificationSettings
        });
        
        return person.UserId;
    }

    public async Task<PersonDto?> GetPersonByIdAsync(Guid userId)
    {
        var person = await _unitOfWork.Persons.GetPersonByIdAsync(userId);
        
        if (person == null) return null;
        
        var personDto = _mapper.Map<Person, PersonDto>(person);
        
        return personDto;
    }

    public async Task<bool> UpdatePersonAsync(UpdatePersonDto updatePersonDto)
    {
        var person = await _unitOfWork.Persons.GetPersonByIdAsync(updatePersonDto.UserId);
        
        if (person == null) return false;
        
        person = _mapper.Map<UpdatePersonDto, Person>(updatePersonDto);
        
        await _unitOfWork.Persons.UpdatePersonAsync(person);
        
        return true;
    }

    public async Task<bool> DeletePersonAsync(Guid userId)
    {
        var result = await _unitOfWork.Persons.DeletePersonAsync(userId);
        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        await _unitOfWork.SaveChangesAsync();
        
        return result;
    }
}