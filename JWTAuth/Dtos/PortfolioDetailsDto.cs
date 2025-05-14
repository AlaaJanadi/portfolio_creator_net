using JWTAuth.Entities;

namespace JWTAuth.Dtos;

public class PortfolioDetailsDto
{
    public string PortfolioImageUrl { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public PersonalInfoCreateUpdateDto? PersonalInfo { get; set; }
    public List<EducationDetailsDto>? Educations { get; set; }
}