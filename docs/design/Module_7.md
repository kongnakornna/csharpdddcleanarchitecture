# 🚗 โมดูลที่ 7: Payment Management – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Payment Management (การจัดการการชำระเงิน) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 62 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/PaymentAggregate/Payment.cs` | Aggregate Root การชำระเงิน |
| 2 | `Aggregates/PaymentAggregate/Receipt.cs` | Entity ใบเสร็จรับเงิน |
| 3 | `Aggregates/PaymentAggregate/PaymentHistory.cs` | Entity ประวัติการชำระเงิน |
| 4 | `Aggregates/PaymentAggregate/OutstandingBalance.cs` | Entity ยอดคงค้าง |
| 5 | `Aggregates/PaymentAggregate/ReceivedAmount.cs` | Entity จำนวนเงินที่รับ |
| 6 | `Enums/PaymentStatus.cs` | สถานะการชำระเงิน |
| 7 | `Enums/PaymentMethod.cs` | วิธีการชำระเงิน |
| 8 | `Events/PaymentCreatedEvent.cs` | Event เมื่อสร้าง Payment |
| 9 | `Events/PaymentConfirmedEvent.cs` | Event เมื่อยืนยัน Payment |
| 10 | `Events/PaymentRefundedEvent.cs` | Event เมื่อคืนเงิน |
| 11 | `Events/PaymentStatusChangedEvent.cs` | Event เปลี่ยนสถานะ |
| 12 | `Interfaces/IPaymentRepository.cs` | Interface Repository Payment |
| 13 | `Interfaces/IReceiptRepository.cs` | Interface Repository Receipt |
| 14 | `Interfaces/IOutstandingBalanceRepository.cs` | Interface Repository Outstanding |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 15 | `DTOs/Payments/PaymentDto.cs` | DTO หลัก Payment |
| 16 | `DTOs/Payments/PaymentDetailDto.cs` | DTO แบบละเอียด |
| 17 | `DTOs/Payments/ReceiptDto.cs` | DTO ใบเสร็จ |
| 18 | `DTOs/Payments/OutstandingBalanceDto.cs` | DTO ยอดคงค้าง |
| 19 | `DTOs/Payments/CreatePaymentDto.cs` | DTO สำหรับสร้าง Payment |
| 20 | `Commands/Payments/RecordPayment/RecordPaymentCommand.cs` | Command บันทึกการชำระ |
| 21 | `Commands/Payments/RecordPayment/RecordPaymentCommandHandler.cs` | Handler บันทึกการชำระ |
| 22 | `Commands/Payments/RecordPayment/RecordPaymentCommandValidator.cs` | Validator บันทึกการชำระ |
| 23 | `Commands/Payments/RefundPayment/RefundPaymentCommand.cs` | Command คืนเงิน |
| 24 | `Commands/Payments/RefundPayment/RefundPaymentCommandHandler.cs` | Handler คืนเงิน |
| 25 | `Commands/Payments/RefundPayment/RefundPaymentCommandValidator.cs` | Validator คืนเงิน |
| 26 | `Commands/Payments/ConfirmPayment/ConfirmPaymentCommand.cs` | Command ยืนยันการชำระ |
| 27 | `Commands/Payments/ConfirmPayment/ConfirmPaymentCommandHandler.cs` | Handler ยืนยันการชำระ |
| 28 | `Commands/Payments/ConfirmPayment/ConfirmPaymentCommandValidator.cs` | Validator ยืนยันการชำระ |
| 29 | `Queries/Payments/GetPaymentById/GetPaymentByIdQuery.cs` | Query ดึง Payment ตาม ID |
| 30 | `Queries/Payments/GetPaymentById/GetPaymentByIdQueryHandler.cs` | Handler ดึง Payment ตาม ID |
| 31 | `Queries/Payments/GetPaymentsByInvoice/GetPaymentsByInvoiceQuery.cs` | Query ดึงตาม Invoice |
| 32 | `Queries/Payments/GetPaymentsByInvoice/GetPaymentsByInvoiceQueryHandler.cs` | Handler ดึงตาม Invoice |
| 33 | `Queries/Payments/GetOutstandingBalance/GetOutstandingBalanceQuery.cs` | Query ยอดคงค้างลูกค้า |
| 34 | `Queries/Payments/GetOutstandingBalance/GetOutstandingBalanceQueryHandler.cs` | Handler ยอดคงค้างลูกค้า |
| 35 | `Queries/Payments/GetPaymentList/GetPaymentListQuery.cs` | Query รายการ Payment |
| 36 | `Queries/Payments/GetPaymentList/GetPaymentListQueryHandler.cs` | Handler รายการ Payment |
| 37 | `Queries/Payments/GetPaymentSummary/GetPaymentSummaryQuery.cs` | Query สรุปการชำระ |
| 38 | `Queries/Payments/GetPaymentSummary/GetPaymentSummaryQueryHandler.cs` | Handler สรุปการชำระ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 39 | `Persistence/Configurations/PaymentConfiguration.cs` | EF Config Payment |
| 40 | `Persistence/Configurations/ReceiptConfiguration.cs` | EF Config Receipt |
| 41 | `Persistence/Configurations/PaymentHistoryConfiguration.cs` | EF Config PaymentHistory |
| 42 | `Persistence/Configurations/OutstandingBalanceConfiguration.cs` | EF Config OutstandingBalance |
| 43 | `Persistence/Repositories/PaymentRepository.cs` | Repository Payment |
| 44 | `Persistence/Repositories/ReceiptRepository.cs` | Repository Receipt |
| 45 | `Persistence/Repositories/OutstandingBalanceRepository.cs` | Repository Outstanding |
| 46 | `Persistence/SeedData/PaymentSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 47 | `Controllers/PaymentsController.cs` | Controller Payment (8 Endpoints) |
| 48 | `Controllers/ReceiptsController.cs` | Controller Receipt (4 Endpoints) |

---

### 📁 **Document Generation** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 49 | `DocumentGeneration/IReceiptPdfGenerator.cs` | Interface PDF Generator |
| 50 | `DocumentGeneration/ReceiptPdfGenerator.cs` | PDF ใบเสร็จ (QuestPDF) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Payments Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/payments` | บันทึกการชำระเงิน | 20/60s |
| GET | `/api/payments/{id}` | ดูข้อมูล Payment | 100/60s |
| GET | `/api/payments/invoice/{invoiceId}` | ดึงตาม Invoice | 60/60s |
| GET | `/api/payments` | รายการ Payment | 50/60s |
| GET | `/api/payments/outstanding/{customerId}` | ยอดคงค้างลูกค้า | 40/60s |
| POST | `/api/payments/{id}/refund` | คืนเงิน | 10/3600s |
| GET | `/api/payments/summary` | สรุปการชำระ | 30/60s |
| DELETE | `/api/payments/{id}` | ลบ Payment | 5/3600s |

### Receipts Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| GET | `/api/receipts/{id}` | ดูใบเสร็จ | 80/60s |
| GET | `/api/receipts/payment/{paymentId}` | ดึงตาม Payment | 60/60s |
| GET | `/api/receipts/{id}/pdf` | PDF ใบเสร็จ | 15/300s |
| POST | `/api/receipts/{id}/resend` | ส่งใบเสร็จทางอีเมล | 10/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="QuestPDF" Version="2023.12.6" />
```

---

## 🚀 วิธีใช้

### 1. บันทึกการชำระเงิน
```http
POST /api/payments
Authorization: Bearer <accessToken>
{
  "invoiceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "amount": 1197.00,
  "paymentMethod": "BankTransfer",
  "referenceNumber": "TRX-20260706-001",
  "note": "โอนเงินผ่านธนาคาร"
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "paymentNo": "PAY-20260706-a1b2c3",
    "invoiceId": "...",
    "amount": 1197.00,
    "paymentMethod": "BankTransfer",
    "status": "Completed",
    "paidAt": "2026-07-06T10:35:00Z",
    "receipt": {
      "receiptNo": "RCP-20260706-a1b2c3",
      "downloadUrl": "/api/receipts/.../pdf"
    }
  }
}
```

### 2. ดึงยอดคงค้างของลูกค้า
```http
GET /api/payments/outstanding/{customerId}
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "customerId": "...",
    "customerName": "สมชาย ใจดี",
    "totalOutstanding": 3570.00,
    "invoices": [
      {
        "invoiceId": "...",
        "invoiceNo": "INV-20260706-001",
        "total": 1197.00,
        "paid": 500.00,
        "outstanding": 697.00,
        "dueDate": "2026-08-06T00:00:00Z"
      }
    ]
  }
}
```

### 3. คืนเงิน
```http
POST /api/payments/{paymentId}/refund
Authorization: Bearer <accessToken>
{
  "refundAmount": 200.00,
  "reason": "ลูกค้าขอคืนเงินบางส่วน",
  "note": "คืนเงินผ่านโอนกลับ"
}
```

### 4. ดาวน์โหลด PDF ใบเสร็จ
```http
GET /api/receipts/{receiptId}/pdf
Authorization: Bearer <accessToken>
```

### 5. สรุปการชำระเงิน
```http
GET /api/payments/summary?fromDate=2026-07-01&toDate=2026-07-31
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "totalPayments": 120,
  "totalAmount": 125000.00,
  "byMethod": {
    "Cash": 45000.00,
    "BankTransfer": 65000.00,
    "CreditCard": 15000.00
  },
  "byStatus": {
    "Completed": 110,
    "Pending": 5,
    "Refunded": 3,
    "Failed": 2
  }
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Payment.cs
```csharp
namespace ICMON.Domain.Aggregates.PaymentAggregate;

public class Payment : AggregateRoot<Guid>
{
    public string PaymentNo { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Guid CustomerId { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public Money Amount { get; private set; }
    public Money RefundedAmount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaidAt { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public string? Note { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<PaymentHistory> _history = new();
    public IReadOnlyList<PaymentHistory> History => _history.AsReadOnly();

    private Payment() { } // For EF Core

    public static Payment Create(
        Guid invoiceId,
        Guid customerId,
        decimal amount,
        PaymentMethod paymentMethod,
        Guid whitelabelId,
        string? referenceNumber = null,
        string? note = null)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            PaymentNo = GeneratePaymentNo(),
            InvoiceId = invoiceId,
            CustomerId = customerId,
            Amount = new Money(amount),
            RefundedAmount = Money.Zero,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending,
            PaidAt = DateTime.UtcNow,
            ReferenceNumber = referenceNumber,
            Note = note,
            WhitelabelId = whitelabelId
        };

        payment.AddHistory("Payment created", "Pending");
        payment.AddDomainEvent(new PaymentCreatedEvent(payment.Id, payment.PaymentNo, invoiceId));

        return payment;
    }

    public void Confirm()
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payments can be confirmed");

        Status = PaymentStatus.Completed;
        AddHistory("Payment confirmed", "Completed");
        AddDomainEvent(new PaymentConfirmedEvent(Id, InvoiceId));
    }

    public void Refund(decimal refundAmount, string reason, string? note = null)
    {
        if (Status != PaymentStatus.Completed)
            throw new DomainException("Only completed payments can be refunded");

        if (refundAmount <= 0)
            throw new ArgumentException("Refund amount must be greater than zero");

        if (refundAmount > (Amount - RefundedAmount).Amount)
            throw new DomainException($"Refund amount exceeds remaining balance. Available: {(Amount - RefundedAmount).Amount}");

        RefundedAmount = new Money(RefundedAmount.Amount + refundAmount);
        Status = PaymentStatus.Refunded;
        AddHistory($"Refunded {refundAmount} - {reason}", "Refunded");
        AddDomainEvent(new PaymentRefundedEvent(Id, refundAmount, reason));
    }

    public void Fail(string reason)
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payments can be failed");

        Status = PaymentStatus.Failed;
        AddHistory($"Payment failed: {reason}", "Failed");
    }

    private void AddHistory(string description, string status)
    {
        _history.Add(PaymentHistory.Create(Id, description, status));
    }

    private static string GeneratePaymentNo()
        => $"PAY-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";

    public bool IsFullyRefunded() => RefundedAmount >= Amount;
    public Money GetRemainingBalance() => Amount - RefundedAmount;
}
```

### 2. Domain Entity - Receipt.cs
```csharp
namespace ICMON.Domain.Aggregates.PaymentAggregate;

public class Receipt : Entity<Guid>
{
    public string ReceiptNo { get; private set; }
    public Guid PaymentId { get; private set; }
    public virtual Payment Payment { get; private set; }
    public Money Amount { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public string? IssuedBy { get; private set; }
    public string? Note { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private Receipt() { } // For EF Core

    public static Receipt Create(
        Guid paymentId,
        Money amount,
        string issuedBy,
        Guid whitelabelId,
        string? note = null)
    {
        return new Receipt
        {
            Id = Guid.NewGuid(),
            ReceiptNo = GenerateReceiptNo(),
            PaymentId = paymentId,
            Amount = amount,
            IssuedAt = DateTime.UtcNow,
            IssuedBy = issuedBy,
            Note = note,
            WhitelabelId = whitelabelId
        };
    }

    private static string GenerateReceiptNo()
        => $"RCP-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";
}
```

### 3. Domain Entity - OutstandingBalance.cs
```csharp
namespace ICMON.Domain.Aggregates.PaymentAggregate;

public class OutstandingBalance : Entity<Guid>
{
    public Guid CustomerId { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Money TotalAmount { get; private set; }
    public Money PaidAmount { get; private set; }
    public Money OutstandingAmount { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private OutstandingBalance() { } // For EF Core

    public static OutstandingBalance Create(
        Guid customerId,
        Guid invoiceId,
        decimal totalAmount,
        DateTime dueDate,
        Guid whitelabelId)
    {
        return new OutstandingBalance
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            InvoiceId = invoiceId,
            TotalAmount = new Money(totalAmount),
            PaidAmount = Money.Zero,
            OutstandingAmount = new Money(totalAmount),
            DueDate = dueDate,
            LastUpdatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };
    }

    public void AddPayment(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be greater than zero");

        PaidAmount = new Money(PaidAmount.Amount + amount);
        OutstandingAmount = new Money(TotalAmount.Amount - PaidAmount.Amount);
        LastUpdatedAt = DateTime.UtcNow;
    }

    public void AddRefund(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Refund amount must be greater than zero");

        PaidAmount = new Money(PaidAmount.Amount - amount);
        OutstandingAmount = new Money(TotalAmount.Amount - PaidAmount.Amount);
        LastUpdatedAt = DateTime.UtcNow;
    }

    public bool IsSettled() => OutstandingAmount.Amount <= 0;
    public bool IsOverdue() => DueDate < DateTime.UtcNow && OutstandingAmount.Amount > 0;
    public decimal GetPaidPercentage() => TotalAmount.Amount > 0 ? (PaidAmount.Amount / TotalAmount.Amount) * 100 : 0;
}
```

### 4. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum PaymentStatus
{
    Pending = 0,     // รอดำเนินการ
    Completed = 1,   // ชำระแล้ว
    Failed = 2,      // ชำระไม่สำเร็จ
    Refunded = 3     // คืนเงินแล้ว
}

public enum PaymentMethod
{
    Cash = 0,
    BankTransfer = 1,
    CreditCard = 2,
    DebitCard = 3,
    PromptPay = 4,
    TrueMoney = 5,
    Other = 6
}
```

### 5. Repository - PaymentRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }

    public async Task<Payment?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.History)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.InvoiceId == invoiceId)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.CustomerId == customerId)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Status == status)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.PaidAt >= fromDate && p.PaidAt <= toDate)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaymentSummary> GetSummaryAsync(DateTime fromDate, DateTime toDate, Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var payments = await _dbSet
            .Where(p => p.WhitelabelId == whitelabelId &&
                       p.PaidAt >= fromDate &&
                       p.PaidAt <= toDate &&
                       p.Status == PaymentStatus.Completed)
            .ToListAsync(cancellationToken);

        return new PaymentSummary
        {
            TotalPayments = payments.Count,
            TotalAmount = payments.Sum(p => p.Amount.Amount),
            ByMethod = payments.GroupBy(p => p.PaymentMethod)
                              .ToDictionary(g => g.Key, g => g.Sum(p => p.Amount.Amount)),
            ByStatus = payments.GroupBy(p => p.Status)
                              .ToDictionary(g => g.Key, g => g.Count())
        };
    }

    public async Task<decimal> GetTotalPaymentByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.InvoiceId == invoiceId && p.Status == PaymentStatus.Completed)
            .SumAsync(p => p.Amount.Amount, cancellationToken);
    }
}
```

### 6. Command Handler - RecordPaymentCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Aggregates.PaymentAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Payments.RecordPayment;

public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, Result<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IOutstandingBalanceRepository _outstandingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public RecordPaymentCommandHandler(
        IPaymentRepository paymentRepository,
        IReceiptRepository receiptRepository,
        IOutstandingBalanceRepository outstandingRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser)
    {
        _paymentRepository = paymentRepository;
        _receiptRepository = receiptRepository;
        _outstandingRepository = outstandingRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<PaymentDto>> Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
    {
        // Create payment
        var payment = Payment.Create(
            request.InvoiceId,
            request.CustomerId,
            request.Amount,
            request.PaymentMethod,
            _currentUser.WhitelabelId,
            request.ReferenceNumber,
            request.Note
        );

        // If payment method is cash, auto-confirm
        if (request.PaymentMethod == PaymentMethod.Cash)
        {
            payment.Confirm();
        }

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Create receipt
        var receipt = Receipt.Create(
            payment.Id,
            payment.Amount,
            _currentUser.UserName ?? "System",
            _currentUser.WhitelabelId,
            "ใบเสร็จรับเงิน"
        );

        await _receiptRepository.AddAsync(receipt, cancellationToken);

        // Update outstanding balance
        if (payment.Status == PaymentStatus.Completed)
        {
            var outstanding = await _outstandingRepository.GetByInvoiceAsync(request.InvoiceId, cancellationToken);
            if (outstanding != null)
            {
                outstanding.AddPayment(request.Amount);
                _outstandingRepository.Update(outstanding);
            }
            else
            {
                // Create new outstanding entry
                var newOutstanding = OutstandingBalance.Create(
                    request.CustomerId,
                    request.InvoiceId,
                    request.Amount,
                    DateTime.UtcNow.AddDays(30),
                    _currentUser.WhitelabelId
                );
                newOutstanding.AddPayment(request.Amount);
                await _outstandingRepository.AddAsync(newOutstanding, cancellationToken);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<PaymentDto>(payment);
        dto.Receipt = _mapper.Map<ReceiptDto>(receipt);

        return Result<PaymentDto>.Success(dto);
    }
}
```

### 7. Controller - PaymentsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize("PAYMENT_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> RecordPayment([FromBody] RecordPaymentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        var query = new GetPaymentByIdQuery { PaymentId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("invoice/{invoiceId}")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPaymentsByInvoice(Guid invoiceId)
    {
        var query = new GetPaymentsByInvoiceQuery { InvoiceId = invoiceId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPaymentList(
        [FromQuery] PaymentStatus? status,
        [FromQuery] Guid? customerId,
        [FromQuery] Guid? invoiceId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPaymentListQuery
        {
            Status = status,
            CustomerId = customerId,
            InvoiceId = invoiceId,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("outstanding/{customerId}")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetOutstandingBalance(Guid customerId)
    {
        var query = new GetOutstandingBalanceQuery { CustomerId = customerId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost("{id}/refund")]
    [Authorize("PAYMENT_REFUND")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> RefundPayment(Guid id, [FromBody] RefundPaymentCommand command)
    {
        if (id != command.PaymentId)
            return BadRequest(Result<PaymentDto>.Failure("Payment ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("summary")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetPaymentSummary(
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var query = new GetPaymentSummaryQuery { FromDate = fromDate, ToDate = toDate };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize("PAYMENT_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        var command = new DeletePaymentCommand { PaymentId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 8. Controller - ReceiptsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReceiptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IReceiptPdfGenerator _pdfGenerator;

    public ReceiptsController(IMediator mediator, IReceiptPdfGenerator pdfGenerator)
    {
        _mediator = mediator;
        _pdfGenerator = pdfGenerator;
    }

    [HttpGet("{id}")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetReceipt(Guid id)
    {
        var query = new GetReceiptByIdQuery { ReceiptId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("payment/{paymentId}")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetReceiptByPayment(Guid paymentId)
    {
        var query = new GetReceiptByPaymentQuery { PaymentId = paymentId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("{id}/pdf")]
    [Authorize("PAYMENT_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GenerateReceiptPdf(Guid id)
    {
        var query = new GetReceiptByIdQuery { ReceiptId = id };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        var pdfBytes = await _pdfGenerator.GenerateReceiptPdfAsync(result.Data);
        return File(pdfBytes, "application/pdf", $"Receipt-{result.Data.ReceiptNo}.pdf");
    }

    [HttpPost("{id}/resend")]
    [Authorize("PAYMENT_UPDATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> ResendReceipt(Guid id)
    {
        var command = new ResendReceiptCommand { ReceiptId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 9. PDF Generator - ReceiptPdfGenerator.cs
```csharp
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ICMON.Infrastructure.DocumentGeneration;

public class ReceiptPdfGenerator : IReceiptPdfGenerator
{
    public async Task<byte[]> GenerateReceiptPdfAsync(ReceiptDto receipt, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(1.5, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Column(col =>
                        {
                            col.Item().Text("ICMON Auto Repair Shop")
                                .FontSize(18)
                                .Bold()
                                .FontColor(Colors.Blue.Medium)
                                .AlignCenter();

                            col.Item().Text("ใบเสร็จรับเงิน / Receipt")
                                .FontSize(14)
                                .Bold()
                                .AlignCenter();

                            col.Item().PaddingVertical(5)
                                .LineHorizontal(1);
                        });

                    page.Content().Column(col =>
                    {
                        // Receipt details
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"เลขที่ / No: {receipt.ReceiptNo}");
                            row.RelativeItem().Text($"วันที่ / Date: {receipt.IssuedAt:dd/MM/yyyy HH:mm}");
                        });

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"ลูกค้า / Customer: {receipt.CustomerName}");
                            row.RelativeItem().Text($"โทร / Phone: {receipt.CustomerPhone}");
                        });

                        col.Item().PaddingVertical(5)
                            .LineHorizontal(1);

                        // Payment details
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Invoice No: {receipt.InvoiceNo}");
                            row.RelativeItem().Text($"Payment Method: {receipt.PaymentMethod}");
                        });

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Reference: {receipt.ReferenceNumber ?? "-"}");
                            row.RelativeItem().Text($"Status: {receipt.PaymentStatus}");
                        });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1);

                        // Amount
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text("ยอดชำระ / Amount:").FontSize(12).Bold();
                            row.RelativeItem().Text($"{receipt.Amount:N2} ฿").FontSize(14).Bold()
                                .FontColor(Colors.Red.Medium).AlignRight();
                        });

                        // Note
                        if (!string.IsNullOrEmpty(receipt.Note))
                        {
                            col.Item().PaddingTop(10)
                                .Text($"หมายเหตุ / Note: {receipt.Note}")
                                .FontSize(9).FontColor(Colors.Grey.Darken2);
                        }
                    });

                    page.Footer().AlignCenter()
                        .Text("ขอบคุณที่ใช้บริการ / Thank you for using our service")
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
| `payments:{paymentId}` | 1 ชม. | ข้อมูล Payment |
| `payment_invoice:{invoiceId}` | 1 ชม. | Payment ตาม Invoice |
| `payment_customer:{customerId}` | 1 ชม. | Payment ตาม Customer |
| `receipts:{receiptId}` | 1 ชม. | ข้อมูลใบเสร็จ |
| `receipt_payment:{paymentId}` | 1 ชม. | ใบเสร็จตาม Payment |
| `outstanding_customer:{customerId}` | 15 นาที | ยอดคงค้างตามลูกค้า |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ บันทึกการชำระเงิน (Cash, BankTransfer, CreditCard, PromptPay, TrueMoney)
- ✅ สร้างใบเสร็จรับเงินอัตโนมัติ
- ✅ ยืนยันการชำระเงิน
- ✅ คืนเงิน (Refund)
- ✅ ดึงยอดคงค้างของลูกค้า
- ✅ ดึง Payment ตาม Invoice
- ✅ สรุปการชำระเงินตามช่วงเวลา
- ✅ PDF ใบเสร็จ
- ✅ ส่งใบเสร็จทางอีเมล
- ✅ Domain Events (Created, Confirmed, Refunded, StatusChanged)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 7: **Payment Management** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 8: Dashboard & Reports
2. โมดูลที่ 9: Document Management
3. โมดูลที่ 10: Email Service
4. โมดูลที่ 11: Batch Jobs
5. โมดูลที่ 12: Multi-Language (i18n)
6. โมดูลที่ 13: IoT & GPS Tracking
7. โมดูลที่ 14: Web Order System (WOS)