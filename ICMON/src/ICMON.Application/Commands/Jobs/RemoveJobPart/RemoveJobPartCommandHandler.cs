using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Commands.Jobs.RemoveJobPart;

public class RemoveJobPartCommandHandler : IRequestHandler<RemoveJobPartCommand, Result<JobDetailDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveJobPartCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<JobDetailDto>> Handle(RemoveJobPartCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdWithDetailsAsync(request.JobId, cancellationToken);
        if (job == null)
            return Result<JobDetailDto>.Failure("Job not found");

        job.RemovePart(request.PartId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<JobDetailDto>.Success(_mapper.Map<JobDetailDto>(job));
    }
}
