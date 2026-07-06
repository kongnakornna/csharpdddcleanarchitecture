using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("m_role");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.Property(r => r.Description).HasMaxLength(200);
        builder.HasMany(r => r.RolePermissions).WithOne(rp => rp.Role).HasForeignKey(rp => rp.RoleId);
    }
}
