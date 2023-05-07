using System.ComponentModel.DataAnnotations;

namespace Classify.Service.DTOs.Administrator
{
    public class AdministratorForUpdateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
    }
}
