using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobSymptomConfiguration : IEntityTypeConfiguration<JobSymptom>
{
    public void Configure(EntityTypeBuilder<JobSymptom> builder)
    {
        builder.ToTable("JobSymptoms");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Description).HasMaxLength(500).IsRequired();
        builder.Property(s => s.Severity).HasMaxLength(50).IsRequired();
        builder.Property(s => s.Code).HasMaxLength(50);
    }
}
