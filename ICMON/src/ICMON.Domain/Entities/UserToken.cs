namespace ICMON.Domain.Entities;

public class UserToken : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime AccessTokenExpiresAt { get; private set; }
    public DateTime RefreshTokenExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public virtual User User { get; private set; }

    private UserToken() { }

    public static UserToken Create(Guid userId, string accessToken, string refreshToken, DateTime accessTokenExpiresAt, DateTime refreshTokenExpiresAt)
    {
        return new UserToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshTokenExpiresAt = refreshTokenExpiresAt,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }

    public bool IsAccessTokenExpired => DateTime.UtcNow >= AccessTokenExpiresAt;
    public bool IsRefreshTokenExpired => DateTime.UtcNow >= RefreshTokenExpiresAt;
}
