using JWTAuth.Data;
using JWTAuth.Dtos;
using JWTAuth.Entities;
using JWTAuth.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Services;

public class AuthService(UserDbContext context, IConfiguration configuration, JwtService jwt) : IAuthService
{
    public async Task<User?> RegisterAsync(UserDto req)
    {
        if (await context.Users.AnyAsync(u => u.Username == req.Username))
        {
            return null;
        }
        User user = new User();
        var hashedPassword = new PasswordHasher<User>()
            .HashPassword(user, req.Password);

        user.Username = req.Username;
        user.PasswordHash = hashedPassword;
        user.Role = "User";
        user.Portfolio = new Portfolio()
        {
            Id = Guid.NewGuid(),
            PortfolioImageUrl = string.Empty,
            ProfessionalSummary = string.Empty,
            IsPublic = true,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<TokenResponseDto?> LoginAsync(UserDto req)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
        if (user is null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, req.Password) ==
            PasswordVerificationResult.Success)
        {
            var response = await GenerateTokenResponse(user);
            return response;
        }
        return null;
    }

    private async Task<TokenResponseDto> GenerateTokenResponse(User user)
    {
        var response = new TokenResponseDto()
        {
            AccessToken = jwt.CreateToken(user),
            RefreshToken = await jwt.CreateAndSaveRefreshTokenAsync(user)
        };
        return response;
    }

    public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req)
    {
        var user = await jwt.ValidateRefreshTokenAsync(req.UserId, req.RefreshToken);
        if (user is null)
            return null;
        return await GenerateTokenResponse(user); 
    }

    public async Task<bool> Logout(Guid userId)
    {
        var user = await context.Users.FindAsync(userId);
        if (user is null)
            return false;
        user.RefreshToken = "";
        user.RefreshTokenExpiryTime = DateTime.MinValue;
        await context.SaveChangesAsync();
        return true;
    }
}