namespace TalentHub.UserService.Infrastructure.Models.Users;

public class Employer : User
{
    public int Type { get; set; }
    public string? City { get; set; }
    public string? ShortName { get; set; }
    public string? FullName { get; set; }
    public int Grn { get; set; }
    public string? Inn { get; set; }
    public string? Kpp { get; set; }
    public string? Address { get; set; }
}