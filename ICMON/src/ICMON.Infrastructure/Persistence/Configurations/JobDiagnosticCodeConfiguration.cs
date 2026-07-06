using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobDiagnosticCodeConfiguration : IEntityTypeConfiguration<JobDiagnosticCode>
{
    public void Configure(EntityTypeBuilder<JobDiagnosticCode> builder)
    {
        builder.ToTable("JobDiagnosticCodes");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Code).HasMaxLength(50).IsRequired();
        builder.Property(d => d.Description).HasMaxLength(500).IsRequired();
        builder.Property(d => d.System).HasMaxLength(50).IsRequired();
    }
}
