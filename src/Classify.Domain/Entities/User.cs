using Classify.Domain.Commons;
using Classify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Classify.Domain.Entities
{
    public class User : Auditable
    {
        [MaxLength(32)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(32)]
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.SchoolAdmin;
    }
}
