using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICMON.Domain.Entities;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("m_user_token");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.UserId).IsRequired();
        builder.Property(t => t.AccessToken).HasMaxLength(2000).IsRequired();
        builder.Property(t => t.RefreshToken).HasMaxLength(500).IsRequired();
        builder.HasIndex(t => t.RefreshToken).IsUnique();
        builder.Property(t => t.AccessTokenExpiresAt).IsRequired();
        builder.Property(t => t.RefreshTokenExpiresAt).IsRequired();
        builder.Property(t => t.IsRevoked).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.RevokedAt);
        builder.HasOne(t => t.User).WithMany(u => u.Tokens).HasForeignKey(t => t.UserId);
    }
}
