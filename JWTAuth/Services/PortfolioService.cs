using JWTAuth.Data;
using JWTAuth.Dtos;
using JWTAuth.Entities;
using JWTAuth.Mapper;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Services;

public class PortfolioService(UserDbContext _context) : IPortfolioService
{
    public async Task<PortfolioDetailsDto?> GetPortfolioAsync(Guid portfolioId)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.PersonalInfo)
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId); // TODO: Add Include()
        return portfolio is null ? null : PortfolioMapper.From(portfolio);
    }

    public async Task<PortfolioDetailsDto?> UpdateProfessionalSummary(Guid portfolioId, string summary)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.PersonalInfo)
            .Include(p => p.Educations) // 
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        portfolio.ProfessionalSummary = summary;
        await _context.SaveChangesAsync();
        return PortfolioMapper.From(portfolio);
    }

    public async Task<bool> ToggleIsPublic(Guid portfolioId)
    {
       var portfolio = await _context.Portfolios
           .FindAsync(portfolioId);
       if (portfolio is null) return false;
       portfolio.IsPublic = !portfolio.IsPublic;
       await _context.SaveChangesAsync();
       return true;
    }

    public async Task<Guid> GetUserId(Guid portfolioId)
    {
        var portfolio = await _context.Portfolios.FindAsync(portfolioId);
        return portfolio?.UserId ?? Guid.Empty;
    }
}