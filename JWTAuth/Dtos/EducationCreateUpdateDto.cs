using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Dtos;

public class EducationCreateUpdateDto
{
    [MaxLength(255)]
    public required string SchoolName { get; set; } 
    [MaxLength(1000)]
    public required string Description { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsCurrent { get; set; } = false;
    public List<string>? Details { get; set; } = [];
    public string CertificateUrl { get; set; }  = string.Empty;
}