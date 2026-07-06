using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;

namespace ICMON.Application.Commands.Jobs.AddJobService;

public class AddJobServiceCommand : IRequest<Result<JobDetailDto>>
{
    public Guid JobId { get; set; }
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}
