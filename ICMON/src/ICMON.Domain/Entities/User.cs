using ICMON.Domain.Enums;
using ICMON.Domain.ValueObjects;

namespace ICMON.Domain.Entities;

public class User : AggregateRoot<Guid>
{
    public string Username { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public UserStatus Status { get; private set; }
    public Guid WhitelabelId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    private readonly List<UserToken> _tokens = new();
    public IReadOnlyList<UserToken> Tokens => _tokens.AsReadOnly();

    private User() { }

    public static User Create(string username, Email email, string passwordHash, string firstName, string lastName, Guid whitelabelId)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            Status = UserStatus.Active,
            WhitelabelId = whitelabelId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProfile(string firstName, string lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void UpdateStatus(UserStatus newStatus)
    {
        Status = newStatus;
    }

    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void AssignRole(Guid roleId)
    {
        if (!_userRoles.Any(ur => ur.RoleId == roleId))
        {
            _userRoles.Add(UserRole.Create(Id, roleId));
        }
    }

    public void RemoveRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole != null)
        {
            _userRoles.Remove(userRole);
        }
    }

    public bool HasRole(Guid roleId) => _userRoles.Any(ur => ur.RoleId == roleId);
}
