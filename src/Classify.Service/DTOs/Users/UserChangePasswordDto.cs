using System.ComponentModel.DataAnnotations;


namespace Classify.Service.DTOs.Users;

public class UserChangePasswordDto
{
    [Required(ErrorMessage = "Email is requaried!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Old password must not be null or empty!")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "New password must not be null or empty!")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirming password must not be null or empty!")]
    [Compare("NewPassword")]
    public string ComfirmPassword { get; set; }
}
