using ICMON.Domain.Enums;

namespace ICMON.Domain.Events;

public record JobStatusChangedEvent(Guid JobId, JobStatus NewStatus) : BaseDomainEvent(JobId);
