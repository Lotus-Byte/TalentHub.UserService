using TalentHub.UserService.Application.DTO.UserSettings;

namespace TalentHub.UserService.Application.DTO.Staff;

public class CreateStaffDto
{
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? AccessLevel { get; set; }
    public UserNotificationSettingsDto UserSettings { get; set; }
}