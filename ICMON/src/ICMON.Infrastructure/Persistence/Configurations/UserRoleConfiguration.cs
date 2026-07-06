using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("m_user_role");
        builder.HasKey(ur => ur.Id);
        builder.Property(ur => ur.UserId).IsRequired();
        builder.Property(ur => ur.RoleId).IsRequired();
        builder.Property(ur => ur.AssignedAt).IsRequired();
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
        builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
        builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
    }
}
