using AutoMapper;
using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Domain.ValueObjects;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = Job.Create(
            request.CustomerId,
            request.CarId,
            request.Description,
            Guid.NewGuid(),
            request.MechanicId
        );

        foreach (var symptom in request.Symptoms)
            job.AddSymptom(symptom.Description, symptom.Severity, symptom.Code);

        foreach (var service in request.Services)
            job.AddService(service.ServiceId, service.Quantity, new Money(service.UnitPrice), service.Note);

        foreach (var part in request.Parts)
            job.AddPart(part.PartId, part.Quantity, new Money(part.UnitPrice), part.Note);

        await _jobRepository.AddAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<JobDto>.Success(_mapper.Map<JobDto>(job));
    }
}
