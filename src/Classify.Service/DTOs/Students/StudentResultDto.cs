namespace Classify.Service.DTOs.Students;

public class StudentResultDto
{
    public long Id { get; set; }
    public short Grade { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public string BirthCertificateSeria { get; set; } = string.Empty;
    public string BirthCertificateNumber { get; set; } = string.Empty;
    public string PassportSeria { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public short Gender { get; set; }
    public string Region { get; set; }
    public string School { get; set; }
    public string Bearings { get; set; }
    public string Language { get; set; }
}
