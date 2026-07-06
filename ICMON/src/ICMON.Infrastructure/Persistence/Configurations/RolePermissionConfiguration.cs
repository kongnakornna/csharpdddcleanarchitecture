using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("m_role_permission");
        builder.HasKey(rp => rp.Id);
        builder.Property(rp => rp.RoleId).IsRequired();
        builder.Property(rp => rp.PermissionId).IsRequired();
        builder.HasIndex(rp => new { rp.RoleId, rp.PermissionId }).IsUnique();
        builder.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
        builder.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId);
    }
}
