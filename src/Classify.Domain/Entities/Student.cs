using Classify.Domain.Commons;
using Classify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Classify.Domain.Entities;

public class Student : Auditable
{
    public short Grade { get; set; }
    [MaxLength(32)]
    public string FirstName { get; set; }
    [MaxLength(32)]
    public string LastName { get; set; }
    [MaxLength(32)]
    public string Surname { get; set; }
    public string BirthCertificateSeria { get; set; }
    public string BirthCertificateNumber { get; set; }
    public string PassportSeria { get; set; }
    public string PassportNumber { get; set; }
    public short Gender { get; set; }
    
    [MaxLength(32)]
    public string Region {get; set;}
    public string School { get; set;}
    public string Bearings { get; set; }
  
    [MaxLength(128)]
    public string Language { get; set; }
}
