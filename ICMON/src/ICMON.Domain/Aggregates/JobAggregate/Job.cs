using ICMON.Domain.Enums;
using ICMON.Domain.Events;
using ICMON.Domain.ValueObjects;

namespace ICMON.Domain.Aggregates.JobAggregate;

public class Job : AggregateRoot<Guid>
{
    public string JobNo { get; private set; } = null!;
    public Guid CustomerId { get; private set; }
    public Guid CarId { get; private set; }
    public Guid? MechanicId { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime ReceivedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public string Description { get; private set; } = null!;
    public Money TotalAmount { get; private set; } = null!;
    public Guid WhitelabelId { get; private set; }

    private readonly List<JobService> _services = new();
    public IReadOnlyList<JobService> Services => _services.AsReadOnly();

    private readonly List<JobPartSales> _parts = new();
    public IReadOnlyList<JobPartSales> Parts => _parts.AsReadOnly();

    private readonly List<JobStatusHistory> _statusHistory = new();
    public IReadOnlyList<JobStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private readonly List<JobSymptom> _symptoms = new();
    public IReadOnlyList<JobSymptom> Symptoms => _symptoms.AsReadOnly();

    private readonly List<JobDiagnosticCode> _diagnosticCodes = new();
    public IReadOnlyList<JobDiagnosticCode> DiagnosticCodes => _diagnosticCodes.AsReadOnly();

    private Job() { }

    public static Job Create(Guid customerId, Guid carId, string description, Guid whitelabelId, Guid? mechanicId = null)
    {
        var job = new Job
        {
            Id = Guid.NewGuid(),
            JobNo = GenerateJobNo(),
            CustomerId = customerId,
            CarId = carId,
            MechanicId = mechanicId,
            Status = JobStatus.Open,
            ReceivedDate = DateTime.UtcNow,
            Description = description,
            WhitelabelId = whitelabelId,
            TotalAmount = Money.Zero
        };

        job.AddDomainEvent(new JobCreatedEvent(job.Id, job.JobNo));
        return job;
    }

    public void ChangeStatus(JobStatus newStatus, string? note = null)
    {
        if (!CanTransitionTo(newStatus))
            throw new DomainException($"Cannot transition from {Status} to {newStatus}");

        var fromStatus = Status;
        Status = newStatus;
        _statusHistory.Add(JobStatusHistory.Create(Id, fromStatus, newStatus, note));

        if (newStatus == JobStatus.Closed)
            CompletedDate = DateTime.UtcNow;

        AddDomainEvent(new JobStatusChangedEvent(Id, newStatus));
    }

    public void AddService(Guid serviceId, int quantity, Money unitPrice, string? note = null)
    {
        var jobService = JobService.Create(Id, serviceId, quantity, unitPrice, note);
        _services.Add(jobService);
        RecalculateTotal();
        AddDomainEvent(new JobServiceAddedEvent(Id, serviceId, quantity));
    }

    public void AddPart(Guid partId, int quantity, Money unitPrice, string? note = null)
    {
        var jobPart = JobPartSales.Create(Id, partId, quantity, unitPrice, note);
        _parts.Add(jobPart);
        RecalculateTotal();
        AddDomainEvent(new JobPartAddedEvent(Id, partId, quantity));
    }

    public void AddSymptom(string description, string severity, string? code = null)
    {
        _symptoms.Add(JobSymptom.Create(Id, description, severity, code));
    }

    public void AddDiagnosticCode(string code, string description, string system)
    {
        _diagnosticCodes.Add(JobDiagnosticCode.Create(Id, code, description, system));
    }

    public void RemoveService(Guid serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service != null)
        {
            _services.Remove(service);
            RecalculateTotal();
        }
    }

    public void RemovePart(Guid partId)
    {
        var part = _parts.FirstOrDefault(p => p.Id == partId);
        if (part != null)
        {
            _parts.Remove(part);
            RecalculateTotal();
        }
    }

    private void RecalculateTotal()
    {
        var serviceTotal = _services.Sum(s => s.TotalAmount.Amount);
        var partTotal = _parts.Sum(p => p.TotalAmount.Amount);
        TotalAmount = new Money(serviceTotal + partTotal, TotalAmount?.Currency ?? "THB");
    }

    private static string GenerateJobNo()
    {
        var shortId = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
        return $"JOB-{DateTime.Now:yyyyMMdd}-{shortId}";
    }

    private bool CanTransitionTo(JobStatus newStatus)
    {
        return newStatus switch
        {
            JobStatus.InProgress => Status is JobStatus.Open or JobStatus.QuotationPending,
            JobStatus.QuotationPending => Status == JobStatus.InProgress,
            JobStatus.QuotationApproved => Status == JobStatus.QuotationPending,
            JobStatus.PartPicking => Status == JobStatus.QuotationApproved,
            JobStatus.RepairInProgress => Status is JobStatus.PartPicking or JobStatus.QuotationApproved,
            JobStatus.RepairDone => Status == JobStatus.RepairInProgress,
            JobStatus.InvoicePending => Status == JobStatus.RepairDone,
            JobStatus.InvoiceCreated => Status == JobStatus.InvoicePending,
            JobStatus.PaymentReceived => Status == JobStatus.InvoiceCreated,
            JobStatus.Closed => Status == JobStatus.PaymentReceived,
            _ => false
        };
    }
}
