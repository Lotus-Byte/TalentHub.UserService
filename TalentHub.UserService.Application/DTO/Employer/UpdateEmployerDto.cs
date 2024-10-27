using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Application.DTO.Employer;

public class UpdateEmployerDto
{
    [Required]
    public Guid UserId { get; set; }
    
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int Type { get; set; }
    public string? City { get; set; }
    public string? ShortName { get; set; }
    public string? FullName { get; set; }
    public int Grn { get; set; }
    public string? Inn { get; set; }
    public string? Kpp { get; set; }
    public string? Address { get; set; }
}