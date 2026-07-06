using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Entities;
using ICMON.Infrastructure.Persistence;

namespace ICMON.Infrastructure.Authentication;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionCode);
    Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId);
}

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _context;

    public PermissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionCode)
    {
        return await _context.Set<RolePermission>()
            .Where(rp => _context.Set<Domain.Entities.UserRole>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .Contains(rp.RoleId))
            .AnyAsync(rp => rp.Permission.Code == permissionCode);
    }

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId)
    {
        return await _context.Set<RolePermission>()
            .Where(rp => _context.Set<Domain.Entities.UserRole>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .Contains(rp.RoleId))
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToListAsync();
    }
}
