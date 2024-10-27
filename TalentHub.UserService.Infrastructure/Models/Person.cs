namespace TalentHub.UserService.Infrastructure.Models;

public class Person : User
{
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
}