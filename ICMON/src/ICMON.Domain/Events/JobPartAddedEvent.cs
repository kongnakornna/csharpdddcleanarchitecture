namespace ICMON.Domain.Events;

public record JobPartAddedEvent(Guid JobId, Guid PartId, int Quantity) : BaseDomainEvent(JobId);
