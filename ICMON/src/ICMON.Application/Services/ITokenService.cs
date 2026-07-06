using ICMON.Application.DTOs.Auth;

namespace ICMON.Application.Services;

public interface ITokenService
{
    Task<LoginResponse> GenerateTokensAsync(Guid userId, string username, IList<string> roles, IList<string> permissions);
    Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken);
    Task RevokeTokenAsync(string refreshToken);
}
