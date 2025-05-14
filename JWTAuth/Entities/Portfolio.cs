using System.Text.Json.Serialization;

namespace JWTAuth.Entities;

public class Portfolio
{
    public Guid Id { get; set; }
    public string PortfolioImageUrl { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    
    
    public PersonalInfo? PersonalInfo { get; set; }
    
    [JsonIgnore]
    public List<Education>? Educations { get; set; } = [];
    
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    
}