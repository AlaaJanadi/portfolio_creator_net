using System.Security.Claims;
using JWTAuth.Dtos;
using JWTAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Controllers;

[Route("api/[controller]/{portfolioId:guid}")]
[ApiController]
public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<PortfolioDetailsDto>> Get(Guid portfolioId)
    {
        var portfolio = await portfolioService.GetPortfolioAsync(portfolioId);
        return portfolio is null ? NotFound() : Ok(portfolio);
    }

    [HttpPost("professional-summary")]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<PortfolioDetailsDto>> AddProfessionalSummary(Guid portfolioId, string summary)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var portfolio = await portfolioService.UpdateProfessionalSummary(portfolioId, summary);
        return portfolio is null ? NotFound() : Ok(portfolio);
    }
    
    [HttpPut("is-public")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<string>> ToggleIsPublic(Guid portfolioId)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var isPublic = await portfolioService.ToggleIsPublic(portfolioId);
        return isPublic ? Ok("Your Public Status has been updated") : BadRequest("Portfolio does not exist");
    }
}