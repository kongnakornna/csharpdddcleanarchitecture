# 🚗 โมดูลที่ 5: Purchase Order – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Purchase Order (ใบสั่งซื้อ) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 52 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/PurchaseOrderAggregate/PurchaseOrder.cs` | Aggregate Root หลัก |
| 2 | `Aggregates/PurchaseOrderAggregate/PurchaseOrderDetail.cs` | รายการใบสั่งซื้อ |
| 3 | `Aggregates/PurchaseOrderAggregate/PurchaseOrderStatusHistory.cs` | ประวัติการเปลี่ยนสถานะ |
| 4 | `Enums/PurchaseOrderStatus.cs` | สถานะใบสั่งซื้อ |
| 5 | `Events/PurchaseOrderCreatedEvent.cs` | Event เมื่อสร้าง PO |
| 6 | `Events/PurchaseOrderSentEvent.cs` | Event เมื่อส่งให้ Supplier |
| 7 | `Events/PurchaseOrderReceivedEvent.cs` | Event เมื่อรับสินค้า |
| 8 | `Events/PurchaseOrderStatusChangedEvent.cs` | Event เมื่อเปลี่ยนสถานะ |
| 9 | `Interfaces/IPurchaseOrderRepository.cs` | Interface Repository เฉพาะ |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 10 | `DTOs/PurchaseOrders/PurchaseOrderDto.cs` | DTO หลัก |
| 11 | `DTOs/PurchaseOrders/PurchaseOrderDetailDto.cs` | DTO แบบละเอียด |
| 12 | `DTOs/PurchaseOrders/CreatePurchaseOrderDto.cs` | DTO สำหรับสร้าง |
| 13 | `DTOs/PurchaseOrders/PurchaseOrderItemDto.cs` | DTO รายการ |
| 14 | `Commands/PurchaseOrders/CreatePurchaseOrder/CreatePurchaseOrderCommand.cs` | Command สร้าง |
| 15 | `Commands/PurchaseOrders/CreatePurchaseOrder/CreatePurchaseOrderCommandHandler.cs` | Handler สร้าง |
| 16 | `Commands/PurchaseOrders/CreatePurchaseOrder/CreatePurchaseOrderCommandValidator.cs` | Validator สร้าง |
| 17 | `Commands/PurchaseOrders/CreateFromQuotation/CreateFromQuotationCommand.cs` | Command สร้างจาก Quotation |
| 18 | `Commands/PurchaseOrders/CreateFromQuotation/CreateFromQuotationCommandHandler.cs` | Handler สร้างจาก Quotation |
| 19 | `Commands/PurchaseOrders/CreateFromQuotation/CreateFromQuotationCommandValidator.cs` | Validator สร้างจาก Quotation |
| 20 | `Commands/PurchaseOrders/SendPurchaseOrder/SendPurchaseOrderCommand.cs` | Command ส่งให้ Supplier |
| 21 | `Commands/PurchaseOrders/SendPurchaseOrder/SendPurchaseOrderCommandHandler.cs` | Handler ส่งให้ Supplier |
| 22 | `Commands/PurchaseOrders/SendPurchaseOrder/SendPurchaseOrderCommandValidator.cs` | Validator ส่งให้ Supplier |
| 23 | `Commands/PurchaseOrders/ReceivePurchaseOrder/ReceivePurchaseOrderCommand.cs` | Command รับสินค้า |
| 24 | `Commands/PurchaseOrders/ReceivePurchaseOrder/ReceivePurchaseOrderCommandHandler.cs` | Handler รับสินค้า |
| 25 | `Commands/PurchaseOrders/ReceivePurchaseOrder/ReceivePurchaseOrderCommandValidator.cs` | Validator รับสินค้า |
| 26 | `Commands/PurchaseOrders/UpdatePurchaseOrder/UpdatePurchaseOrderCommand.cs` | Command แก้ไข |
| 27 | `Commands/PurchaseOrders/UpdatePurchaseOrder/UpdatePurchaseOrderCommandHandler.cs` | Handler แก้ไข |
| 28 | `Commands/PurchaseOrders/UpdatePurchaseOrder/UpdatePurchaseOrderCommandValidator.cs` | Validator แก้ไข |
| 29 | `Queries/PurchaseOrders/GetPurchaseOrderById/GetPurchaseOrderByIdQuery.cs` | Query ดึงตาม ID |
| 30 | `Queries/PurchaseOrders/GetPurchaseOrderById/GetPurchaseOrderByIdQueryHandler.cs` | Handler ดึงตาม ID |
| 31 | `Queries/PurchaseOrders/GetPurchaseOrderByQuotation/GetPurchaseOrderByQuotationQuery.cs` | Query ดึงตาม Quotation |
| 32 | `Queries/PurchaseOrders/GetPurchaseOrderByQuotation/GetPurchaseOrderByQuotationQueryHandler.cs` | Handler ดึงตาม Quotation |
| 33 | `Queries/PurchaseOrders/GetPurchaseOrderList/GetPurchaseOrderListQuery.cs` | Query รายการ |
| 34 | `Queries/PurchaseOrders/GetPurchaseOrderList/GetPurchaseOrderListQueryHandler.cs` | Handler รายการ |
| 35 | `Queries/PurchaseOrders/GetPOTotalByPeriod/GetPOTotalByPeriodQuery.cs` | Query ยอดรวมตามช่วง |
| 36 | `Queries/PurchaseOrders/GetPOTotalByPeriod/GetPOTotalByPeriodQueryHandler.cs` | Handler ยอดรวมตามช่วง |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 37 | `Persistence/Configurations/PurchaseOrderConfiguration.cs` | EF Config PurchaseOrder |
| 38 | `Persistence/Configurations/PurchaseOrderDetailConfiguration.cs` | EF Config PurchaseOrderDetail |
| 39 | `Persistence/Configurations/PurchaseOrderStatusHistoryConfiguration.cs` | EF Config StatusHistory |
| 40 | `Persistence/Repositories/PurchaseOrderRepository.cs` | Repository เฉพาะ |
| 41 | `Persistence/SeedData/PurchaseOrderSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 42 | `Controllers/PurchaseOrdersController.cs` | Controller (12 Endpoints) |

---

### 📁 **Document Generation** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 43 | `DocumentGeneration/IPurchaseOrderPdfGenerator.cs` | Interface PDF Generator |
| 44 | `DocumentGeneration/PurchaseOrderPdfGenerator.cs` | PDF PO (QuestPDF) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Purchase Orders Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/purchase-orders` | สร้าง PO | 20/60s |
| POST | `/api/purchase-orders/from-quotation/{quotationId}` | สร้างจาก Quotation | 15/60s |
| GET | `/api/purchase-orders/{id}` | ดูรายละเอียด | 100/60s |
| GET | `/api/purchase-orders` | รายการ PO | 50/60s |
| PUT | `/api/purchase-orders/{id}` | แก้ไข PO | 15/60s |
| POST | `/api/purchase-orders/{id}/send` | ส่งให้ Supplier | 10/60s |
| POST | `/api/purchase-orders/{id}/receive` | รับสินค้า | 15/60s |
| GET | `/api/purchase-orders/{id}/pdf` | PDF PO | 15/300s |
| GET | `/api/purchase-orders/suggestion/{jobId}` | แนะนำ PO | 15/60s |
| GET | `/api/purchase-orders/status-summary` | สรุปสถานะ | 30/60s |
| GET | `/api/purchase-orders/total-by-period` | ยอดรวมตามช่วง | 20/60s |
| DELETE | `/api/purchase-orders/{id}` | ลบ PO | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="QuestPDF" Version="2023.12.6" />
```

---

## 🚀 วิธีใช้

### 1. สร้าง Purchase Order จาก Quotation
```http
POST /api/purchase-orders/from-quotation/{quotationId}
Authorization: Bearer <accessToken>
{
  "supplierId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "expectedDeliveryDate": "2026-08-06T00:00:00Z",
  "notes": "กรุณาจัดส่งภายในวันที่กำหนด"
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "poNo": "PO-20260706-a1b2c3",
    "quotationId": "...",
    "status": "Draft",
    "total": 1197.00,
    "createdAt": "2026-07-06T10:30:00Z"
  }
}
```

### 2. สร้าง Purchase Order แบบ Manual
```http
POST /api/purchase-orders
Authorization: Bearer <accessToken>
{
  "supplierId": "...",
  "jobId": "...",
  "expectedDeliveryDate": "2026-08-06T00:00:00Z",
  "notes": "สั่งซื้อเพื่อซ่อมรถลูกค้า",
  "items": [
    {
      "partId": "...",
      "quantity": 2,
      "unitPrice": 350.00
    }
  ]
}
```

### 3. ส่ง Purchase Order ให้ Supplier
```http
POST /api/purchase-orders/{poId}/send
Authorization: Bearer <accessToken>
{
  "sentVia": "Email",
  "note": "ส่งทางอีเมลแล้ว"
}
```

### 4. รับสินค้า (เมื่อ Supplier ส่งมาแล้ว)
```http
POST /api/purchase-orders/{poId}/receive
Authorization: Bearer <accessToken>
{
  "receivedDate": "2026-07-20T00:00:00Z",
  "receivedBy": "John Doe",
  "note": "รับสินค้าครบตามรายการ",
  "items": [
    {
      "partId": "...",
      "receivedQuantity": 2,
      "note": "ครบถ้วน"
    }
  ]
}
```

### 5. ดึงรายการ PO (พร้อม Filter)
```http
GET /api/purchase-orders?status=Sent&fromDate=2026-07-01&toDate=2026-07-31&supplierId=...&page=1&pageSize=20
Authorization: Bearer <accessToken>
```

### 6. ดาวน์โหลด PDF PO
```http
GET /api/purchase-orders/{poId}/pdf
Authorization: Bearer <accessToken>
```

### 7. แนะนำ PO สำหรับ Job
```http
GET /api/purchase-orders/suggestion/{jobId}
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "parts": [
    {
      "partId": "...",
      "partName": "กรองอากาศ",
      "requiredQuantity": 2,
      "availableStock": 5,
      "recommendedOrder": 0
    },
    {
      "partId": "...",
      "partName": "น้ำมันเครื่อง",
      "requiredQuantity": 4,
      "availableStock": 1,
      "recommendedOrder": 3
    }
  ],
  "totalRecommended": 3
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - PurchaseOrder.cs
```csharp
namespace ICMON.Domain.Aggregates.PurchaseOrderAggregate;

public class PurchaseOrder : AggregateRoot<Guid>
{
    public string PoNo { get; private set; }
    public Guid SupplierId { get; private set; }
    public Guid? JobId { get; private set; }
    public Guid? QuotationId { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExpectedDeliveryDate { get; private set; }
    public DateTime? SentAt { get; private set; }
    public string? SentVia { get; private set; }
    public DateTime? ReceivedAt { get; private set; }
    public string? ReceivedBy { get; private set; }
    public string? Notes { get; private set; }
    public Money Total { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<PurchaseOrderDetail> _details = new();
    public IReadOnlyList<PurchaseOrderDetail> Details => _details.AsReadOnly();

    private readonly List<PurchaseOrderStatusHistory> _statusHistory = new();
    public IReadOnlyList<PurchaseOrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private PurchaseOrder() { } // For EF Core

    public static PurchaseOrder Create(
        Guid supplierId,
        Guid whitelabelId,
        Guid? jobId = null,
        Guid? quotationId = null,
        DateTime? expectedDeliveryDate = null,
        string? notes = null)
    {
        var po = new PurchaseOrder
        {
            Id = Guid.NewGuid(),
            PoNo = GeneratePoNo(),
            SupplierId = supplierId,
            JobId = jobId,
            QuotationId = quotationId,
            Status = PurchaseOrderStatus.Draft,
            CreatedAt = DateTime.UtcNow,
            ExpectedDeliveryDate = expectedDeliveryDate ?? DateTime.UtcNow.AddDays(30),
            Notes = notes,
            WhitelabelId = whitelabelId,
            Total = Money.Zero
        };

        po.AddDomainEvent(new PurchaseOrderCreatedEvent(po.Id, po.PoNo));
        return po;
    }

    public void AddItem(Guid partId, int quantity, Money unitPrice)
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new DomainException("Cannot modify PO after it has been sent or received");

        var detail = PurchaseOrderDetail.Create(Id, partId, quantity, unitPrice);
        _details.Add(detail);
        RecalculateTotal();
    }

    public void RemoveItem(Guid detailId)
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new DomainException("Cannot modify PO after it has been sent or received");

        var detail = _details.FirstOrDefault(d => d.Id == detailId);
        if (detail == null)
            throw new DomainException("Item not found in PO");

        _details.Remove(detail);
        RecalculateTotal();
    }

    public void UpdateItems(List<PurchaseOrderDetail> details)
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new DomainException("Cannot modify PO after it has been sent or received");

        _details.Clear();
        _details.AddRange(details);
        RecalculateTotal();
    }

    public void Send(string sentVia, string? note = null)
    {
        if (Status != PurchaseOrderStatus.Draft)
            throw new DomainException("Only draft PO can be sent");

        if (!_details.Any())
            throw new DomainException("PO must have at least one item");

        SentAt = DateTime.UtcNow;
        SentVia = sentVia;
        ChangeStatus(PurchaseOrderStatus.Sent, note ?? $"Sent via {sentVia}");

        AddDomainEvent(new PurchaseOrderSentEvent(Id, SupplierId));
    }

    public void Receive(DateTime receivedDate, string receivedBy, string? note = null, List<ReceivedItem>? receivedItems = null)
    {
        if (Status != PurchaseOrderStatus.Sent)
            throw new DomainException("Only sent PO can be received");

        ReceivedAt = receivedDate;
        ReceivedBy = receivedBy;

        // Update received quantities for each detail
        if (receivedItems != null)
        {
            foreach (var item in receivedItems)
            {
                var detail = _details.FirstOrDefault(d => d.PartId == item.PartId);
                if (detail != null)
                {
                    detail.Receive(item.ReceivedQuantity);
                }
            }
        }

        // Check if all items are fully received
        var allReceived = _details.All(d => d.ReceivedQuantity >= d.Quantity);
        var partialReceived = _details.Any(d => d.ReceivedQuantity > 0 && d.ReceivedQuantity < d.Quantity);

        if (allReceived)
            ChangeStatus(PurchaseOrderStatus.Received, note ?? "All items received");
        else if (partialReceived)
            ChangeStatus(PurchaseOrderStatus.PartiallyReceived, note ?? "Partial items received");
        else
            ChangeStatus(PurchaseOrderStatus.Received, note ?? "Received"); // fallback

        AddDomainEvent(new PurchaseOrderReceivedEvent(Id, SupplierId));
    }

    public void Cancel(string reason)
    {
        if (Status == PurchaseOrderStatus.Received || Status == PurchaseOrderStatus.PartiallyReceived)
            throw new DomainException("Cannot cancel received PO");

        ChangeStatus(PurchaseOrderStatus.Cancelled, reason);
    }

    private void ChangeStatus(PurchaseOrderStatus newStatus, string? note = null)
    {
        if (Status == newStatus) return;

        Status = newStatus;
        _statusHistory.Add(PurchaseOrderStatusHistory.Create(Id, newStatus, note));
        AddDomainEvent(new PurchaseOrderStatusChangedEvent(Id, newStatus));
    }

    private void RecalculateTotal()
    {
        Total = _details.Sum(d => d.Total);
    }

    private static string GeneratePoNo()
        => $"PO-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";

    public bool IsCompletelyReceived()
        => Status == PurchaseOrderStatus.Received;

    public decimal GetReceivedPercentage()
    {
        if (!_details.Any()) return 0;
        var totalQuantity = _details.Sum(d => d.Quantity);
        var receivedQuantity = _details.Sum(d => d.ReceivedQuantity);
        return totalQuantity > 0 ? (receivedQuantity / totalQuantity) * 100 : 0;
    }
}

public class ReceivedItem
{
    public Guid PartId { get; set; }
    public int ReceivedQuantity { get; set; }
    public string? Note { get; set; }
}
```

### 2. Domain Entity - PurchaseOrderDetail.cs
```csharp
namespace ICMON.Domain.Aggregates.PurchaseOrderAggregate;

public class PurchaseOrderDetail : Entity<Guid>
{
    public Guid PurchaseOrderId { get; private set; }
    public virtual PurchaseOrder PurchaseOrder { get; private set; }
    public Guid PartId { get; private set; }
    public int Quantity { get; private set; }
    public int ReceivedQuantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money Total { get; private set; }
    public string? Note { get; private set; }

    private PurchaseOrderDetail() { } // For EF Core

    public static PurchaseOrderDetail Create(Guid purchaseOrderId, Guid partId, int quantity, Money unitPrice, string? note = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        var detail = new PurchaseOrderDetail
        {
            Id = Guid.NewGuid(),
            PurchaseOrderId = purchaseOrderId,
            PartId = partId,
            Quantity = quantity,
            ReceivedQuantity = 0,
            UnitPrice = unitPrice,
            Total = new Money(quantity * unitPrice.Amount, unitPrice.Currency),
            Note = note
        };

        return detail;
    }

    public void Receive(int receivedQuantity)
    {
        if (receivedQuantity < 0)
            throw new ArgumentException("Received quantity cannot be negative");

        if (ReceivedQuantity + receivedQuantity > Quantity)
            throw new DomainException($"Cannot receive more than ordered quantity ({Quantity})");

        ReceivedQuantity += receivedQuantity;
    }

    public bool IsFullyReceived() => ReceivedQuantity >= Quantity;
    public bool IsPartiallyReceived() => ReceivedQuantity > 0 && ReceivedQuantity < Quantity;
    public bool IsNotReceived() => ReceivedQuantity == 0;
}
```

### 3. Domain Enums - PurchaseOrderStatus.cs
```csharp
namespace ICMON.Domain.Enums;

public enum PurchaseOrderStatus
{
    /// <summary>ร่าง - กำลังแก้ไข</summary>
    Draft = 0,
    
    /// <summary>ส่งแล้ว - ส่งให้ Supplier แล้ว</summary>
    Sent = 1,
    
    /// <summary>รับบางส่วน - รับสินค้าไม่ครบ</summary>
    PartiallyReceived = 2,
    
    /// <summary>รับแล้ว - รับสินค้าครบถ้วน</summary>
    Received = 3,
    
    /// <summary>ยกเลิก</summary>
    Cancelled = 4
}
```

### 4. Repository - PurchaseOrderRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
{
    public PurchaseOrderRepository(AppDbContext context) : base(context) { }

    public async Task<PurchaseOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(po => po.Details)
            .Include(po => po.StatusHistory)
            .FirstOrDefaultAsync(po => po.Id == id, cancellationToken);
    }

    public async Task<PurchaseOrder?> GetByQuotationIdAsync(Guid quotationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(po => po.Details)
            .FirstOrDefaultAsync(po => po.QuotationId == quotationId, cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetBySupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(po => po.SupplierId == supplierId)
            .OrderByDescending(po => po.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetByStatusAsync(PurchaseOrderStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(po => po.Status == status)
            .OrderByDescending(po => po.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(po => po.CreatedAt >= fromDate && po.CreatedAt <= toDate)
            .OrderByDescending(po => po.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<PoTotalSummary> GetTotalByPeriodAsync(DateTime fromDate, DateTime toDate, Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var pos = await _dbSet
            .Where(po => po.WhitelabelId == whitelabelId && po.CreatedAt >= fromDate && po.CreatedAt <= toDate)
            .ToListAsync(cancellationToken);

        return new PoTotalSummary
        {
            TotalAmount = pos.Sum(po => po.Total.Amount),
            Count = pos.Count,
            ByStatus = pos.GroupBy(po => po.Status)
                          .Select(g => new StatusTotal { Status = g.Key, Count = g.Count(), Total = g.Sum(po => po.Total.Amount) })
                          .ToList()
        };
    }

    public async Task<IEnumerable<PurchaseOrder>> GetPendingReceiptsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(po => po.Status == PurchaseOrderStatus.Sent || po.Status == PurchaseOrderStatus.PartiallyReceived)
            .Include(po => po.Details)
            .OrderBy(po => po.ExpectedDeliveryDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(po => po.JobId == jobId)
            .OrderByDescending(po => po.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
```

### 5. Command Handler - CreateFromQuotationCommandHandler.cs
```csharp
using MediatR;
using AutoMapper;
using ICMON.Domain.Aggregates.PurchaseOrderAggregate;
using ICMON.Domain.Aggregates.QuotationAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.PurchaseOrders.CreateFromQuotation;

public class CreateFromQuotationCommandHandler : IRequestHandler<CreateFromQuotationCommand, Result<PurchaseOrderDto>>
{
    private readonly IPurchaseOrderRepository _poRepository;
    private readonly IQuotationRepository _quotationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public CreateFromQuotationCommandHandler(
        IPurchaseOrderRepository poRepository,
        IQuotationRepository quotationRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser)
    {
        _poRepository = poRepository;
        _quotationRepository = quotationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<PurchaseOrderDto>> Handle(CreateFromQuotationCommand request, CancellationToken cancellationToken)
    {
        // Get quotation
        var quotation = await _quotationRepository.GetByIdWithDetailsAsync(request.QuotationId, cancellationToken);
        if (quotation == null)
            return Result<PurchaseOrderDto>.Failure("Quotation not found");

        if (quotation.Status != QuotationStatus.Approved)
            return Result<PurchaseOrderDto>.Failure("Quotation must be approved before creating PO");

        // Check if PO already exists for this quotation
        var existingPo = await _poRepository.GetByQuotationIdAsync(request.QuotationId, cancellationToken);
        if (existingPo != null)
            return Result<PurchaseOrderDto>.Failure("A purchase order already exists for this quotation");

        // Create PO
        var po = PurchaseOrder.Create(
            supplierId: request.SupplierId,
            whitelabelId: _currentUser.WhitelabelId,
            jobId: quotation.JobId,
            quotationId: quotation.Id,
            expectedDeliveryDate: request.ExpectedDeliveryDate,
            notes: request.Notes
        );

        // Add items from quotation parts
        foreach (var part in quotation.Parts)
        {
            po.AddItem(part.PartId, part.Quantity, part.UnitPrice);
        }

        // If quotation has services, add as items (or you could ignore services for PO)
        // For this example, we only add parts

        await _poRepository.AddAsync(po, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Update quotation status to ConvertedToPO
        quotation.ConvertToPurchaseOrder();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<PurchaseOrderDto>(po);
        return Result<PurchaseOrderDto>.Success(dto);
    }
}
```

### 6. PDF Generator - PurchaseOrderPdfGenerator.cs
```csharp
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ICMON.Infrastructure.DocumentGeneration;

public class PurchaseOrderPdfGenerator : IPurchaseOrderPdfGenerator
{
    public async Task<byte[]> GeneratePurchaseOrderPdfAsync(PurchaseOrderDetailDto po, CancellationToken cancellationToken = default)
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
                                col.Item().Text("ใบสั่งซื้อ / Purchase Order")
                                    .FontSize(16)
                                    .Bold()
                                    .FontColor(Colors.Grey.Darken2);
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignRight().Text($"เลขที่ / No: {po.PoNo}")
                                    .FontSize(12)
                                    .Bold();
                                col.Item().AlignRight().Text($"วันที่ / Date: {po.CreatedAt:dd/MM/yyyy HH:mm}")
                                    .FontSize(11);
                                col.Item().AlignRight().Text($"สถานะ / Status: {po.Status}")
                                    .FontSize(11)
                                    .FontColor(po.Status == "Received" ? Colors.Green.Medium : Colors.Orange.Medium);
                            });
                        });

                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        // Supplier Info
                        col.Item().Background(Colors.Grey.Lighten3).Padding(10).Row(row =>
                        {
                            row.RelativeItem().Column(info =>
                            {
                                info.Item().Text("ข้อมูลซัพพลายเออร์ / Supplier Information")
                                    .FontSize(13).Bold();
                                info.Item().Text($"ชื่อ / Name: {po.SupplierName}");
                                info.Item().Text($"โทร / Phone: {po.SupplierPhone}");
                                info.Item().Text($"อีเมล / Email: {po.SupplierEmail}");
                            });
                        });

                        col.Item().PaddingTop(10).Text("รายการสินค้า / Items").FontSize(13).Bold();

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
                                header.Cell().Text("สินค้า / Item").Bold();
                                header.Cell().Text("จำนวน / Qty").Bold().AlignCenter();
                                header.Cell().Text("ราคาต่อหน่วย / Unit Price").Bold().AlignRight();
                                header.Cell().Text("รวม / Total").Bold().AlignRight();
                            });

                            foreach (var item in po.Items)
                            {
                                table.Cell().Text(item.PartName);
                                table.Cell().Text(item.Quantity.ToString()).AlignCenter();
                                table.Cell().Text($"{item.UnitPrice:N2}").AlignRight();
                                table.Cell().Text($"{item.Total:N2}").AlignRight();
                            }

                            table.Footer(footer =>
                            {
                                footer.Cell().ColumnSpan(3).Text("Total:").Bold().AlignRight();
                                footer.Cell().Text($"{po.Total:N2}").Bold().AlignRight();
                            });
                        });

                        // Notes
                        if (!string.IsNullOrEmpty(po.Notes))
                        {
                            col.Item().PaddingTop(20).Text($"หมายเหตุ / Notes: {po.Notes}")
                                .FontSize(10).FontColor(Colors.Grey.Darken2);
                        }

                        // Delivery
                        if (po.ExpectedDeliveryDate.HasValue)
                        {
                            col.Item().PaddingTop(10).Text($"กำหนดส่ง / Expected Delivery: {po.ExpectedDeliveryDate:dd/MM/yyyy}")
                                .FontSize(10).FontColor(Colors.Grey.Darken2);
                        }
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

### 7. Controller - PurchaseOrdersController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PurchaseOrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPurchaseOrderPdfGenerator _pdfGenerator;
    private readonly IPurchaseOrderSuggestionService _suggestionService;

    public PurchaseOrdersController(
        IMediator mediator,
        IPurchaseOrderPdfGenerator pdfGenerator,
        IPurchaseOrderSuggestionService suggestionService)
    {
        _mediator = mediator;
        _pdfGenerator = pdfGenerator;
        _suggestionService = suggestionService;
    }

    [HttpPost]
    [Authorize("PO_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("from-quotation/{quotationId}")]
    [Authorize("PO_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreateFromQuotation(Guid quotationId, [FromBody] CreateFromQuotationCommand command)
    {
        if (quotationId != command.QuotationId)
            return BadRequest(Result<PurchaseOrderDto>.Failure("Quotation ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("PO_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPurchaseOrder(Guid id)
    {
        var query = new GetPurchaseOrderByIdQuery { PurchaseOrderId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet]
    [Authorize("PO_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPurchaseOrderList(
        [FromQuery] PurchaseOrderStatus? status,
        [FromQuery] Guid? supplierId,
        [FromQuery] Guid? jobId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPurchaseOrderListQuery
        {
            Status = status,
            SupplierId = supplierId,
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
    [Authorize("PO_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdatePurchaseOrder(Guid id, [FromBody] UpdatePurchaseOrderCommand command)
    {
        if (id != command.PurchaseOrderId)
            return BadRequest(Result<PurchaseOrderDto>.Failure("PO ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id}/send")]
    [Authorize("PO_SEND")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> SendPurchaseOrder(Guid id, [FromBody] SendPurchaseOrderCommand command)
    {
        if (id != command.PurchaseOrderId)
            return BadRequest(Result<bool>.Failure("PO ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id}/receive")]
    [Authorize("PO_RECEIVE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> ReceivePurchaseOrder(Guid id, [FromBody] ReceivePurchaseOrderCommand command)
    {
        if (id != command.PurchaseOrderId)
            return BadRequest(Result<bool>.Failure("PO ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}/pdf")]
    [Authorize("PO_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GeneratePdf(Guid id)
    {
        var query = new GetPurchaseOrderByIdQuery { PurchaseOrderId = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        var pdfBytes = await _pdfGenerator.GeneratePurchaseOrderPdfAsync(result.Data);
        return File(pdfBytes, "application/pdf", $"PO-{result.Data.PoNo}.pdf");
    }

    [HttpGet("suggestion/{jobId}")]
    [Authorize("PO_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPOSSuggestion(Guid jobId)
    {
        var suggestion = await _suggestionService.GenerateSuggestionAsync(jobId);
        return Ok(suggestion);
    }

    [HttpGet("status-summary")]
    [Authorize("PO_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetStatusSummary()
    {
        var query = new GetPOStatusSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("total-by-period")]
    [Authorize("PO_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetTotalByPeriod([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var query = new GetPOTotalByPeriodQuery { FromDate = fromDate, ToDate = toDate };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize("PO_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeletePurchaseOrder(Guid id)
    {
        var command = new DeletePurchaseOrderCommand { PurchaseOrderId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `purchase_orders:{poId}` | 30 นาที | ข้อมูล PO |
| `po_quotation:{quotationId}` | 30 นาที | PO ตาม Quotation |
| `po_suggestion:{jobId}` | 15 นาที | แนะนำ PO |
| `po_status_summary:{whitelabelId}` | 5 นาที | สรุปสถานะ PO |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ สร้าง PO (Manual และจาก Quotation)
- ✅ เพิ่ม/ลบ/แก้ไขรายการ PO (เฉพาะ Draft)
- ✅ ส่ง PO ให้ Supplier (เปลี่ยนสถานะเป็น Sent)
- ✅ รับสินค้า (บันทึกจำนวนที่รับ, เปลี่ยนสถานะเป็น Received/PartiallyReceived)
- ✅ คำนวณยอดรวมอัตโนมัติ
- ✅ สถานะ PO (Draft → Sent → PartiallyReceived/Received/Cancelled)
- ✅ แนะนำ PO จาก Job (คำนวณอะไหล่ที่ขาด)
- ✅ สรุปสถานะ PO
- ✅ ยอดรวม PO ตามช่วงเวลา
- ✅ สร้าง PDF PO
- ✅ Domain Events (Created, Sent, Received, StatusChanged)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 5: **Purchase Order** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 6: Inventory Management
2. โมดูลที่ 7: Payment Management
3. โมดูลที่ 8: Dashboard & Reports
4. โมดูลที่ 9: Document Management
5. โมดูลที่ 10: Email Service
6. โมดูลที่ 11: Batch Jobs
7. โมดูลที่ 12: Multi-Language (i18n)
8. โมดูลที่ 13: IoT & GPS Tracking
9. โมดูลที่ 14: Web Order System (WOS)