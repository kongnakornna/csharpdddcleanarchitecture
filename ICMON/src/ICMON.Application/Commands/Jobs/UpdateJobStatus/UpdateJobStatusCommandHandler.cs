using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;

namespace ICMON.Application.Commands.Jobs.UpdateJobStatus;

public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, Result<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateJobStatusCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<JobDto>> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId, cancellationToken);
        if (job == null)
            return Result<JobDto>.Failure("Job not found");

        job.ChangeStatus(request.NewStatus, request.Note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<JobDto>.Success(_mapper.Map<JobDto>(job));
    }
}
