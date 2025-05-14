using JWTAuth.Data;
using JWTAuth.Dtos;
using JWTAuth.Entities;
using JWTAuth.Mapper;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Services;

public class PersonalInfoService(UserDbContext _context) : IPersonalInfoService
{
    public async Task<PersonalInfoCreateUpdateDto?> GetPersonalInfoAsync(Guid portfolioId)
    {
        Console.WriteLine(portfolioId);
        var portfolio = await _context.Portfolios
            .Include(p => p.PersonalInfo)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        if (portfolio.PersonalInfo is null) return null;
        return PersonalInfoMapper.From(portfolio.PersonalInfo);
    }

    public async Task<PersonalInfoCreateUpdateDto?> UpdatePersonalInfoAsync(Guid portfolioId, PersonalInfoCreateUpdateDto req)
    {
        // first find the portfolio 
        var portfolio = await _context.Portfolios
            .Include(p => p.PersonalInfo)
            .FirstOrDefaultAsync(p => p.Id == portfolioId);
        if (portfolio is null) return null;
        // if the personal info is null, create a new one
        if (portfolio.PersonalInfo == null)
        {
            portfolio.PersonalInfo = req.To();
            portfolio.PersonalInfo.PortfolioId = portfolioId;
            _context.PersonalInfos.Add(portfolio.PersonalInfo);
            await _context.SaveChangesAsync();
            return PersonalInfoMapper.From(portfolio.PersonalInfo);
        }
        // update the personal info
        portfolio.PersonalInfo = req.To();
        _context.PersonalInfos.Update(portfolio.PersonalInfo);
        await _context.SaveChangesAsync();
        return PersonalInfoMapper.From(portfolio.PersonalInfo);
    }

   
}