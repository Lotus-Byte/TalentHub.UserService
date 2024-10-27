using System.ComponentModel.DataAnnotations;

namespace TalentHub.UserService.Application.DTO.Person;

public class UpdatePersonDto
{
    [Required]
    public Guid UserId { get; set; }
    
    public string? Role { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
}