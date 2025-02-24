using AutoMapper;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Models.Settings;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationEventFactory _eventFactory;
    private readonly IMapper _mapper;

    public PersonService(IUnitOfWork unitOfWork, INotificationEventFactory eventFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _eventFactory = eventFactory;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreatePersonAsync(CreatePersonDto createPersonDto)
    {
        var person = _mapper.Map<CreatePersonDto, Person>(createPersonDto); 
        
        await _unitOfWork.Persons.AddPersonAsync(person);
        
        await _unitOfWork.UserSettings.AddUserSettingsAsync(new UserSettings
        {
            UserId = person.UserId,
            NotificationSettings = _mapper.Map<UserNotificationSettingsDto, UserNotificationSettings>(createPersonDto.UserSettings),
            Created = DateTimeOffset.Now,
            Updated = DateTimeOffset.Now
        });

        var notificationEvent = _eventFactory.Create(
            person.UserId,
            new Notification
            {
                Title = "Person created",
                Content = $"New '{person.UserId}' person has been created",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);

        await _unitOfWork.CommitChangesAsync();
        
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
        
        var notificationEvent = _eventFactory.Create(
            person.UserId,
            new Notification
            {
                Title = "Person updated",
                Content = $"'{person.UserId}' person information has been updated",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();
        
        return true;
    }

    public async Task<bool> DeletePersonAsync(Guid userId)
    {
        var result = await _unitOfWork.Persons.DeletePersonAsync(userId);
        
        await _unitOfWork.UserSettings.DeleteUserSettingsAsync(userId);
        
        var notificationEvent = _eventFactory.Create(
            userId,
            new Notification
            {
                Title = "Person deleted",
                Content = $"'{userId}' person and personal notification settings have been deleted",
            });

        _unitOfWork.AddDomainEvent(notificationEvent);
        
        await _unitOfWork.CommitChangesAsync();
        
        return result;
    }
}