namespace ICMON.Domain;

public abstract record BaseDomainEvent(Guid AggregateId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
