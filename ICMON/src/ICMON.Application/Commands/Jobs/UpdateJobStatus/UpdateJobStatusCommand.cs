using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;
using ICMON.Domain.Enums;

namespace ICMON.Application.Commands.Jobs.UpdateJobStatus;

public class UpdateJobStatusCommand : IRequest<Result<JobDto>>
{
    public Guid JobId { get; set; }
    public JobStatus NewStatus { get; set; }
    public string? Note { get; set; }
}
