using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuth.Dtos;
using JWTAuth.Entities;
using JWTAuth.Jwt;
using JWTAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto req)
    {
        var user = await authService.RegisterAsync(req);
        if (user is null)
            return BadRequest("User already exists");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(UserDto req)
    {
        var result = await authService.LoginAsync(req);
        if (result is null)
            return BadRequest("Bad credentials");
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto req)
    {
        var result = await authService.RefreshTokenAsync(req);
        if (result is null || result.AccessToken is null || result.RefreshToken is null)
            return Unauthorized("Invalid refresh token");
        return Ok(result);
    }

    [HttpPost("logout/{userId}")]
    public async Task<ActionResult> Logout(Guid userId)
    {
        var result = await authService.Logout(userId);
        if (result is false)
            return BadRequest("Bad credentials");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("authenticated-only")]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("Hi there");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("Hello, Admin");
    }
    
}