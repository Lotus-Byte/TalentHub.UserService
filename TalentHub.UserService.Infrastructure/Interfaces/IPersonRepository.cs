using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Infrastructure.Interfaces;

public interface IPersonRepository
{
    Task<Person?> AddPersonAsync(Person? user);
    Task<Person?> GetPersonByIdAsync(Guid userId);
    Task<bool> UpdatePersonAsync(Person? user);
    Task<bool> DeletePersonAsync(Guid userId);
}