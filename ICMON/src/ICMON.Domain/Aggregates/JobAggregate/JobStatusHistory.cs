using ICMON.Domain.Enums;

namespace ICMON.Domain.Aggregates.JobAggregate;

public class JobStatusHistory : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public JobStatus FromStatus { get; private set; }
    public JobStatus ToStatus { get; private set; }
    public string? Note { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public Guid ChangedByUserId { get; private set; }

    private JobStatusHistory() { }

    public static JobStatusHistory Create(Guid jobId, JobStatus fromStatus, JobStatus toStatus, string? note = null)
    {
        return new JobStatusHistory
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            FromStatus = fromStatus,
            ToStatus = toStatus,
            Note = note,
            ChangedAt = DateTime.UtcNow
        };
    }
}
