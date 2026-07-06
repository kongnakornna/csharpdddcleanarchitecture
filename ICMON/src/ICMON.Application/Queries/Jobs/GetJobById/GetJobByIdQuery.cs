using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;

namespace ICMON.Application.Queries.Jobs.GetJobById;

public class GetJobByIdQuery : IRequest<Result<JobDetailDto>>
{
    public Guid Id { get; set; }
}
