using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ICMON.Application.Services;

namespace ICMON.Infrastructure.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null && Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }
    }

    public string? Username
        => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

    public Guid WhitelabelId
    {
        get
        {
            var whitelabelClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("whitelabelId")?.Value;
            return whitelabelClaim != null && Guid.TryParse(whitelabelClaim, out var whitelabelId) ? whitelabelId : Guid.Empty;
        }
    }

    public bool IsAuthenticated
        => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Roles
        => _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value) ?? Enumerable.Empty<string>();

    public IEnumerable<string> Permissions
        => _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value) ?? Enumerable.Empty<string>();
}
