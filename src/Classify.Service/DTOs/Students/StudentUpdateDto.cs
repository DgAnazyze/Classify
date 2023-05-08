using Classify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify.Service.DTOs.Students
{

    public class StudentUpdateDto
    {
        [Required]
        public short Grade { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string BirthCertificateSeria { get; set; }
        [Required]
        public string BirthCertificateNumber { get; set; }
        [Required]
        public string PassportSeria { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public short Gender { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public string Bearings { get; set; }
        [Required]
        public string Language { get; set; }
    }
}
