using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ICMON.Application.DTOs.Auth;
using ICMON.Application.Services;
using ICMON.Domain.Entities;
using ICMON.Infrastructure.Persistence;

namespace ICMON.Infrastructure.Authentication;

public class JwtTokenService : ITokenService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public JwtTokenService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponse> GenerateTokensAsync(User user, IList<string> roles, IList<string> permissions)
    {
        var accessTokenExpires = DateTime.UtcNow.AddMinutes(30);
        var refreshTokenExpires = DateTime.UtcNow.AddDays(7);

        var accessToken = GenerateAccessToken(user.Id, user.Username, roles, permissions, accessTokenExpires);
        var refreshToken = GenerateRefreshToken();

        var userToken = UserToken.Create(user.Id, accessToken, refreshToken, accessTokenExpires, refreshTokenExpires);
        _context.UserTokens.Add(userToken);
        await _context.SaveChangesAsync();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = accessTokenExpires,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email.Value,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status.ToString(),
                Roles = roles.ToList(),
                Permissions = permissions.ToList()
            }
        };
    }

    public async Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = _context.UserTokens
            .FirstOrDefault(t => t.RefreshToken == refreshToken && !t.IsRevoked);

        if (storedToken == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if (storedToken.IsRefreshTokenExpired)
            throw new UnauthorizedAccessException("Refresh token has expired");

        var user = _context.Users
            .Find(storedToken.UserId);

        if (user == null || user.Status != Domain.Enums.UserStatus.Active)
            throw new UnauthorizedAccessException("User account is not active");

        storedToken.Revoke();
        _context.UserTokens.Update(storedToken);

        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        var permissions = new List<string>(); // Fetch from DB if needed

        var accessTokenExpires = DateTime.UtcNow.AddMinutes(30);
        var newRefreshTokenExpires = DateTime.UtcNow.AddDays(7);

        var newAccessToken = GenerateAccessToken(user.Id, user.Username, roles, permissions, accessTokenExpires);
        var newRefreshToken = GenerateRefreshToken();

        var newToken = UserToken.Create(user.Id, newAccessToken, newRefreshToken, accessTokenExpires, newRefreshTokenExpires);
        _context.UserTokens.Add(newToken);
        await _context.SaveChangesAsync();

        return new RefreshTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = accessTokenExpires
        };
    }

    public async Task RevokeTokenAsync(string refreshToken)
    {
        var storedToken = _context.UserTokens
            .FirstOrDefault(t => t.RefreshToken == refreshToken && !t.IsRevoked);

        if (storedToken != null)
        {
            storedToken.Revoke();
            _context.UserTokens.Update(storedToken);
            await _context.SaveChangesAsync();
        }
    }

    private string GenerateAccessToken(Guid userId, string username, IList<string> roles, IList<string> permissions, DateTime expires)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(permissions.Select(perm => new Claim("permission", perm)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
