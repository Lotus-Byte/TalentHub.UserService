namespace TalentHub.UserService.Application.DTO.UserSettings;

public class CreateUserSettingsDto
{
    public Guid UserId { get; set; }
    public UserNotificationSettingsDto UserSettings { get; init; }
}