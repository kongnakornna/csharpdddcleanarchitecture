using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Enums;

namespace ICMON.Domain.Interfaces;

public class JobStatusSummary
{
    public int Open { get; set; }
    public int InProgress { get; set; }
    public int QuotationPending { get; set; }
    public int QuotationApproved { get; set; }
    public int PartPicking { get; set; }
    public int RepairInProgress { get; set; }
    public int RepairDone { get; set; }
    public int InvoicePending { get; set; }
    public int InvoiceCreated { get; set; }
    public int PaymentReceived { get; set; }
    public int Closed { get; set; }
    public int Total => Open + InProgress + QuotationPending + QuotationApproved + PartPicking
                        + RepairInProgress + RepairDone + InvoicePending + InvoiceCreated
                        + PaymentReceived + Closed;
}

public interface IJobRepository : IRepository<Job>
{
    Task<Job?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Job>> GetByStatusAsync(JobStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Job>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Job>> GetByMechanicAsync(Guid mechanicId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Job>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<JobStatusSummary> GetStatusSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Job>> GetFilteredAsync(JobStatus? status, Guid? customerId, Guid? mechanicId,
        DateTime? fromDate, DateTime? toDate, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<int> GetFilteredCountAsync(JobStatus? status, Guid? customerId, Guid? mechanicId,
        DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken = default);
}
