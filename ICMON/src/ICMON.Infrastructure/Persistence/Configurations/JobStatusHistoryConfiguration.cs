using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobStatusHistoryConfiguration : IEntityTypeConfiguration<JobStatusHistory>
{
    public void Configure(EntityTypeBuilder<JobStatusHistory> builder)
    {
        builder.ToTable("JobStatusHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.FromStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.ToStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.Note).HasMaxLength(500);
        builder.Property(h => h.ChangedAt).IsRequired();
    }
}
