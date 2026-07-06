using ICMON.Domain.ValueObjects;

namespace ICMON.Domain.Aggregates.JobAggregate;

public class JobService : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public Guid ServiceId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalAmount { get; private set; }
    public string? Note { get; private set; }

    private JobService() { }

    public static JobService Create(Guid jobId, Guid serviceId, int quantity, Money unitPrice, string? note = null)
    {
        return new JobService
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            ServiceId = serviceId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalAmount = new Money(unitPrice.Amount * quantity, unitPrice.Currency),
            Note = note
        };
    }
}
