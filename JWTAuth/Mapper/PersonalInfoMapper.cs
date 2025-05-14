using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Mapper;

public static class PersonalInfoMapper
{
    public static PersonalInfoCreateUpdateDto From(this PersonalInfo? personalInfo)
    {
        if (personalInfo == null) return null; 
        return new PersonalInfoCreateUpdateDto()
        {
            FirstName = personalInfo.FirstName,
            LastName = personalInfo.LastName,
            Address = personalInfo.Address,
            PostalCode = personalInfo.PostalCode,
            City = personalInfo.City,
            Country = personalInfo.Country,
            PhoneNumber = personalInfo.PhoneNumber,
            Email = personalInfo.Email,
            GithubUrl = personalInfo.GithubUrl,
            LinkedInUrl = personalInfo.LinkedInUrl,
            TwitterUrl = personalInfo.TwitterUrl,
            InstagramUrl = personalInfo.InstagramUrl,
            FacebookUrl = personalInfo.FacebookUrl,
        };
    }
    
    public static PersonalInfo To(this PersonalInfoCreateUpdateDto personalInfoCreateUpdateDto)
    {
        return new PersonalInfo()
        {
            FirstName = personalInfoCreateUpdateDto.FirstName,
            LastName = personalInfoCreateUpdateDto.LastName,
            Address = personalInfoCreateUpdateDto.Address,
            PostalCode = personalInfoCreateUpdateDto.PostalCode,
            City = personalInfoCreateUpdateDto.City,
            Country = personalInfoCreateUpdateDto.Country,
            PhoneNumber = personalInfoCreateUpdateDto.PhoneNumber,
            Email = personalInfoCreateUpdateDto.Email,
            GithubUrl = personalInfoCreateUpdateDto.GithubUrl,
            LinkedInUrl = personalInfoCreateUpdateDto.LinkedInUrl,
            TwitterUrl = personalInfoCreateUpdateDto.TwitterUrl,
            InstagramUrl = personalInfoCreateUpdateDto.InstagramUrl,
            FacebookUrl = personalInfoCreateUpdateDto.FacebookUrl,
        };
    }
}