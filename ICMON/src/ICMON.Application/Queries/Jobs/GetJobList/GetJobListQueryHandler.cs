using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Queries.Jobs.GetJobList;

public class GetJobListQueryHandler : IRequestHandler<GetJobListQuery, PagedResult<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public GetJobListQueryHandler(IJobRepository jobRepository, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<JobDto>> Handle(GetJobListQuery request, CancellationToken cancellationToken)
    {
        var items = await _jobRepository.GetFilteredAsync(
            request.Status, request.CustomerId, request.MechanicId,
            request.FromDate, request.ToDate,
            request.Page, request.PageSize, cancellationToken);

        var totalCount = await _jobRepository.GetFilteredCountAsync(
            request.Status, request.CustomerId, request.MechanicId,
            request.FromDate, request.ToDate, cancellationToken);

        return PagedResult<JobDto>.Create(
            _mapper.Map<List<JobDto>>(items),
            totalCount,
            request.Page,
            request.PageSize
        );
    }
}
