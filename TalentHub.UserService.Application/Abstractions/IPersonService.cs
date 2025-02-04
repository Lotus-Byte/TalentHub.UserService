using TalentHub.UserService.Application.DTO.Person;

namespace TalentHub.UserService.Application.Interfaces;

public interface IPersonService
{
    Task<Guid> CreatePersonAsync(CreatePersonDto user);
    Task<PersonDto?> GetPersonByIdAsync(Guid userId);
    Task<bool> UpdatePersonAsync(UpdatePersonDto user);
    Task<bool> DeletePersonAsync(Guid userId);
}