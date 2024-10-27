using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Api.Models.Staff;

public class UpdateStaffModel
{
    [Required]
    public Guid UserId { get; set; }
    
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; } 
    public string? AccessLevel { get; set; }
}