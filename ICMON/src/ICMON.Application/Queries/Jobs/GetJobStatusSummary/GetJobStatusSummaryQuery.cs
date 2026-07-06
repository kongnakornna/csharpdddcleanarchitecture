using MediatR;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Queries.Jobs.GetJobStatusSummary;

public class GetJobStatusSummaryQuery : IRequest<JobStatusSummary>
{
    public Guid WhitelabelId { get; set; }
}
