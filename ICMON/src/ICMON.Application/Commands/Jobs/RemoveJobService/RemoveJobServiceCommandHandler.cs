using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Commands.Jobs.RemoveJobService;

public class RemoveJobServiceCommandHandler : IRequestHandler<RemoveJobServiceCommand, Result<JobDetailDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveJobServiceCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<JobDetailDto>> Handle(RemoveJobServiceCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdWithDetailsAsync(request.JobId, cancellationToken);
        if (job == null)
            return Result<JobDetailDto>.Failure("Job not found");

        job.RemoveService(request.ServiceId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<JobDetailDto>.Success(_mapper.Map<JobDetailDto>(job));
    }
}
