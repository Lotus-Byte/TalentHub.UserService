namespace TalentHub.UserService.Application.DTO.Staff;

public class StaffDto
{
    public Guid UserId { get; set; }
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? AccessLevel { get; set; }
    public DateTime Created { get; set; }
    public bool Deleted { get; set; }
}