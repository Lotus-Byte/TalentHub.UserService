namespace TalentHub.UserService.Infrastructure.Models.Users;

public class Person : User
{
    public DateTimeOffset BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
}