using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Enums;
using ICMON.Domain.Interfaces;

namespace ICMON.Infrastructure.Persistence.Repositories;

public class JobRepository : GenericRepository<Job>, IJobRepository
{
    public JobRepository(AppDbContext context) : base(context) { }

    public async Task<Job?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include(j => j.Services)
            .Include(j => j.Parts)
            .Include(j => j.StatusHistory)
            .Include(j => j.Symptoms)
            .Include(j => j.DiagnosticCodes)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);

    public async Task<IEnumerable<Job>> GetByStatusAsync(JobStatus status, CancellationToken cancellationToken = default)
        => await _dbSet.Where(j => j.Status == status).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Job>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        => await _dbSet.Where(j => j.CustomerId == customerId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Job>> GetByMechanicAsync(Guid mechanicId, CancellationToken cancellationToken = default)
        => await _dbSet.Where(j => j.MechanicId == mechanicId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Job>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        => await _dbSet.Where(j => j.ReceivedDate >= fromDate && j.ReceivedDate <= toDate).ToListAsync(cancellationToken);

    public async Task<JobStatusSummary> GetStatusSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var jobs = await _dbSet.Where(j => j.WhitelabelId == whitelabelId).ToListAsync(cancellationToken);
        return new JobStatusSummary
        {
            Open = jobs.Count(j => j.Status == JobStatus.Open),
            InProgress = jobs.Count(j => j.Status == JobStatus.InProgress),
            QuotationPending = jobs.Count(j => j.Status == JobStatus.QuotationPending),
            QuotationApproved = jobs.Count(j => j.Status == JobStatus.QuotationApproved),
            PartPicking = jobs.Count(j => j.Status == JobStatus.PartPicking),
            RepairInProgress = jobs.Count(j => j.Status == JobStatus.RepairInProgress),
            RepairDone = jobs.Count(j => j.Status == JobStatus.RepairDone),
            InvoicePending = jobs.Count(j => j.Status == JobStatus.InvoicePending),
            InvoiceCreated = jobs.Count(j => j.Status == JobStatus.InvoiceCreated),
            PaymentReceived = jobs.Count(j => j.Status == JobStatus.PaymentReceived),
            Closed = jobs.Count(j => j.Status == JobStatus.Closed)
        };
    }

    public async Task<IEnumerable<Job>> GetFilteredAsync(JobStatus? status, Guid? customerId, Guid? mechanicId,
        DateTime? fromDate, DateTime? toDate, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = BuildFilterQuery(status, customerId, mechanicId, fromDate, toDate);
        return await query
            .OrderByDescending(j => j.ReceivedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetFilteredCountAsync(JobStatus? status, Guid? customerId, Guid? mechanicId,
        DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken = default)
    {
        var query = BuildFilterQuery(status, customerId, mechanicId, fromDate, toDate);
        return await query.CountAsync(cancellationToken);
    }

    private IQueryable<Job> BuildFilterQuery(JobStatus? status, Guid? customerId, Guid? mechanicId,
        DateTime? fromDate, DateTime? toDate)
    {
        var query = _dbSet.AsQueryable();

        if (status.HasValue)
            query = query.Where(j => j.Status == status.Value);
        if (customerId.HasValue)
            query = query.Where(j => j.CustomerId == customerId.Value);
        if (mechanicId.HasValue)
            query = query.Where(j => j.MechanicId == mechanicId.Value);
        if (fromDate.HasValue)
            query = query.Where(j => j.ReceivedDate >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(j => j.ReceivedDate <= toDate.Value);

        return query;
    }
}
