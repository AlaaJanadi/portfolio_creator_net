using JWTAuth.Data;
using JWTAuth.Dtos;
using JWTAuth.Entities;
using JWTAuth.Mapper;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Services;

public class EducationService(UserDbContext _context) : IEductionService
{
    public async Task<List<EducationDetailsDto>?> GetAllEducations(Guid portfolioId)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        return portfolio.Educations.Select(e => e.From()).ToList();
    }

    public async Task<EducationDetailsDto?> GetEducation(Guid portfolioId, Guid educationId)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        return portfolio.Educations.FirstOrDefault(e => e.Id == educationId).From();
    }

    public async Task<EducationDetailsDto> AddEducation(Guid portfolioId, EducationCreateUpdateDto req)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        var education = req.To();
        portfolio.Educations?.Add(education);
        await _context.SaveChangesAsync();
        return education.From();
    }

    public async Task<EducationDetailsDto> UpdateEducation(Guid portfolioId, Guid educationId, EducationCreateUpdateDto req)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        var education = portfolio.Educations?.FirstOrDefault(e => e.Id == educationId);
        if (education is null) return null;
        education = req.To();
        await _context.SaveChangesAsync();
        return education.From();
    }

    public async Task<bool> DeleteEducation(Guid portfolioId, Guid educationId)
    {
        var portfolio = await _context.Portfolios
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return false;
        var education = portfolio.Educations?.FirstOrDefault(e => e.Id == educationId);
        if (education is null) return false;
        portfolio.Educations?.Remove(education);
        await _context.SaveChangesAsync();
        return true;
    }
}