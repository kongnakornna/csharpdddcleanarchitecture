namespace ICMON.Application.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Username { get; }
    Guid WhitelabelId { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    IEnumerable<string> Permissions { get; }
}
