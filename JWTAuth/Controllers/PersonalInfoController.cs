using System.Security.Claims;
using JWTAuth.Dtos;
using JWTAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Controllers;

[Route("/api/portfolio/[controller]/{portfolioId:guid}")]
[ApiController]
public class PersonalInfoController(IPersonalInfoService personalInfoService, IPortfolioService portfolioService) : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<PersonalInfoCreateUpdateDto>> GetPersonalInfoAsync(Guid portfolioId)
    {
        var portfolio = await personalInfoService.GetPersonalInfoAsync(portfolioId);
        if (portfolio is null) return BadRequest("Portfolio/Personal Info does not exist");
        return Ok(portfolio);
    }
    
    [HttpPost("")]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<PersonalInfoCreateUpdateDto>> CreatePersonalInfoAsync(Guid portfolioId, PersonalInfoCreateUpdateDto personalInfo)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var portfolio = await personalInfoService.UpdatePersonalInfoAsync(portfolioId, personalInfo);
        if (portfolio is null) return BadRequest("Portfolio does not exist");
        return Ok(portfolio);
    }
}