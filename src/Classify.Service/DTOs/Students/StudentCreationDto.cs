using Classify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Classify.Service.DTOs.Students
{
    public class StudentCreationDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Grade is required")]
        public short Grade { get; set; }
        [Required,MaxLength(32)]
        public string FirstName { get; set; }
        [Required, MaxLength(32)]
        public string LastName { get; set; }
        [Required, MaxLength(32)]
        public string MiddleName { get; set; }
        public string BirthCertificateSeria { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string PassportSeria { get; set; }
        public string PassportNumber { get; set; }
        public byte Gender { get; set; }

        [Required, MaxLength(32)]
        public string Region { get; set; }
        public string School { get; set; }
        public string Bearings { get; set; }

        [Required, MaxLength(32)]
        public string Language { get; set; }
    }
}
