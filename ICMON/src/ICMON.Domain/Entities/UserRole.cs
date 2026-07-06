namespace ICMON.Domain.Entities;

public class UserRole : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime AssignedAt { get; private set; }

    public virtual User User { get; private set; } = null!;
    public virtual Role Role { get; private set; } = null!;

    private UserRole() { }

    public static UserRole Create(Guid userId, Guid roleId)
    {
        return new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RoleId = roleId,
            AssignedAt = DateTime.UtcNow
        };
    }
}
