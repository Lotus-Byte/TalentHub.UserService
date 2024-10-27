namespace TalentHub.UserService.Api.Models.Person;

public class CreatePersonModel
{
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
}