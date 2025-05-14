namespace JWTAuth.Dtos;

public class EducationDetailsDto
{
    public string SchoolName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public List<string>? Details { get; set; } = [];
    public string? CertificateUrl { get; set; } = string.Empty;
}