using System.ComponentModel.DataAnnotations;

namespace Classify.Service.DTOs.LoginDto;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
