using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("m_user");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).HasMaxLength(50).IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(200).IsRequired();
        });
        builder.HasIndex("Email").IsUnique();
        builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(u => u.WhitelabelId).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.LastLoginAt);
        builder.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId);
        builder.HasMany(u => u.Tokens).WithOne(t => t.User).HasForeignKey(t => t.UserId);
    }
}
