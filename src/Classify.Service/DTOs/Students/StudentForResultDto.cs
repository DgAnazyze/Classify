using Classify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify.Service.DTOs.Students
{
    public class StudentForResultDto
    {
        public long Id { get; set; }
        public short Grade { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string BirthCertificateSeria { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string PassportSeria { get; set; }
        public string PassportNumber { get; set; }
        public byte Gender { get; set; }
        public string Region { get; set; }
        public string School { get; set; }
        public string Bearings { get; set; }
        public string Language { get; set; }
    }
}
