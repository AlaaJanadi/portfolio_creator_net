using System.Security.Claims;
using JWTAuth.Dtos;
using JWTAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.Controllers;

[Route("api/portfolio/[controller]/{portfolioId:guid}")]
[ApiController]
public class EducationController(IEductionService educationService, IPortfolioService portfolioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<EducationDetailsDto>>> GetAllEducations(Guid portfolioId)
    {
        var educations = await educationService.GetAllEducations(portfolioId);
        return Ok(educations);
    }
    
    [HttpGet("{educationId:guid}")]
    public async Task<ActionResult<EducationDetailsDto>> GetEducation(Guid portfolioId, Guid educationId)
    {
        var education = await educationService.GetEducation(portfolioId, educationId);
        if (education is null) return NotFound("Education does not exist");
        return Ok(education);
    }

    [HttpPost]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<EducationDetailsDto>> AddEducation(Guid portfolioId,[FromBody] EducationCreateUpdateDto req)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var education = await educationService.AddEducation(portfolioId, req);
        if (education is null) return BadRequest("Portfolio does not exist");
        return Ok(education);
    }

    [HttpPut("{educationId:guid}")]
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<EducationDetailsDto>> UpdateEducation(Guid portfolioId, Guid educationId,[FromBody] EducationCreateUpdateDto req)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var education = await educationService.UpdateEducation(portfolioId, educationId, req);
        if (education is null) return BadRequest("Portfolio does not exist");
        return Ok(education);
    }
    
    [HttpDelete("{educationId:guid}")] 
    [Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<bool>> DeleteEducation(Guid portfolioId, Guid educationId)
    {
        var userGuid = await portfolioService.GetUserId(portfolioId);
        if (userGuid.ToString() != User.FindFirst(ClaimTypes.NameIdentifier)?.Value) return Forbid();
        var isDeleted = await educationService.DeleteEducation(portfolioId, educationId);
        return Ok(isDeleted);
    }


}