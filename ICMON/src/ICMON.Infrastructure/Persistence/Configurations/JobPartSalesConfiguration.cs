using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Infrastructure.Persistence.Configurations;

public class JobPartSalesConfiguration : IEntityTypeConfiguration<JobPartSales>
{
    public void Configure(EntityTypeBuilder<JobPartSales> builder)
    {
        builder.ToTable("JobPartSales");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.Note).HasMaxLength(500);

        builder.OwnsOne(p => p.UnitPrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("UnitPriceAmount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(10);
        });

        builder.OwnsOne(p => p.TotalAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("TotalAmountValue").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("TotalAmountCurrency").HasMaxLength(10);
        });
    }
}
