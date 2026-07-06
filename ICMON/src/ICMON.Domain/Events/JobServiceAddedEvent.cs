namespace ICMON.Domain.Events;

public record JobServiceAddedEvent(Guid JobId, Guid ServiceId, int Quantity) : BaseDomainEvent(JobId);
