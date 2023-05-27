using System.ComponentModel.DataAnnotations;

namespace Classify.Service.DTOs.Users
{
    public class UserUpdateDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string School { get; set; } = string.Empty;
    }
}
