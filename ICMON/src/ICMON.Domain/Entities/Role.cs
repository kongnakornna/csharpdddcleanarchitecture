namespace ICMON.Domain.Entities;

public class Role : Entity<Guid>
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    private Role() { }

    public static Role Create(string name, string? description = null)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void AssignPermission(Guid permissionId)
    {
        if (!_rolePermissions.Any(rp => rp.PermissionId == permissionId))
        {
            _rolePermissions.Add(RolePermission.Create(Id, permissionId));
        }
    }

    public void RemovePermission(Guid permissionId)
    {
        var rolePermission = _rolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
        if (rolePermission != null)
        {
            _rolePermissions.Remove(rolePermission);
        }
    }

    public bool HasPermission(Guid permissionId) => _rolePermissions.Any(rp => rp.PermissionId == permissionId);
}
