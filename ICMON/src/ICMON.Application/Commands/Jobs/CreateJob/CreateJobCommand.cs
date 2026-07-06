using MediatR;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Jobs;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommand : IRequest<Result<JobDto>>
{
    public Guid CustomerId { get; set; }
    public Guid CarId { get; set; }
    public Guid? MechanicId { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<SymptomItem> Symptoms { get; set; } = new();
    public List<ServiceItem> Services { get; set; } = new();
    public List<PartItem> Parts { get; set; } = new();
}

public class SymptomItem
{
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = "Medium";
    public string? Code { get; set; }
}

public class ServiceItem
{
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}

public class PartItem
{
    public Guid PartId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string? Note { get; set; }
}
