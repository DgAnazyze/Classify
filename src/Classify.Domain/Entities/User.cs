using Classify.Domain.Commons;
using Classify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Classify.Domain.Entities
{
    public class User : Auditable
    {
        [MaxLength(32)]
        public string FirstName { get; set; } 
        [MaxLength(32)]
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; } 
        public string PasswordHash { get; set; }
        public string Address { get; set; } 
        public Role Role { get; set; }
    }
}
