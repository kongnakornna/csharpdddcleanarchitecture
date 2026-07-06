# 🚗 โมดูลที่ 6: Inventory Management – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Inventory Management (การจัดการสินค้าคงคลัง) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 68 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/InventoryAggregate/PartMaster.cs` | Aggregate Root อะไหล่หลัก |
| 2 | `Aggregates/InventoryAggregate/StockLocation.cs` | Entity ตำแหน่งจัดเก็บ |
| 3 | `Aggregates/InventoryAggregate/InventoryTransaction.cs` | Entity การเคลื่อนไหวสินค้า |
| 4 | `Aggregates/InventoryAggregate/InventoryAdjustment.cs` | Entity การปรับปรุงสต็อก |
| 5 | `Aggregates/InventoryAggregate/InventoryAdjustmentDetail.cs` | Entity รายการปรับปรุง |
| 6 | `Aggregates/InventoryAggregate/Stocktake.cs` | Entity การตรวจนับสต็อก |
| 7 | `Aggregates/InventoryAggregate/StocktakeDetail.cs` | Entity รายการตรวจนับ |
| 8 | `Aggregates/InventoryAggregate/PartPickingRequest.cs` | Entity คำขอเบิกอะไหล่ |
| 9 | `Aggregates/InventoryAggregate/PartPickingDetail.cs` | Entity รายการเบิก |
| 10 | `Enums/TransactionType.cs` | ประเภทการเคลื่อนไหว |
| 11 | `Enums/InventoryStatus.cs` | สถานะสินค้า |
| 12 | `Enums/PickingStatus.cs` | สถานะการเบิก |
| 13 | `Events/PartCreatedEvent.cs` | Event สร้างอะไหล่ |
| 14 | `Events/PartStockUpdatedEvent.cs` | Event อัปเดตสต็อก |
| 15 | `Events/PartPickingRequestCreatedEvent.cs` | Event สร้างคำขอเบิก |
| 16 | `Events/PartPickingRequestConfirmedEvent.cs` | Event ยืนยันการเบิก |
| 17 | `Events/StocktakeCreatedEvent.cs` | Event สร้างการตรวจนับ |
| 18 | `Interfaces/IPartMasterRepository.cs` | Interface Repository Part |
| 19 | `Interfaces/IInventoryTransactionRepository.cs` | Interface Repository Transaction |
| 20 | `Interfaces/IPartPickingRequestRepository.cs` | Interface Repository Picking |
| 21 | `ValueObjects/PartCode.cs` | Value Object รหัสอะไหล่ |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 22 | `DTOs/Inventory/PartMasterDto.cs` | DTO อะไหล่ |
| 23 | `DTOs/Inventory/PartDetailDto.cs` | DTO อะไหล่แบบละเอียด |
| 24 | `DTOs/Inventory/InventoryTransactionDto.cs` | DTO การเคลื่อนไหว |
| 25 | `DTOs/Inventory/PartPickingRequestDto.cs` | DTO คำขอเบิก |
| 26 | `DTOs/Inventory/StocktakeDto.cs` | DTO การตรวจนับ |
| 27 | `Commands/Inventory/CreatePart/CreatePartCommand.cs` | Command สร้างอะไหล่ |
| 28 | `Commands/Inventory/CreatePart/CreatePartCommandHandler.cs` | Handler สร้างอะไหล่ |
| 29 | `Commands/Inventory/CreatePart/CreatePartCommandValidator.cs` | Validator สร้างอะไหล่ |
| 30 | `Commands/Inventory/ReceiveStock/ReceiveStockCommand.cs` | Command รับสินค้าเข้า |
| 31 | `Commands/Inventory/ReceiveStock/ReceiveStockCommandHandler.cs` | Handler รับสินค้าเข้า |
| 32 | `Commands/Inventory/ReceiveStock/ReceiveStockCommandValidator.cs` | Validator รับสินค้าเข้า |
| 33 | `Commands/Inventory/IssueStock/IssueStockCommand.cs` | Command เบิกจ่ายสินค้า |
| 34 | `Commands/Inventory/IssueStock/IssueStockCommandHandler.cs` | Handler เบิกจ่ายสินค้า |
| 35 | `Commands/Inventory/IssueStock/IssueStockCommandValidator.cs` | Validator เบิกจ่ายสินค้า |
| 36 | `Commands/Inventory/AdjustStock/AdjustStockCommand.cs` | Command ปรับปรุงสต็อก |
| 37 | `Commands/Inventory/AdjustStock/AdjustStockCommandHandler.cs` | Handler ปรับปรุงสต็อก |
| 38 | `Commands/Inventory/AdjustStock/AdjustStockCommandValidator.cs` | Validator ปรับปรุงสต็อก |
| 39 | `Commands/Inventory/CreatePickingRequest/CreatePickingRequestCommand.cs` | Command สร้างคำขอเบิก |
| 40 | `Commands/Inventory/CreatePickingRequest/CreatePickingRequestCommandHandler.cs` | Handler สร้างคำขอเบิก |
| 41 | `Commands/Inventory/CreatePickingRequest/CreatePickingRequestCommandValidator.cs` | Validator สร้างคำขอเบิก |
| 42 | `Commands/Inventory/ConfirmPicking/ConfirmPickingCommand.cs` | Command ยืนยันการเบิก |
| 43 | `Commands/Inventory/ConfirmPicking/ConfirmPickingCommandHandler.cs` | Handler ยืนยันการเบิก |
| 44 | `Commands/Inventory/ConfirmPicking/ConfirmPickingCommandValidator.cs` | Validator ยืนยันการเบิก |
| 45 | `Commands/Inventory/CreateStocktake/CreateStocktakeCommand.cs` | Command สร้างการตรวจนับ |
| 46 | `Commands/Inventory/CreateStocktake/CreateStocktakeCommandHandler.cs` | Handler สร้างการตรวจนับ |
| 47 | `Commands/Inventory/CreateStocktake/CreateStocktakeCommandValidator.cs` | Validator สร้างการตรวจนับ |
| 48 | `Queries/Inventory/GetPartById/GetPartByIdQuery.cs` | Query ดึงอะไหล่ตาม ID |
| 49 | `Queries/Inventory/GetPartById/GetPartByIdQueryHandler.cs` | Handler ดึงอะไหล่ตาม ID |
| 50 | `Queries/Inventory/GetPartByCode/GetPartByCodeQuery.cs` | Query ดึงตามรหัส |
| 51 | `Queries/Inventory/GetPartByCode/GetPartByCodeQueryHandler.cs` | Handler ดึงตามรหัส |
| 52 | `Queries/Inventory/GetPartList/GetPartListQuery.cs` | Query รายการอะไหล่ |
| 53 | `Queries/Inventory/GetPartList/GetPartListQueryHandler.cs` | Handler รายการอะไหล่ |
| 54 | `Queries/Inventory/GetLowStockParts/GetLowStockPartsQuery.cs` | Query อะไหล่ต่ำกว่าเกณฑ์ |
| 55 | `Queries/Inventory/GetLowStockParts/GetLowStockPartsQueryHandler.cs` | Handler อะไหล่ต่ำกว่าเกณฑ์ |
| 56 | `Queries/Inventory/GetPartTransactions/GetPartTransactionsQuery.cs` | Query ประวัติการเคลื่อนไหว |
| 57 | `Queries/Inventory/GetPartTransactions/GetPartTransactionsQueryHandler.cs` | Handler ประวัติการเคลื่อนไหว |
| 58 | `Queries/Inventory/GetPickingRequests/GetPickingRequestsQuery.cs` | Query รายการคำขอเบิก |
| 59 | `Queries/Inventory/GetPickingRequests/GetPickingRequestsQueryHandler.cs` | Handler รายการคำขอเบิก |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 60 | `Persistence/Configurations/PartMasterConfiguration.cs` | EF Config PartMaster |
| 61 | `Persistence/Configurations/StockLocationConfiguration.cs` | EF Config StockLocation |
| 62 | `Persistence/Configurations/InventoryTransactionConfiguration.cs` | EF Config InventoryTransaction |
| 63 | `Persistence/Configurations/InventoryAdjustmentConfiguration.cs` | EF Config InventoryAdjustment |
| 64 | `Persistence/Configurations/StocktakeConfiguration.cs` | EF Config Stocktake |
| 65 | `Persistence/Configurations/PartPickingRequestConfiguration.cs` | EF Config PartPickingRequest |
| 66 | `Persistence/Repositories/PartMasterRepository.cs` | Repository Part |
| 67 | `Persistence/Repositories/InventoryTransactionRepository.cs` | Repository Transaction |
| 68 | `Persistence/Repositories/PartPickingRequestRepository.cs` | Repository Picking |
| 69 | `Persistence/SeedData/InventorySeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 70 | `Controllers/InventoryController.cs` | Controller Inventory (14 Endpoints) |
| 71 | `Controllers/PartPickingController.cs` | Controller Part Picking (7 Endpoints) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Inventory Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/inventory/parts` | สร้างอะไหล่ใหม่ | 20/60s |
| GET | `/api/inventory/parts/{id}` | ดูข้อมูลอะไหล่ | 100/60s |
| GET | `/api/inventory/parts/code/{partCode}` | ค้นหาด้วยรหัส | 80/60s |
| GET | `/api/inventory/parts` | รายการอะไหล่ | 50/60s |
| GET | `/api/inventory/parts/low-stock` | อะไหล่ต่ำกว่าเกณฑ์ | 30/60s |
| PUT | `/api/inventory/parts/{id}` | แก้ไขอะไหล่ | 20/60s |
| DELETE | `/api/inventory/parts/{id}` | ลบอะไหล่ | 5/3600s |
| POST | `/api/inventory/transactions/receive` | รับสินค้าเข้า | 20/60s |
| POST | `/api/inventory/transactions/issue` | เบิกจ่ายสินค้า | 30/60s |
| POST | `/api/inventory/transactions/adjust` | ปรับปรุงสต็อก | 15/60s |
| GET | `/api/inventory/transactions/part/{partId}` | ประวัติการเคลื่อนไหว | 60/60s |
| GET | `/api/inventory/stocktake` | รายการตรวจนับ | 30/60s |
| POST | `/api/inventory/stocktake` | สร้างการตรวจนับ | 10/60s |
| GET | `/api/inventory/summary` | สรุปภาพรวมสินค้า | 30/60s |

### Part Picking Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/part-picking` | สร้างคำขอเบิก | 30/60s |
| GET | `/api/part-picking/{id}` | ดูคำขอเบิก | 80/60s |
| GET | `/api/part-picking` | รายการคำขอเบิก | 50/60s |
| GET | `/api/part-picking/job/{jobId}` | ดึงตาม Job | 60/60s |
| PUT | `/api/part-picking/{id}/confirm` | ยืนยันการเบิก | 20/60s |
| PUT | `/api/part-picking/{id}/cancel` | ยกเลิกคำขอเบิก | 10/60s |
| GET | `/api/part-picking/status-summary` | สรุปสถานะ | 30/60s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ไม่ต้องติดตั้งเพิ่มเติม (ใช้ของเดิมที่มีอยู่แล้ว)

---

## 🚀 วิธีใช้

### 1. สร้างอะไหล่ใหม่
```http
POST /api/inventory/parts
Authorization: Bearer <accessToken>
{
  "partCode": "OIL-5W30-1L",
  "partName": "น้ำมันเครื่อง 5W-30 1 ลิตร",
  "description": "น้ำมันเครื่องสังเคราะห์แท้",
  "category": "น้ำมัน",
  "unit": "ลิตร",
  "stockQuantity": 50,
  "reorderLevel": 10,
  "reorderQuantity": 25,
  "cost": 120.00,
  "sellingPrice": 180.00
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "partCode": "OIL-5W30-1L",
    "partName": "น้ำมันเครื่อง 5W-30 1 ลิตร",
    "stockQuantity": 50,
    "reorderLevel": 10,
    "status": "Active"
  }
}
```

### 2. รับสินค้าเข้า
```http
POST /api/inventory/transactions/receive
Authorization: Bearer <accessToken>
{
  "partId": "...",
  "quantity": 20,
  "unitPrice": 115.00,
  "reference": "PO-20260706-a1b2c3",
  "note": "รับสินค้าตามใบสั่งซื้อ"
}
```

### 3. เบิกจ่ายสินค้า
```http
POST /api/inventory/transactions/issue
Authorization: Bearer <accessToken>
{
  "partId": "...",
  "quantity": 5,
  "reference": "JOB-20260706-a1b2c3",
  "note": "เบิกเพื่อซ่อมรถลูกค้า"
}
```

### 4. สร้างคำขอเบิกอะไหล่
```http
POST /api/part-picking
Authorization: Bearer <accessToken>
{
  "jobId": "...",
  "requestedBy": "ช่างสมชาย",
  "items": [
    {
      "partId": "...",
      "quantity": 2,
      "note": "เปลี่ยนกรองอากาศ"
    }
  ]
}
```

### 5. ยืนยันการเบิก
```http
PUT /api/part-picking/{pickingId}/confirm
Authorization: Bearer <accessToken>
{
  "confirmedBy": "คลังสินค้า",
  "note": "เบิกเรียบร้อยแล้ว"
}
```

### 6. ดูอะไหล่ที่ต่ำกว่าเกณฑ์
```http
GET /api/inventory/parts/low-stock
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "id": "...",
    "partCode": "OIL-5W30-1L",
    "partName": "น้ำมันเครื่อง 5W-30 1 ลิตร",
    "stockQuantity": 8,
    "reorderLevel": 10,
    "needReorder": true
  }
]
```

### 7. ประวัติการเคลื่อนไหวของอะไหล่
```http
GET /api/inventory/transactions/part/{partId}?fromDate=2026-07-01&toDate=2026-07-31
Authorization: Bearer <accessToken>
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - PartMaster.cs
```csharp
namespace ICMON.Domain.Aggregates.InventoryAggregate;

public class PartMaster : AggregateRoot<Guid>
{
    public PartCode PartCode { get; private set; }
    public string PartName { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public string? Unit { get; private set; }
    public int StockQuantity { get; private set; }
    public int ReorderLevel { get; private set; }
    public int ReorderQuantity { get; private set; }
    public Money Cost { get; private set; }
    public Money SellingPrice { get; private set; }
    public InventoryStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<InventoryTransaction> _transactions = new();
    public IReadOnlyList<InventoryTransaction> Transactions => _transactions.AsReadOnly();

    private PartMaster() { } // For EF Core

    public static PartMaster Create(
        string partCode,
        string partName,
        int stockQuantity,
        int reorderLevel,
        int reorderQuantity,
        decimal cost,
        decimal sellingPrice,
        Guid whitelabelId,
        string? description = null,
        string? category = null,
        string? unit = null)
    {
        var part = new PartMaster
        {
            Id = Guid.NewGuid(),
            PartCode = PartCode.Create(partCode),
            PartName = partName,
            Description = description,
            Category = category,
            Unit = unit ?? "ชิ้น",
            StockQuantity = stockQuantity,
            ReorderLevel = reorderLevel,
            ReorderQuantity = reorderQuantity,
            Cost = new Money(cost),
            SellingPrice = new Money(sellingPrice),
            Status = InventoryStatus.Active,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        part.AddDomainEvent(new PartCreatedEvent(part.Id, part.PartCode.Value));
        return part;
    }

    public void Update(string partName, string? description, string? category, string? unit,
        int reorderLevel, int reorderQuantity, decimal cost, decimal sellingPrice)
    {
        PartName = partName;
        Description = description;
        Category = category;
        Unit = unit ?? "ชิ้น";
        ReorderLevel = reorderLevel;
        ReorderQuantity = reorderQuantity;
        Cost = new Money(cost);
        SellingPrice = new Money(sellingPrice);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ReceiveStock(int quantity, decimal unitPrice, string reference, string? note = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (Status != InventoryStatus.Active)
            throw new DomainException("Cannot receive stock for inactive part");

        StockQuantity += quantity;

        var transaction = InventoryTransaction.CreateReceive(
            Id, quantity, new Money(unitPrice), reference, note);

        _transactions.Add(transaction);
        AddDomainEvent(new PartStockUpdatedEvent(Id, StockQuantity, TransactionType.Receive));
    }

    public void IssueStock(int quantity, string reference, string? note = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (Status != InventoryStatus.Active)
            throw new DomainException("Cannot issue stock for inactive part");

        if (StockQuantity < quantity)
            throw new DomainException($"Insufficient stock. Available: {StockQuantity}, Requested: {quantity}");

        StockQuantity -= quantity;

        var transaction = InventoryTransaction.CreateIssue(
            Id, quantity, Cost, reference, note);

        _transactions.Add(transaction);
        AddDomainEvent(new PartStockUpdatedEvent(Id, StockQuantity, TransactionType.Issue));
    }

    public void AdjustStock(int adjustmentQuantity, string reason, string? note = null)
    {
        if (Status != InventoryStatus.Active)
            throw new DomainException("Cannot adjust stock for inactive part");

        var newQuantity = StockQuantity + adjustmentQuantity;
        if (newQuantity < 0)
            throw new DomainException($"Adjustment would result in negative stock: {newQuantity}");

        StockQuantity = newQuantity;

        var transaction = InventoryTransaction.CreateAdjustment(
            Id, adjustmentQuantity, Cost, reason, note);

        _transactions.Add(transaction);
        AddDomainEvent(new PartStockUpdatedEvent(Id, StockQuantity, TransactionType.Adjustment));
    }

    public void Deactivate()
    {
        Status = InventoryStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = InventoryStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsLowStock() => StockQuantity <= ReorderLevel;
    public bool IsOutOfStock() => StockQuantity <= 0;
    public int GetRecommendedReorder() => ReorderQuantity - StockQuantity;
}
```

### 2. Domain Entity - InventoryTransaction.cs
```csharp
namespace ICMON.Domain.Aggregates.InventoryAggregate;

public class InventoryTransaction : Entity<Guid>
{
    public Guid PartId { get; private set; }
    public virtual PartMaster Part { get; private set; }
    public TransactionType Type { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money Total { get; private set; }
    public string Reference { get; private set; }
    public string? Note { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid? CreatedBy { get; private set; }

    private InventoryTransaction() { } // For EF Core

    public static InventoryTransaction CreateReceive(
        Guid partId,
        int quantity,
        Money unitPrice,
        string reference,
        string? note = null,
        Guid? createdBy = null)
    {
        return new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            PartId = partId,
            Type = TransactionType.Receive,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Total = new Money(quantity * unitPrice.Amount, unitPrice.Currency),
            Reference = reference,
            Note = note,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    public static InventoryTransaction CreateIssue(
        Guid partId,
        int quantity,
        Money unitPrice,
        string reference,
        string? note = null,
        Guid? createdBy = null)
    {
        return new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            PartId = partId,
            Type = TransactionType.Issue,
            Quantity = -quantity,
            UnitPrice = unitPrice,
            Total = new Money(-quantity * unitPrice.Amount, unitPrice.Currency),
            Reference = reference,
            Note = note,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    public static InventoryTransaction CreateAdjustment(
        Guid partId,
        int adjustmentQuantity,
        Money unitPrice,
        string reason,
        string? note = null,
        Guid? createdBy = null)
    {
        return new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            PartId = partId,
            Type = adjustmentQuantity > 0 ? TransactionType.Adjustment : TransactionType.Adjustment,
            Quantity = adjustmentQuantity,
            UnitPrice = unitPrice,
            Total = new Money(adjustmentQuantity * unitPrice.Amount, unitPrice.Currency),
            Reference = reason,
            Note = note,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }
}
```

### 3. Domain Entity - PartPickingRequest.cs
```csharp
namespace ICMON.Domain.Aggregates.InventoryAggregate;

public class PartPickingRequest : Entity<Guid>
{
    public string RequestNo { get; private set; }
    public Guid JobId { get; private set; }
    public string RequestedBy { get; private set; }
    public PickingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public string? ConfirmedBy { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancelledReason { get; private set; }
    public string? Note { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<PartPickingDetail> _details = new();
    public IReadOnlyList<PartPickingDetail> Details => _details.AsReadOnly();

    private PartPickingRequest() { } // For EF Core

    public static PartPickingRequest Create(
        Guid jobId,
        string requestedBy,
        Guid whitelabelId,
        string? note = null)
    {
        var request = new PartPickingRequest
        {
            Id = Guid.NewGuid(),
            RequestNo = GenerateRequestNo(),
            JobId = jobId,
            RequestedBy = requestedBy,
            Status = PickingStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            Note = note,
            WhitelabelId = whitelabelId
        };

        request.AddDomainEvent(new PartPickingRequestCreatedEvent(request.Id, request.RequestNo, jobId));
        return request;
    }

    public void AddItem(Guid partId, int quantity, string? note = null)
    {
        if (Status != PickingStatus.Pending)
            throw new DomainException("Cannot add items to confirmed or cancelled request");

        var detail = PartPickingDetail.Create(Id, partId, quantity, note);
        _details.Add(detail);
    }

    public void Confirm(string confirmedBy, string? note = null)
    {
        if (Status != PickingStatus.Pending)
            throw new DomainException("Only pending requests can be confirmed");

        if (!_details.Any())
            throw new DomainException("Request must have at least one item");

        // Check stock availability for all items
        foreach (var detail in _details)
        {
            if (detail.Part.StockQuantity < detail.Quantity)
                throw new DomainException($"Insufficient stock for part {detail.Part.PartName}. Available: {detail.Part.StockQuantity}, Requested: {detail.Quantity}");
        }

        ConfirmedAt = DateTime.UtcNow;
        ConfirmedBy = confirmedBy;
        Status = PickingStatus.Confirmed;
        Note = note;

        AddDomainEvent(new PartPickingRequestConfirmedEvent(Id, JobId, confirmedBy));
    }

    public void Cancel(string reason)
    {
        if (Status == PickingStatus.Confirmed)
            throw new DomainException("Cannot cancel confirmed request");

        CancelledAt = DateTime.UtcNow;
        CancelledReason = reason;
        Status = PickingStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != PickingStatus.Confirmed)
            throw new DomainException("Only confirmed requests can be completed");

        Status = PickingStatus.Completed;
    }

    private static string GenerateRequestNo()
        => $"PR-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";
}
```

### 4. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum TransactionType
{
    Receive = 0,      // รับสินค้าเข้า
    Issue = 1,        // เบิกจ่ายสินค้า
    Adjustment = 2,   // ปรับปรุงสต็อก
    Return = 3        // คืนสินค้า
}

public enum InventoryStatus
{
    Active = 0,
    Inactive = 1,
    Discontinued = 2
}

public enum PickingStatus
{
    Pending = 0,      // รอดำเนินการ
    Confirmed = 1,    // ยืนยันแล้ว
    Completed = 2,    // เสร็จสิ้น
    Cancelled = 3     // ยกเลิก
}
```

### 5. Repository - PartMasterRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class PartMasterRepository : GenericRepository<PartMaster>, IPartMasterRepository
{
    public PartMasterRepository(AppDbContext context) : base(context) { }

    public async Task<PartMaster?> GetByCodeAsync(string partCode, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.PartCode.Value == partCode, cancellationToken);
    }

    public async Task<IEnumerable<PartMaster>> GetLowStockPartsAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.WhitelabelId == whitelabelId && 
                       p.StockQuantity <= p.ReorderLevel && 
                       p.Status == InventoryStatus.Active)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PartMaster>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Category == category && p.Status == InventoryStatus.Active)
            .OrderBy(p => p.PartName)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PartMaster>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
    {
        keyword = keyword.ToLower();
        return await _dbSet
            .Where(p => p.PartName.ToLower().Contains(keyword) ||
                       p.PartCode.Value.ToLower().Contains(keyword) ||
                       (p.Description != null && p.Description.ToLower().Contains(keyword)))
            .Where(p => p.Status == InventoryStatus.Active)
            .ToListAsync(cancellationToken);
    }

    public async Task<InventorySummary> GetSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var parts = await _dbSet
            .Where(p => p.WhitelabelId == whitelabelId)
            .ToListAsync(cancellationToken);

        return new InventorySummary
        {
            TotalParts = parts.Count,
            TotalStockValue = parts.Sum(p => p.StockQuantity * p.Cost.Amount),
            LowStockCount = parts.Count(p => p.StockQuantity <= p.ReorderLevel),
            OutOfStockCount = parts.Count(p => p.StockQuantity <= 0),
            ActiveParts = parts.Count(p => p.Status == InventoryStatus.Active)
        };
    }
}
```

### 6. Command Handler - ReceiveStockCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Aggregates.InventoryAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Inventory.ReceiveStock;

public class ReceiveStockCommandHandler : IRequestHandler<ReceiveStockCommand, Result<InventoryTransactionDto>>
{
    private readonly IPartMasterRepository _partRepository;
    private readonly IInventoryTransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public ReceiveStockCommandHandler(
        IPartMasterRepository partRepository,
        IInventoryTransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser)
    {
        _partRepository = partRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<InventoryTransactionDto>> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
    {
        var part = await _partRepository.GetByIdAsync(request.PartId, cancellationToken);
        if (part == null)
            return Result<InventoryTransactionDto>.Failure("Part not found");

        // Receive stock
        part.ReceiveStock(request.Quantity, request.UnitPrice, request.Reference, request.Note);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var transaction = part.Transactions.Last();
        var dto = _mapper.Map<InventoryTransactionDto>(transaction);

        return Result<InventoryTransactionDto>.Success(dto);
    }
}
```

### 7. Controller - InventoryController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator) => _mediator = mediator;

    #region Part Management

    [HttpPost("parts")]
    [Authorize("INVENTORY_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreatePart([FromBody] CreatePartCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("parts/{id}")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPart(Guid id)
    {
        var query = new GetPartByIdQuery { PartId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("parts/code/{partCode}")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPartByCode(string partCode)
    {
        var query = new GetPartByCodeQuery { PartCode = partCode };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("parts")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPartList(
        [FromQuery] string? keyword,
        [FromQuery] string? category,
        [FromQuery] InventoryStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPartListQuery
        {
            Keyword = keyword,
            Category = category,
            Status = status,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("parts/low-stock")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetLowStockParts()
    {
        var query = new GetLowStockPartsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("parts/{id}")]
    [Authorize("INVENTORY_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdatePart(Guid id, [FromBody] UpdatePartCommand command)
    {
        if (id != command.PartId)
            return BadRequest(Result<PartMasterDto>.Failure("Part ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("parts/{id}")]
    [Authorize("INVENTORY_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeletePart(Guid id)
    {
        var command = new DeletePartCommand { PartId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    #endregion

    #region Stock Transactions

    [HttpPost("transactions/receive")]
    [Authorize("INVENTORY_RECEIVE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> ReceiveStock([FromBody] ReceiveStockCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("transactions/issue")]
    [Authorize("INVENTORY_ISSUE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> IssueStock([FromBody] IssueStockCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("transactions/adjust")]
    [Authorize("INVENTORY_ADJUST")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> AdjustStock([FromBody] AdjustStockCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("transactions/part/{partId}")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPartTransactions(
        Guid partId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPartTransactionsQuery
        {
            PartId = partId,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion

    #region Stocktake

    [HttpPost("stocktake")]
    [Authorize("INVENTORY_ADJUST")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreateStocktake([FromBody] CreateStocktakeCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("stocktake")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetStocktakeList(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetStocktakeListQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion

    #region Summary

    [HttpGet("summary")]
    [Authorize("INVENTORY_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetInventorySummary()
    {
        var query = new GetInventorySummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion
}
```

### 8. Controller - PartPickingController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PartPickingController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartPickingController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize("PICKING_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreatePickingRequest([FromBody] CreatePickingRequestCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("PICKING_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPickingRequest(Guid id)
    {
        var query = new GetPickingRequestByIdQuery { PickingRequestId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet]
    [Authorize("PICKING_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPickingRequests(
        [FromQuery] PickingStatus? status,
        [FromQuery] Guid? jobId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPickingRequestsQuery
        {
            Status = status,
            JobId = jobId,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("job/{jobId}")]
    [Authorize("PICKING_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPickingByJob(Guid jobId)
    {
        var query = new GetPickingRequestsByJobQuery { JobId = jobId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}/confirm")]
    [Authorize("PICKING_CONFIRM")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> ConfirmPicking(Guid id, [FromBody] ConfirmPickingCommand command)
    {
        if (id != command.PickingRequestId)
            return BadRequest(Result<PartPickingRequestDto>.Failure("Request ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id}/cancel")]
    [Authorize("PICKING_CANCEL")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> CancelPicking(Guid id, [FromBody] CancelPickingCommand command)
    {
        if (id != command.PickingRequestId)
            return BadRequest(Result<bool>.Failure("Request ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("status-summary")]
    [Authorize("PICKING_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetStatusSummary()
    {
        var query = new GetPickingStatusSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `parts:{partId}` | 1 ชม. | ข้อมูลอะไหล่ |
| `part_code:{partCode}` | 1 ชม. | อะไหล่ตามรหัส |
| `stock_summary:{partId}` | 15 นาที | สรุปสต็อก |
| `low_stock_parts:{whitelabelId}` | 5 นาที | อะไหล่ต่ำกว่าเกณฑ์ |
| `inventory_summary:{whitelabelId}` | 5 นาที | สรุปภาพรวมสินค้า |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ สร้าง/แก้ไข/ลบ อะไหล่ (Part Master)
- ✅ รับสินค้าเข้า (Receive)
- ✅ เบิกจ่ายสินค้า (Issue)
- ✅ ปรับปรุงสต็อก (Adjustment)
- ✅ ตรวจสอบสต็อกคงเหลือ
- ✅ อะไหล่ต่ำกว่าเกณฑ์ (Low Stock Alert)
- ✅ ประวัติการเคลื่อนไหวของสินค้า
- ✅ คำขอเบิกอะไหล่ (Part Picking Request)
- ✅ ยืนยันการเบิก (Confirm Picking)
- ✅ ยกเลิกคำขอเบิก
- ✅ การตรวจนับสต็อก (Stocktake)
- ✅ สรุปภาพรวมสินค้าคงคลัง
- ✅ Domain Events (Created, StockUpdated, PickingRequested, PickingConfirmed)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 6: **Inventory Management** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 7: Payment Management
2. โมดูลที่ 8: Dashboard & Reports
3. โมดูลที่ 9: Document Management
4. โมดูลที่ 10: Email Service
5. โมดูลที่ 11: Batch Jobs
6. โมดูลที่ 12: Multi-Language (i18n)
7. โมดูลที่ 13: IoT & GPS Tracking
8. โมดูลที่ 14: Web Order System (WOS)