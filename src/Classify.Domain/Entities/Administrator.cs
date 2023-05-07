using Classify.Domain.Commons;
using Classify.Domain.Enums;

namespace Classify.Domain.Entities
{
    public class Administrator : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    
    }
}
