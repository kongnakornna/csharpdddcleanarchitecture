namespace ICMON.Application.DTOs.Jobs;

public class JobDetailDto
{
    public Guid Id { get; set; }
    public string JobNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public Guid CarId { get; set; }
    public string? LicensePlate { get; set; }
    public string? CarModel { get; set; }
    public Guid? MechanicId { get; set; }
    public string? MechanicName { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "THB";

    public List<JobServiceDto> Services { get; set; } = new();
    public List<JobPartDto> Parts { get; set; } = new();
    public List<JobStatusHistoryDto> StatusHistory { get; set; } = new();
    public List<JobSymptomDto> Symptoms { get; set; } = new();
    public List<JobDiagnosticCodeDto> DiagnosticCodes { get; set; } = new();
}

public class JobServiceDto
{
    public Guid Id { get; set; }
    public string? ServiceName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
}

public class JobPartDto
{
    public Guid Id { get; set; }
    public string? PartName { get; set; }
    public string? PartCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
}

public class JobStatusHistoryDto
{
    public Guid Id { get; set; }
    public string FromStatus { get; set; } = string.Empty;
    public string ToStatus { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime ChangedAt { get; set; }
}

public class JobSymptomDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string? Code { get; set; }
}

public class JobDiagnosticCodeDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string System { get; set; } = string.Empty;
}
