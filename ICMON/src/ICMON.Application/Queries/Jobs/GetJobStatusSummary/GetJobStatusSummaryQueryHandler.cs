using MediatR;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Queries.Jobs.GetJobStatusSummary;

public class GetJobStatusSummaryQueryHandler : IRequestHandler<GetJobStatusSummaryQuery, JobStatusSummary>
{
    private readonly IJobRepository _jobRepository;

    public GetJobStatusSummaryQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobStatusSummary> Handle(GetJobStatusSummaryQuery request, CancellationToken cancellationToken)
    {
        return await _jobRepository.GetStatusSummaryAsync(request.WhitelabelId, cancellationToken);
    }
}
