namespace ICMON.Domain.Entities;

public class Permission : Entity<Guid>
{
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public string GroupName { get; private set; } = null!;

    private Permission() { }

    public static Permission Create(string code, string name, string groupName, string? description = null)
    {
        return new Permission
        {
            Id = Guid.NewGuid(),
            Code = code,
            Name = name,
            GroupName = groupName,
            Description = description
        };
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}
