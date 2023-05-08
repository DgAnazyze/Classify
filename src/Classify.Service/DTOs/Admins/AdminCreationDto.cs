using Classify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Classify.Service.DTOs.Administrator;

public class AdminCreationDto
{
    [Required, MaxLength(32)]
    public string FirstName { get; set; }
    [Required, MaxLength(32)]
    public string LastName { get; set; }
    [Required, MaxLength(32)]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Phone is required")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Phone is required")]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public Role Role { get; set; }
}
