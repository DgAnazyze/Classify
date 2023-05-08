using Classify.Domain.Enums;

namespace Classify.Service.DTOs.Administrator;

public class AdminResultDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
}
