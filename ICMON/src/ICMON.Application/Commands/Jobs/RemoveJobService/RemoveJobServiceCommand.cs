using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;

namespace ICMON.Application.Commands.Jobs.RemoveJobService;

public class RemoveJobServiceCommand : IRequest<Result<JobDetailDto>>
{
    public Guid JobId { get; set; }
    public Guid ServiceId { get; set; }
}
