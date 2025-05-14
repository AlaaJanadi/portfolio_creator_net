using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Services;

public interface IPortfolioService
{
    Task<PortfolioDetailsDto?> GetPortfolioAsync(Guid portfolioId);

    Task<PortfolioDetailsDto?> UpdateProfessionalSummary(Guid portfolioId, string summary);

    Task<bool> ToggleIsPublic(Guid portfolioId);
    
    Task<Guid> GetUserId(Guid portfolioId);

}