using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;

namespace ICMON.Application.Commands.Jobs.RemoveJobPart;

public class RemoveJobPartCommand : IRequest<Result<JobDetailDto>>
{
    public Guid JobId { get; set; }
    public Guid PartId { get; set; }
}
