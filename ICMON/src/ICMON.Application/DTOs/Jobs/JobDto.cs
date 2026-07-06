namespace ICMON.Application.DTOs.Jobs;

public class JobDto
{
    public Guid Id { get; set; }
    public string JobNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public Guid CarId { get; set; }
    public string? LicensePlate { get; set; }
    public Guid? MechanicId { get; set; }
    public string? MechanicName { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ReceivedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "THB";
}
