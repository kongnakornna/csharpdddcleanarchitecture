using ICMON.Domain.ValueObjects;

namespace ICMON.Domain.Aggregates.JobAggregate;

public class JobPartSales : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public Guid PartId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalAmount { get; private set; }
    public string? Note { get; private set; }

    private JobPartSales() { }

    public static JobPartSales Create(Guid jobId, Guid partId, int quantity, Money unitPrice, string? note = null)
    {
        return new JobPartSales
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            PartId = partId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalAmount = new Money(unitPrice.Amount * quantity, unitPrice.Currency),
            Note = note
        };
    }
}
