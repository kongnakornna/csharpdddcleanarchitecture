using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("m_permission");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Code).HasMaxLength(50).IsRequired();
        builder.HasIndex(p => p.Code).IsUnique();
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(200);
        builder.Property(p => p.GroupName).HasMaxLength(50).IsRequired();
    }
}
