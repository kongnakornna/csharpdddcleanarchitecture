using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Enums;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.JobNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(j => j.JobNo).IsUnique();

        builder.Property(j => j.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(j => j.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(j => j.TotalAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("TotalAmount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(10);
        });

        builder.Property(j => j.ReceivedDate)
            .IsRequired();

        builder.Property(j => j.CompletedDate);

        builder.HasMany(j => j.Services)
            .WithOne()
            .HasForeignKey(s => s.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.Parts)
            .WithOne()
            .HasForeignKey(p => p.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.StatusHistory)
            .WithOne()
            .HasForeignKey(h => h.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.Symptoms)
            .WithOne()
            .HasForeignKey(s => s.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.DiagnosticCodes)
            .WithOne()
            .HasForeignKey(d => d.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(j => j.DomainEvents);
    }
}
