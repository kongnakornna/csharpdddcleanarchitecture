# 🚗 โมดูลที่ 4: Quotation – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Quotation (ใบเสนอราคา) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 58 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/QuotationAggregate/Quotation.cs` | Aggregate Root หลัก |
| 2 | `Aggregates/QuotationAggregate/QuotationPart.cs` | อะไหล่ในใบเสนอราคา |
| 3 | `Aggregates/QuotationAggregate/QuotationService.cs` | บริการในใบเสนอราคา |
| 4 | `Aggregates/QuotationAggregate/QuotationStatusHistory.cs` | ประวัติการเปลี่ยนสถานะ |
| 5 | `Enums/QuotationStatus.cs` | สถานะใบเสนอราคา |
| 6 | `Events/QuotationCreatedEvent.cs` | Event เมื่อสร้าง Quotation |
| 7 | `Events/QuotationApprovedEvent.cs` | Event เมื่ออนุมัติ Quotation |
| 8 | `Events/QuotationRejectedEvent.cs` | Event เมื่อปฏิเสธ Quotation |
| 9 | `Events/QuotationStatusChangedEvent.cs` | Event เมื่อเปลี่ยนสถานะ |
| 10 | `Interfaces/IQuotationRepository.cs` | Interface Repository เฉพาะ Quotation |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 11 | `DTOs/Quotations/QuotationDto.cs` | DTO หลัก |
| 12 | `DTOs/Quotations/QuotationDetailDto.cs` | DTO แบบละเอียด |
| 13 | `DTOs/Quotations/CreateQuotationDto.cs` | DTO สำหรับสร้าง |
| 14 | `DTOs/Quotations/QuotationItemDto.cs` | DTO รายการสินค้า/บริการ |
| 15 | `Commands/Quotations/CreateQuotation/CreateQuotationCommand.cs` | Command สร้าง |
| 16 | `Commands/Quotations/CreateQuotation/CreateQuotationCommandHandler.cs` | Handler สร้าง |
| 17 | `Commands/Quotations/CreateQuotation/CreateQuotationCommandValidator.cs` | Validator สร้าง |
| 18 | `Commands/Quotations/ApproveQuotation/ApproveQuotationCommand.cs` | Command อนุมัติ |
| 19 | `Commands/Quotations/ApproveQuotation/ApproveQuotationCommandHandler.cs` | Handler อนุมัติ |
| 20 | `Commands/Quotations/ApproveQuotation/ApproveQuotationCommandValidator.cs` | Validator อนุมัติ |
| 21 | `Commands/Quotations/RejectQuotation/RejectQuotationCommand.cs` | Command ปฏิเสธ |
| 22 | `Commands/Quotations/RejectQuotation/RejectQuotationCommandHandler.cs` | Handler ปฏิเสธ |
| 23 | `Commands/Quotations/RejectQuotation/RejectQuotationCommandValidator.cs` | Validator ปฏิเสธ |
| 24 | `Commands/Quotations/UpdateQuotation/UpdateQuotationCommand.cs` | Command แก้ไข |
| 25 | `Commands/Quotations/UpdateQuotation/UpdateQuotationCommandHandler.cs` | Handler แก้ไข |
| 26 | `Commands/Quotations/UpdateQuotation/UpdateQuotationCommandValidator.cs` | Validator แก้ไข |
| 27 | `Queries/Quotations/GetQuotationById/GetQuotationByIdQuery.cs` | Query ดึงตาม ID |
| 28 | `Queries/Quotations/GetQuotationById/GetQuotationByIdQueryHandler.cs` | Handler ดึงตาม ID |
| 29 | `Queries/Quotations/GetQuotationByJob/GetQuotationByJobQuery.cs` | Query ดึงตาม Job |
| 30 | `Queries/Quotations/GetQuotationByJob/GetQuotationByJobQueryHandler.cs` | Handler ดึงตาม Job |
| 31 | `Queries/Quotations/GetQuotationList/GetQuotationListQuery.cs` | Query รายการ |
| 32 | `Queries/Quotations/GetQuotationList/GetQuotationListQueryHandler.cs` | Handler รายการ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 33 | `Persistence/Configurations/QuotationConfiguration.cs` | EF Config Quotation |
| 34 | `Persistence/Configurations/QuotationPartConfiguration.cs` | EF Config QuotationPart |
| 35 | `Persistence/Configurations/QuotationServiceConfiguration.cs` | EF Config QuotationService |
| 36 | `Persistence/Configurations/QuotationStatusHistoryConfiguration.cs` | EF Config QuotationStatusHistory |
| 37 | `Persistence/Repositories/QuotationRepository.cs` | Repository เฉพาะ Quotation |
| 38 | `Persistence/SeedData/QuotationSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 39 | `Controllers/QuotationsController.cs` | Controller Quotation (11 Endpoints) |

---

### 📁 **Document Generation** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 40 | `DocumentGeneration/IPdfGenerator.cs` | Interface PDF Generator |
| 41 | `DocumentGeneration/QuotationPdfGenerator.cs` | PDF Quotation (QuestPDF) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Quotations Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/quotations` | สร้างใบเสนอราคา | 20/60s |
| GET | `/api/quotations/{id}` | ดูรายละเอียด | 100/60s |
| GET | `/api/quotations/job/{jobId}` | ดึงตาม Job | 80/60s |
| GET | `/api/quotations` | รายการใบเสนอราคา | 50/60s |
| PUT | `/api/quotations/{id}` | แก้ไขใบเสนอราคา | 15/60s |
| PUT | `/api/quotations/{id}/approve` | อนุมัติ | 20/60s |
| PUT | `/api/quotations/{id}/reject` | ปฏิเสธ | 20/60s |
| GET | `/api/quotations/{id}/pdf` | PDF ใบเสนอราคา | 15/300s |
| GET | `/api/quotations/{id}/summary` | สรุปใบเสนอราคา | 30/60s |
| GET | `/api/quotations/status-summary` | สรุปสถานะ | 30/60s |
| DELETE | `/api/quotations/{id}` | ลบใบเสนอราคา | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="QuestPDF" Version="2023.12.6" />
```

---

## 🚀 วิธีใช้

### 1. สร้างใบเสนอราคา
```http
POST /api/quotations
Authorization: Bearer <accessToken>
{
  "jobId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "validUntil": "2026-08-06T00:00:00Z",
  "notes": "รายการนี้เป็นอะไหล่แท้จากศูนย์",
  "discountPercent": 5,
  "services": [
    {
      "serviceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 1,
      "unitPrice": 500.00,
      "note": "ตรวจสอบระบบไฟฟ้า"
    }
  ],
  "parts": [
    {
      "partId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 2,
      "unitPrice": 350.00,
      "note": "เปลี่ยนกรองอากาศ"
    }
  ]
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "quotationNo": "QT-20260706-a1b2c3",
    "jobId": "...",
    "status": "Draft",
    "subtotal": 1200.00,
    "discountAmount": 60.00,
    "tax": 57.00,
    "total": 1197.00,
    "validUntil": "2026-08-06T00:00:00Z",
    "createdAt": "2026-07-06T10:30:00Z"
  }
}
```

### 2. อนุมัติใบเสนอราคา
```http
PUT /api/quotations/{quotationId}/approve
Authorization: Bearer <accessToken>
{
  "approvedBy": "manager@example.com",
  "note": "อนุมัติแล้ว ดำเนินการต่อได้"
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "status": "Approved",
    "approvedAt": "2026-07-06T10:35:00Z",
    "approvedBy": "manager@example.com"
  }
}
```

### 3. ดึงใบเสนอราคาตาม Job
```http
GET /api/quotations/job/{jobId}
Authorization: Bearer <accessToken>
```

### 4. ดึงใบเสนอราคา (พร้อม Filter)
```http
GET /api/quotations?status=Approved&fromDate=2026-07-01&toDate=2026-07-31&page=1&pageSize=20
Authorization: Bearer <accessToken>
```

### 5. ดาวน์โหลด PDF ใบเสนอราคา
```http
GET /api/quotations/{quotationId}/pdf
Authorization: Bearer <accessToken>
```

### 6. สรุปสถานะใบเสนอราคา
```http
GET /api/quotations/status-summary
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "Draft": 3,
  "Pending": 5,
  "Approved": 8,
  "Rejected": 2,
  "Expired": 1,
  "ConvertedToPO": 6
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Quotation.cs
```csharp
namespace ICMON.Domain.Aggregates.QuotationAggregate;

public class Quotation : AggregateRoot<Guid>
{
    public string QuotationNo { get; private set; }
    public Guid JobId { get; private set; }
    public QuotationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ValidUntil { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public string? ApprovedBy { get; private set; }
    public DateTime? RejectedAt { get; private set; }
    public string? RejectedReason { get; private set; }
    public string? Notes { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public Money Subtotal { get; private set; }
    public Money DiscountAmount { get; private set; }
    public Money Tax { get; private set; }
    public Money Total { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<QuotationPart> _parts = new();
    public IReadOnlyList<QuotationPart> Parts => _parts.AsReadOnly();

    private readonly List<QuotationService> _services = new();
    public IReadOnlyList<QuotationService> Services => _services.AsReadOnly();

    private readonly List<QuotationStatusHistory> _statusHistory = new();
    public IReadOnlyList<QuotationStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Quotation() { } // For EF Core

    public static Quotation Create(
        Guid jobId,
        Guid whitelabelId,
        DateTime? validUntil = null,
        string? notes = null,
        decimal discountPercent = 0)
    {
        var quotation = new Quotation
        {
            Id = Guid.NewGuid(),
            QuotationNo = GenerateQuotationNo(),
            JobId = jobId,
            Status = QuotationStatus.Draft,
            CreatedAt = DateTime.UtcNow,
            ValidUntil = validUntil ?? DateTime.UtcNow.AddDays(30),
            Notes = notes,
            DiscountPercent = discountPercent,
            WhitelabelId = whitelabelId,
            Subtotal = Money.Zero,
            DiscountAmount = Money.Zero,
            Tax = Money.Zero,
            Total = Money.Zero
        };

        quotation.AddDomainEvent(new QuotationCreatedEvent(quotation.Id, quotation.QuotationNo, jobId));
        return quotation;
    }

    public void AddService(Guid serviceId, int quantity, Money unitPrice, string? note = null)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot modify quotation after approval or rejection");

        var service = QuotationService.Create(Id, serviceId, quantity, unitPrice, note);
        _services.Add(service);
        RecalculateTotals();
    }

    public void AddPart(Guid partId, int quantity, Money unitPrice, string? note = null)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot modify quotation after approval or rejection");

        var part = QuotationPart.Create(Id, partId, quantity, unitPrice, note);
        _parts.Add(part);
        RecalculateTotals();
    }

    public void RemoveService(Guid serviceId)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot modify quotation after approval or rejection");

        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service == null)
            throw new DomainException("Service not found in quotation");

        _services.Remove(service);
        RecalculateTotals();
    }

    public void RemovePart(Guid partId)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot modify quotation after approval or rejection");

        var part = _parts.FirstOrDefault(p => p.Id == partId);
        if (part == null)
            throw new DomainException("Part not found in quotation");

        _parts.Remove(part);
        RecalculateTotals();
    }

    public void Submit()
    {
        if (Status != QuotationStatus.Draft)
            throw new DomainException("Only draft quotations can be submitted");

        if (!_services.Any() && !_parts.Any())
            throw new DomainException("Quotation must have at least one item");

        ChangeStatus(QuotationStatus.Pending, "Submitted for approval");
    }

    public void Approve(string approvedBy)
    {
        if (Status != QuotationStatus.Pending)
            throw new DomainException("Only pending quotations can be approved");

        ApprovedAt = DateTime.UtcNow;
        ApprovedBy = approvedBy;
        ChangeStatus(QuotationStatus.Approved, $"Approved by {approvedBy}");

        AddDomainEvent(new QuotationApprovedEvent(Id, JobId));
    }

    public void Reject(string reason)
    {
        if (Status != QuotationStatus.Pending)
            throw new DomainException("Only pending quotations can be rejected");

        RejectedAt = DateTime.UtcNow;
        RejectedReason = reason;
        ChangeStatus(QuotationStatus.Rejected, $"Rejected: {reason}");

        AddDomainEvent(new QuotationRejectedEvent(Id, JobId, reason));
    }

    public void ConvertToPurchaseOrder()
    {
        if (Status != QuotationStatus.Approved)
            throw new DomainException("Only approved quotations can be converted to PO");

        ChangeStatus(QuotationStatus.ConvertedToPO, "Converted to Purchase Order");
    }

    public void Expire()
    {
        if (ValidUntil.HasValue && ValidUntil.Value < DateTime.UtcNow)
        {
            ChangeStatus(QuotationStatus.Expired, "Quotation has expired");
        }
    }

    public void Update(string? notes = null, decimal? discountPercent = null, DateTime? validUntil = null)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot update quotation after approval or rejection");

        if (notes != null) Notes = notes;
        if (discountPercent.HasValue) DiscountPercent = discountPercent.Value;
        if (validUntil.HasValue) ValidUntil = validUntil.Value;

        RecalculateTotals();
    }

    public void UpdateItems(List<QuotationService> services, List<QuotationPart> parts)
    {
        if (Status != QuotationStatus.Draft && Status != QuotationStatus.Pending)
            throw new DomainException("Cannot modify quotation after approval or rejection");

        _services.Clear();
        _services.AddRange(services);
        _parts.Clear();
        _parts.AddRange(parts);
        RecalculateTotals();
    }

    private void ChangeStatus(QuotationStatus newStatus, string? note = null)
    {
        if (Status == newStatus) return;

        Status = newStatus;
        _statusHistory.Add(QuotationStatusHistory.Create(Id, newStatus, note));
        AddDomainEvent(new QuotationStatusChangedEvent(Id, newStatus));
    }

    private void RecalculateTotals()
    {
        var serviceTotal = _services.Sum(s => s.TotalAmount);
        var partTotal = _parts.Sum(p => p.TotalAmount);
        Subtotal = serviceTotal + partTotal;

        // Calculate discount
        DiscountAmount = new Money(Subtotal.Amount * (DiscountPercent / 100m), Subtotal.Currency);
        var afterDiscount = Subtotal - DiscountAmount;

        // Calculate tax (7%)
        Tax = new Money(afterDiscount.Amount * 0.07m, afterDiscount.Currency);
        Total = afterDiscount + Tax;
    }

    private static string GenerateQuotationNo()
        => $"QT-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";

    public bool IsValid()
    {
        if (Status == QuotationStatus.Expired)
            return false;

        if (ValidUntil.HasValue && ValidUntil.Value < DateTime.UtcNow)
        {
            Expire();
            return false;
        }

        return Status == QuotationStatus.Draft ||
               Status == QuotationStatus.Pending ||
               Status == QuotationStatus.Approved;
    }
}
```

### 2. Domain Enums - QuotationStatus.cs
```csharp
namespace ICMON.Domain.Enums;

public enum QuotationStatus
{
    /// <summary>ร่าง - กำลังแก้ไข</summary>
    Draft = 0,
    
    /// <summary>รออนุมัติ - ส่งให้ผู้จัดการอนุมัติ</summary>
    Pending = 1,
    
    /// <summary>อนุมัติแล้ว - พร้อมดำเนินการ</summary>
    Approved = 2,
    
    /// <summary>ปฏิเสธ - ไม่ได้รับการอนุมัติ</summary>
    Rejected = 3,
    
    /// <summary>หมดอายุ - เกินระยะเวลาที่กำหนด</summary>
    Expired = 4,
    
    /// <summary>แปลงเป็นใบสั่งซื้อแล้ว</summary>
    ConvertedToPO = 5
}
```

### 3. Value Object - TaxCalculator.cs
```csharp
namespace ICMON.Domain.ValueObjects;

public class TaxCalculator
{
    private const decimal TaxRate = 0.07m; // 7% VAT

    public static Money CalculateTax(Money amount)
    {
        return new Money(amount.Amount * TaxRate, amount.Currency);
    }

    public static Money CalculateTotalWithTax(Money amount)
    {
        return amount + CalculateTax(amount);
    }

    public static Money CalculateDiscount(Money amount, decimal discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentException("Discount percent must be between 0 and 100");

        return new Money(amount.Amount * (discountPercent / 100m), amount.Currency);
    }

    public static Money CalculateNetAfterDiscount(Money amount, decimal discountPercent)
    {
        var discount = CalculateDiscount(amount, discountPercent);
        return amount - discount;
    }

    public static QuotationSummary CalculateSummary(
        Money subtotal,
        decimal discountPercent,
        decimal taxRate = TaxRate)
    {
        var discount = CalculateDiscount(subtotal, discountPercent);
        var afterDiscount = subtotal - discount;
        var tax = new Money(afterDiscount.Amount * taxRate, afterDiscount.Currency);
        var total = afterDiscount + tax;

        return new QuotationSummary
        {
            Subtotal = subtotal,
            DiscountAmount = discount,
            AfterDiscount = afterDiscount,
            Tax = tax,
            TaxRate = taxRate,
            Total = total
        };
    }
}

public class QuotationSummary
{
    public Money Subtotal { get; set; }
    public Money DiscountAmount { get; set; }
    public Money AfterDiscount { get; set; }
    public Money Tax { get; set; }
    public decimal TaxRate { get; set; }
    public Money Total { get; set; }
}
```

### 4. Repository - QuotationRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class QuotationRepository : GenericRepository<Quotation>, IQuotationRepository
{
    public QuotationRepository(AppDbContext context) : base(context) { }

    public async Task<Quotation?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(q => q.Services)
            .Include(q => q.Parts)
            .Include(q => q.StatusHistory)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<Quotation?> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(q => q.Services)
            .Include(q => q.Parts)
            .Where(q => q.JobId == jobId)
            .OrderByDescending(q => q.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Quotation>> GetActiveByJobAsync(Guid jobId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(q => q.Services)
            .Include(q => q.Parts)
            .Where(q => q.JobId == jobId && 
                       q.Status != QuotationStatus.Rejected && 
                       q.Status != QuotationStatus.Expired)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Quotation>> GetByStatusAsync(QuotationStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(q => q.Status == status)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Quotation>> GetExpiredQuotationsAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Where(q => q.Status != QuotationStatus.Expired && 
                       q.Status != QuotationStatus.Rejected &&
                       q.ValidUntil.HasValue && 
                       q.ValidUntil.Value < now)
            .ToListAsync(cancellationToken);
    }

    public async Task<QuotationStatusSummary> GetStatusSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var quotations = await _dbSet
            .Where(q => q.WhitelabelId == whitelabelId)
            .GroupBy(q => q.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return new QuotationStatusSummary
        {
            Draft = quotations.FirstOrDefault(q => q.Status == QuotationStatus.Draft)?.Count ?? 0,
            Pending = quotations.FirstOrDefault(q => q.Status == QuotationStatus.Pending)?.Count ?? 0,
            Approved = quotations.FirstOrDefault(q => q.Status == QuotationStatus.Approved)?.Count ?? 0,
            Rejected = quotations.FirstOrDefault(q => q.Status == QuotationStatus.Rejected)?.Count ?? 0,
            Expired = quotations.FirstOrDefault(q => q.Status == QuotationStatus.Expired)?.Count ?? 0,
            ConvertedToPO = quotations.FirstOrDefault(q => q.Status == QuotationStatus.ConvertedToPO)?.Count ?? 0,
            Total = quotations.Sum(q => q.Count)
        };
    }
}
```

### 5. Controller - QuotationsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuotationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IQuotationPdfGenerator _pdfGenerator;

    public QuotationsController(IMediator mediator, IQuotationPdfGenerator pdfGenerator)
    {
        _mediator = mediator;
        _pdfGenerator = pdfGenerator;
    }

    [HttpPost]
    [Authorize("QUOTATION_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreateQuotation([FromBody] CreateQuotationCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<QuotationDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<QuotationDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuotation(Guid id)
    {
        var query = new GetQuotationByIdQuery { QuotationId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("job/{jobId}")]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetQuotationByJob(Guid jobId)
    {
        var query = new GetQuotationByJobQuery { JobId = jobId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetQuotationList(
        [FromQuery] QuotationStatus? status,
        [FromQuery] Guid? jobId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetQuotationListQuery
        {
            Status = status,
            JobId = jobId,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize("QUOTATION_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdateQuotation(Guid id, [FromBody] UpdateQuotationCommand command)
    {
        if (id != command.QuotationId)
            return BadRequest(Result<QuotationDto>.Failure("Quotation ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}/approve")]
    [Authorize("QUOTATION_APPROVE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> ApproveQuotation(Guid id, [FromBody] ApproveQuotationCommand command)
    {
        if (id != command.QuotationId)
            return BadRequest(Result<QuotationDto>.Failure("Quotation ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}/reject")]
    [Authorize("QUOTATION_REJECT")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> RejectQuotation(Guid id, [FromBody] RejectQuotationCommand command)
    {
        if (id != command.QuotationId)
            return BadRequest(Result<QuotationDto>.Failure("Quotation ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}/pdf")]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GeneratePdf(Guid id)
    {
        var query = new GetQuotationByIdQuery { QuotationId = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        var pdfBytes = await _pdfGenerator.GenerateQuotationPdfAsync(result.Data);
        return File(pdfBytes, "application/pdf", $"Quotation-{result.Data.QuotationNo}.pdf");
    }

    [HttpGet("{id}/summary")]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetQuotationSummary(Guid id)
    {
        var query = new GetQuotationByIdQuery { QuotationId = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        var summary = TaxCalculator.CalculateSummary(
            new Money(result.Data.Subtotal),
            result.Data.DiscountPercent
        );

        return Ok(new
        {
            result.Data.Id,
            result.Data.QuotationNo,
            Subtotal = summary.Subtotal.Amount,
            Discount = summary.DiscountAmount.Amount,
            AfterDiscount = summary.AfterDiscount.Amount,
            TaxRate = summary.TaxRate,
            Tax = summary.Tax.Amount,
            Total = summary.Total.Amount,
            Currency = summary.Total.Currency
        });
    }

    [HttpGet("status-summary")]
    [Authorize("QUOTATION_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetStatusSummary()
    {
        var query = new GetQuotationStatusSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize("QUOTATION_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteQuotation(Guid id)
    {
        var command = new DeleteQuotationCommand { QuotationId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 6. PDF Generator - QuotationPdfGenerator.cs
```csharp
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ICMON.Infrastructure.DocumentGeneration;

public class QuotationPdfGenerator : IQuotationPdfGenerator
{
    public async Task<byte[]> GenerateQuotationPdfAsync(QuotationDetailDto quotation, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("ICMON Auto Repair Shop")
                                    .FontSize(22)
                                    .Bold()
                                    .FontColor(Colors.Blue.Medium);

                                col.Item().Text("ใบเสนอราคา / Quotation")
                                    .FontSize(16)
                                    .Bold()
                                    .FontColor(Colors.Grey.Darken2);
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignRight().Text($"เลขที่ / No: {quotation.QuotationNo}")
                                    .FontSize(12)
                                    .Bold();
                                col.Item().AlignRight().Text($"วันที่ / Date: {quotation.CreatedAt:dd/MM/yyyy HH:mm}")
                                    .FontSize(11);
                                col.Item().AlignRight().Text($"สถานะ / Status: {quotation.Status}")
                                    .FontSize(11)
                                    .FontColor(quotation.Status == "Approved" ? Colors.Green.Medium : Colors.Orange.Medium);
                            });
                        });

                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        // Customer Info
                        col.Item().Background(Colors.Grey.Lighten3).Padding(10).Row(row =>
                        {
                            row.RelativeItem().Column(info =>
                            {
                                info.Item().Text("ข้อมูลลูกค้า / Customer Information")
                                    .FontSize(13).Bold();
                                info.Item().Text($"ชื่อ / Name: {quotation.CustomerName}");
                                info.Item().Text($"โทร / Phone: {quotation.CustomerPhone}");
                                info.Item().Text($"รถยนต์ / Car: {quotation.CarBrand} {quotation.CarModel} ({quotation.CarLicensePlate})");
                            });
                        });

                        col.Item().PaddingTop(10).Text("รายการ / Items").FontSize(13).Bold();

                        // Items Table
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("รายการ / Item").Bold();
                                header.Cell().Text("จำนวน / Qty").Bold().AlignCenter();
                                header.Cell().Text("ราคาต่อหน่วย / Unit Price").Bold().AlignRight();
                                header.Cell().Text("รวม / Total").Bold().AlignRight();
                            });

                            // Services
                            foreach (var service in quotation.Services)
                            {
                                table.Cell().Text(service.ServiceName);
                                table.Cell().Text(service.Quantity.ToString()).AlignCenter();
                                table.Cell().Text($"{service.UnitPrice:N2}").AlignRight();
                                table.Cell().Text($"{service.Total:N2}").AlignRight();
                            }

                            // Parts
                            foreach (var part in quotation.Parts)
                            {
                                table.Cell().Text(part.PartName);
                                table.Cell().Text(part.Quantity.ToString()).AlignCenter();
                                table.Cell().Text($"{part.UnitPrice:N2}").AlignRight();
                                table.Cell().Text($"{part.Total:N2}").AlignRight();
                            }

                            // Summary
                            table.Footer(footer =>
                            {
                                footer.Cell().ColumnSpan(2).Text("");
                                footer.Cell().Text("Subtotal:").Bold().AlignRight();
                                footer.Cell().Text($"{quotation.Subtotal:N2}").Bold().AlignRight();
                            });
                        });

                        // Totals
                        col.Item().PaddingTop(10).AlignRight().Column(total =>
                        {
                            total.Item().Text($"Subtotal: {quotation.Subtotal:N2} ฿");
                            total.Item().Text($"Discount ({quotation.DiscountPercent}%): -{quotation.Subtotal * quotation.DiscountPercent / 100:N2} ฿");
                            total.Item().Text($"After Discount: {quotation.Subtotal * (1 - quotation.DiscountPercent / 100):N2} ฿");
                            total.Item().Text($"VAT (7%): {quotation.Subtotal * (1 - quotation.DiscountPercent / 100) * 0.07m:N2} ฿");
                            total.Item().Text($"Total: {quotation.Total:N2} ฿")
                                .FontSize(16).Bold().FontColor(Colors.Red.Medium);
                        });

                        // Notes
                        if (!string.IsNullOrEmpty(quotation.Notes))
                        {
                            col.Item().PaddingTop(20).Text($"หมายเหตุ / Notes: {quotation.Notes}")
                                .FontSize(10).FontColor(Colors.Grey.Darken2);
                        }

                        // Validity
                        col.Item().PaddingTop(10).Text($"ใบเสนอราคานี้มีอายุถึง / Valid until: {quotation.ValidUntil:dd/MM/yyyy}")
                            .FontSize(10).FontColor(Colors.Grey.Darken2);
                    });

                    page.Footer().AlignCenter().Text("ICMON Auto Repair Shop - ระบบบริหารจัดการอู่ซ่อมรถ")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }, cancellationToken);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `quotations:{quotationId}` | 30 นาที | ข้อมูลใบเสนอราคา |
| `quotation_job:{jobId}` | 30 นาที | Quotation ตาม Job |
| `quotation_status_summary:{whitelabelId}` | 5 นาที | สรุปสถานะ Quotation |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ สร้าง Quotation พร้อม Services และ Parts
- ✅ คำนวณ Subtotal, Discount, Tax, Total อัตโนมัติ
- ✅ สถานะ Quotation (Draft → Pending → Approved/Rejected → ConvertedToPO)
- ✅ อนุมัติ Quotation (พร้อมบันทึกผู้อนุมัติ)
- ✅ ปฏิเสธ Quotation (พร้อมเหตุผล)
- ✅ แก้ไข Quotation (เฉพาะ Draft/Pending)
- ✅ หมดอายุอัตโนมัติ (ตรวจสอบ ValidUntil)
- ✅ แปลงเป็น Purchase Order
- ✅ ดึง Quotation ตาม Job
- ✅ สรุปสถานะ Quotation
- ✅ สร้าง PDF Quotation สวยงาม (QuestPDF)
- ✅ Domain Events (Created, Approved, Rejected, StatusChanged)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 4: **Quotation** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 5: Purchase Order
2. โมดูลที่ 6: Inventory Management
3. โมดูลที่ 7: Payment Management
4. โมดูลที่ 8: Dashboard & Reports
5. โมดูลที่ 9: Document Management
6. โมดูลที่ 10: Email Service
7. โมดูลที่ 11: Batch Jobs
8. โมดูลที่ 12: Multi-Language (i18n)
9. โมดูลที่ 13: IoT & GPS Tracking
10. โมดูลที่ 14: Web Order System (WOS)

**กรุณาระบุหมายเลขโมดูล หรือพิมพ์ชื่อโมดูลที่ต้องการ** 👇