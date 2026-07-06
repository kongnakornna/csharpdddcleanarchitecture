namespace ICMON.Domain.Aggregates.JobAggregate;

public class JobDiagnosticCode : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public string Code { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string System { get; private set; } = null!;

    private JobDiagnosticCode() { }

    public static JobDiagnosticCode Create(Guid jobId, string code, string description, string system)
    {
        return new JobDiagnosticCode
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            Code = code,
            Description = description,
            System = system
        };
    }
}
