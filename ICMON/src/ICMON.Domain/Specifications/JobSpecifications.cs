using System.Linq.Expressions;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Enums;

namespace ICMON.Domain.Specifications;

public static class JobSpecifications
{
    public static Expression<Func<Job, bool>> ByStatus(JobStatus status)
        => job => job.Status == status;

    public static Expression<Func<Job, bool>> ByCustomer(Guid customerId)
        => job => job.CustomerId == customerId;

    public static Expression<Func<Job, bool>> ByMechanic(Guid mechanicId)
        => job => job.MechanicId == mechanicId;

    public static Expression<Func<Job, bool>> ByDateRange(DateTime from, DateTime to)
        => job => job.ReceivedDate >= from && job.ReceivedDate <= to;

    public static Expression<Func<Job, bool>> ByWhitelabel(Guid whitelabelId)
        => job => job.WhitelabelId == whitelabelId;
}
