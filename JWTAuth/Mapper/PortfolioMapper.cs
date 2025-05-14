using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Mapper;

public static class PortfolioMapper
{
    public static PortfolioDetailsDto From(Portfolio? portfolio)
    {
        if (portfolio == null) return null; 
        return new PortfolioDetailsDto()
        {
            PortfolioImageUrl = portfolio.PortfolioImageUrl,
            ProfessionalSummary = portfolio.ProfessionalSummary,
            IsPublic = portfolio.IsPublic,
            PersonalInfo = PersonalInfoMapper.From(portfolio.PersonalInfo),
            Educations = portfolio.Educations.Select(e => e.From()).ToList()
        };
    }
}