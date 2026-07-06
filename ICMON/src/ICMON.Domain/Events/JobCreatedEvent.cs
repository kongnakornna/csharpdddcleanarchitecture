namespace ICMON.Domain.Events;

public record JobCreatedEvent(Guid JobId, string JobNo) : BaseDomainEvent(JobId);
