using AutoMapper;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Aggregates.JobAggregate;

namespace ICMON.Application.Profiles;

public class JobMappingProfile : Profile
{
    public JobMappingProfile()
    {
        CreateMap<Job, JobDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<Job, JobDetailDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<JobService, JobServiceDto>();
        CreateMap<JobPartSales, JobPartDto>();
        CreateMap<JobStatusHistory, JobStatusHistoryDto>()
            .ForMember(d => d.FromStatus, o => o.MapFrom(s => s.FromStatus.ToString()))
            .ForMember(d => d.ToStatus, o => o.MapFrom(s => s.ToStatus.ToString()));
        CreateMap<JobSymptom, JobSymptomDto>();
        CreateMap<JobDiagnosticCode, JobDiagnosticCodeDto>();
    }
}
