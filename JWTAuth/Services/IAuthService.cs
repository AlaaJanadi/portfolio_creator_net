using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Services;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto req);
    Task<TokenResponseDto?> LoginAsync(UserDto req);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req);
    Task<bool> Logout(Guid userId);
}