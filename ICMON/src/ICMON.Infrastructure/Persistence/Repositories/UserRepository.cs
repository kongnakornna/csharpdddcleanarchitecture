using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Entities;
using ICMON.Domain.Interfaces;

namespace ICMON.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email.Value == email.ToLowerInvariant(), cancellationToken);

    public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(
                u => u.Username == usernameOrEmail || u.Email.Value == usernameOrEmail.ToLowerInvariant(),
                cancellationToken);

    public async Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<bool> IsUsernameTakenAsync(string username, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(u => u.Username == username, cancellationToken);

    public async Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(u => u.Email.Value == email.ToLowerInvariant(), cancellationToken);

    public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<RolePermission>()
            .Where(rp => _context.Set<UserRole>()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .Contains(rp.RoleId))
            .Select(rp => rp.Permission)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
