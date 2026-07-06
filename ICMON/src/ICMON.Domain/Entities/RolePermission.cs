namespace ICMON.Domain.Entities;

public class RolePermission : Entity<Guid>
{
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }

    public virtual Role Role { get; private set; } = null!;
    public virtual Permission Permission { get; private set; } = null!;

    private RolePermission() { }

    public static RolePermission Create(Guid roleId, Guid permissionId)
    {
        return new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = roleId,
            PermissionId = permissionId
        };
    }
}
