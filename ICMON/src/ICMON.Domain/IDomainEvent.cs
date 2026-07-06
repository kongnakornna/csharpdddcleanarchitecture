namespace ICMON.Domain;

public interface IDomainEvent
{
    Guid AggregateId { get; }
    DateTime OccurredOn { get; }
}
