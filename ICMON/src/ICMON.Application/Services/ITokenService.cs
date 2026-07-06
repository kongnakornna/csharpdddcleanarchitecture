using ICMON.Application.DTOs.Auth;
using ICMON.Domain.Entities;

namespace ICMON.Application.Services;

public interface ITokenService
{
    Task<LoginResponse> GenerateTokensAsync(User user, IList<string> roles, IList<string> permissions);
    Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken);
    Task RevokeTokenAsync(string refreshToken);
}
