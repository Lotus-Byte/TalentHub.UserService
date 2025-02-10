namespace TalentHub.UserService.Application.DTO.UserSettings;

public class UserSettingsDto
{
    public Guid UserSettingsId { get; set; }
    public Guid UserId { get; set; }
    public UserNotificationSettingsDto UserSettings { get; init; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Deleted { get; set; }
}