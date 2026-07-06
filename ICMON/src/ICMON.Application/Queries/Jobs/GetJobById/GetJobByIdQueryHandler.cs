using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Queries.Jobs.GetJobById;

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Result<JobDetailDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public GetJobByIdQueryHandler(IJobRepository jobRepository, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<Result<JobDetailDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (job == null)
            return Result<JobDetailDto>.Failure("Job not found");

        return Result<JobDetailDto>.Success(_mapper.Map<JobDetailDto>(job));
    }
}
