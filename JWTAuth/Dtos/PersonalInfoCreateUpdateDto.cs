using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Dtos;

public class PersonalInfoCreateUpdateDto
{
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(100)]
    public string? Address { get; set; }

    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    [Phone]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Url]
    public string? GithubUrl { get; set; }

    [Url]
    public string? LinkedInUrl { get; set; }

    [Url]
    public string? TwitterUrl { get; set; }

    [Url]
    public string? InstagramUrl { get; set; }

    [Url]
    public string? FacebookUrl { get; set; }
}