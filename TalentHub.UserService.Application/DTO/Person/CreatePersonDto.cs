using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Application.DTO.Person;

public class CreatePersonDto
{
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
    public UserNotificationSettingsDto UserSettings { get; set; }
}