namespace ICMON.Domain.Aggregates.JobAggregate;

public class JobSymptom : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public string Description { get; private set; } = null!;
    public string Severity { get; private set; } = null!;
    public string? Code { get; private set; }

    private JobSymptom() { }

    public static JobSymptom Create(Guid jobId, string description, string severity, string? code = null)
    {
        return new JobSymptom
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            Description = description,
            Severity = severity,
            Code = code
        };
    }
}
