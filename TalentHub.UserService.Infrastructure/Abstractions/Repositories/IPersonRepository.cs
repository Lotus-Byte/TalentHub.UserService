using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Infrastructure.Abstractions.Repositories;

public interface IPersonRepository
{
    Task<Person?> AddPersonAsync(Person? user);
    Task<Person?> GetPersonByIdAsync(Guid userId);
    Task<bool> UpdatePersonAsync(Person? user);
    Task<bool> DeletePersonAsync(Guid userId);
}