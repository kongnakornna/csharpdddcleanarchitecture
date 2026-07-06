using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Interfaces;
using ICMON.Domain.ValueObjects;

namespace ICMON.Application.Commands.Jobs.AddJobService;

public class AddJobServiceCommandHandler : IRequestHandler<AddJobServiceCommand, Result<JobDetailDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddJobServiceCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<JobDetailDto>> Handle(AddJobServiceCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdWithDetailsAsync(request.JobId, cancellationToken);
        if (job == null)
            return Result<JobDetailDto>.Failure("Job not found");

        job.AddService(request.ServiceId, request.Quantity, new Money(request.UnitPrice), request.Note);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<JobDetailDto>.Success(_mapper.Map<JobDetailDto>(job));
    }
}
