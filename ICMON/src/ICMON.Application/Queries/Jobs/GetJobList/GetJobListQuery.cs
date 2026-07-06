using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Enums;

namespace ICMON.Application.Queries.Jobs.GetJobList;

public class GetJobListQuery : IRequest<PagedResult<JobDto>>
{
    public JobStatus? Status { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? MechanicId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
