using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobServiceConfiguration : IEntityTypeConfiguration<JobService>
{
    public void Configure(EntityTypeBuilder<JobService> builder)
    {
        builder.ToTable("JobServices");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Quantity).IsRequired();
        builder.Property(s => s.Note).HasMaxLength(500);

        builder.OwnsOne(s => s.UnitPrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("UnitPriceAmount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(10);
        });

        builder.OwnsOne(s => s.TotalAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("TotalAmountValue").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("TotalAmountCurrency").HasMaxLength(10);
        });
    }
}
