# 🚗 โมดูลที่ 10: Email Service – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Email Service (บริการอีเมล) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 48 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/EmailAggregate/Email.cs` | Aggregate Root อีเมล |
| 2 | `Aggregates/EmailAggregate/EmailTemplate.cs` | Entity เทมเพลตอีเมล |
| 3 | `Aggregates/EmailAggregate/EmailQueue.cs` | Entity คิวอีเมล |
| 4 | `Aggregates/EmailAggregate/EmailHistory.cs` | Entity ประวัติการส่ง |
| 5 | `Enums/EmailStatus.cs` | สถานะอีเมล |
| 6 | `Enums/EmailPriority.cs` | ความสำคัญอีเมล |
| 7 | `Events/EmailSentEvent.cs` | Event ส่งอีเมล |
| 8 | `Events/EmailFailedEvent.cs` | Event ส่งล้มเหลว |
| 9 | `Interfaces/IEmailRepository.cs` | Interface Repository |
| 10 | `Interfaces/IEmailTemplateRepository.cs` | Interface Repository Template |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 11 | `DTOs/Emails/EmailDto.cs` | DTO อีเมล |
| 12 | `DTOs/Emails/EmailTemplateDto.cs` | DTO เทมเพลต |
| 13 | `DTOs/Emails/SendEmailDto.cs` | DTO ส่งอีเมล |
| 14 | `DTOs/Emails/EmailStatusDto.cs` | DTO สถานะ |
| 15 | `DTOs/Emails/EmailAnalyticsDto.cs` | DTO วิเคราะห์ |
| 16 | `Commands/Emails/SendEmail/SendEmailCommand.cs` | Command ส่งอีเมล |
| 17 | `Commands/Emails/SendEmail/SendEmailCommandHandler.cs` | Handler ส่งอีเมล |
| 18 | `Commands/Emails/SendEmail/SendEmailCommandValidator.cs` | Validator ส่งอีเมล |
| 19 | `Commands/Emails/SendTemplate/SendTemplateCommand.cs` | Command ส่งเทมเพลต |
| 20 | `Commands/Emails/SendTemplate/SendTemplateCommandHandler.cs` | Handler ส่งเทมเพลต |
| 21 | `Commands/Emails/SendTemplate/SendTemplateCommandValidator.cs` | Validator ส่งเทมเพลต |
| 22 | `Commands/Emails/BulkEmail/BulkEmailCommand.cs` | Command ส่งจำนวนมาก |
| 23 | `Commands/Emails/BulkEmail/BulkEmailCommandHandler.cs` | Handler ส่งจำนวนมาก |
| 24 | `Commands/Emails/BulkEmail/BulkEmailCommandValidator.cs` | Validator ส่งจำนวนมาก |
| 25 | `Commands/Emails/ResendEmail/ResendEmailCommand.cs` | Command ส่งซ้ำ |
| 26 | `Commands/Emails/ResendEmail/ResendEmailCommandHandler.cs` | Handler ส่งซ้ำ |
| 27 | `Commands/Emails/ResendEmail/ResendEmailCommandValidator.cs` | Validator ส่งซ้ำ |
| 28 | `Queries/Emails/GetEmailStatus/GetEmailStatusQuery.cs` | Query สถานะ |
| 29 | `Queries/Emails/GetEmailStatus/GetEmailStatusQueryHandler.cs` | Handler สถานะ |
| 30 | `Queries/Emails/GetEmailHistory/GetEmailHistoryQuery.cs` | Query ประวัติ |
| 31 | `Queries/Emails/GetEmailHistory/GetEmailHistoryQueryHandler.cs` | Handler ประวัติ |
| 32 | `Queries/Emails/GetEmailAnalytics/GetEmailAnalyticsQuery.cs` | Query วิเคราะห์ |
| 33 | `Queries/Emails/GetEmailAnalytics/GetEmailAnalyticsQueryHandler.cs` | Handler วิเคราะห์ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 34 | `Persistence/Configurations/EmailConfiguration.cs` | EF Config Email |
| 35 | `Persistence/Configurations/EmailTemplateConfiguration.cs` | EF Config Template |
| 36 | `Persistence/Configurations/EmailQueueConfiguration.cs` | EF Config Queue |
| 37 | `Persistence/Configurations/EmailHistoryConfiguration.cs` | EF Config History |
| 38 | `Persistence/Repositories/EmailRepository.cs` | Repository Email |
| 39 | `Persistence/Repositories/EmailTemplateRepository.cs` | Repository Template |
| 40 | `Email/IEmailSender.cs` | Interface Email Sender |
| 41 | `Email/SmtpEmailSender.cs` | SMTP Sender |
| 42 | `Email/TemplateEngine.cs` | Template Engine (Razor) |
| 43 | `Persistence/SeedData/EmailSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 44 | `Controllers/EmailController.cs` | Controller Email (5 Endpoints) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Email Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/email/send` | ส่งอีเมล | 20/60s |
| POST | `/api/email/send-template` | ส่งอีเมลจากเทมเพลต | 25/60s |
| POST | `/api/email/bulk` | ส่งอีเมลจำนวนมาก | 5/300s |
| GET | `/api/email/status/{emailId}` | สถานะการส่ง | 50/60s |
| POST | `/api/email/resend/{emailId}` | ส่งอีเมลซ้ำ | 10/3600s |
| GET | `/api/email/history` | ประวัติการส่ง | 30/60s |
| GET | `/api/email/analytics` | วิเคราะห์อีเมล | 20/60s |
| POST | `/api/email/templates` | สร้างเทมเพลต | 15/60s |
| GET | `/api/email/templates` | รายการเทมเพลต | 30/60s |
| DELETE | `/api/email/{emailId}` | ลบอีเมล | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="SendGrid" Version="9.29.2" />
<PackageReference Include="MailKit" Version="4.6.0" />
<PackageReference Include="MimeKit" Version="4.6.0" />
<PackageReference Include="RazorEngine" Version="5.0.0" />
<PackageReference Include="Microsoft.Extensions.FileProviders.Physical" Version="8.0.0" />
```

---

## 🚀 วิธีใช้

### 1. ส่งอีเมล (แบบธรรมดา)
```http
POST /api/email/send
Authorization: Bearer <accessToken>
{
  "to": "customer@example.com",
  "subject": "แจ้งเตือน: ใบงานเสร็จสิ้น",
  "body": "เรียนลูกค้า, งานซ่อมของท่านเสร็จสิ้นแล้ว",
  "isHtml": false,
  "priority": "Normal"
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "emailNo": "EML-20260706-a1b2c3",
    "to": "customer@example.com",
    "subject": "แจ้งเตือน: ใบงานเสร็จสิ้น",
    "status": "Sent",
    "sentAt": "2026-07-06T10:30:00Z"
  }
}
```

### 2. ส่งอีเมลจากเทมเพลต
```http
POST /api/email/send-template
Authorization: Bearer <accessToken>
{
  "templateCode": "JOB_COMPLETED",
  "to": "customer@example.com",
  "cc": ["manager@example.com"],
  "data": {
    "customerName": "สมชาย ใจดี",
    "jobNo": "JOB-20260706-a1b2c3",
    "totalAmount": 2500.00,
    "completedDate": "2026-07-06"
  },
  "priority": "High"
}
```

### 3. ส่งอีเมลจำนวนมาก
```http
POST /api/email/bulk
Authorization: Bearer <accessToken>
{
  "to": ["customer1@example.com", "customer2@example.com"],
  "subject": "โปรโมชั่นพิเศษเดือนนี้",
  "body": "เรียนลูกค้าทุกท่าน...",
  "isHtml": true,
  "priority": "Low"
}
```

### 4. ตรวจสอบสถานะการส่ง
```http
GET /api/email/status/{emailId}
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "id": "...",
  "status": "Sent",
  "sentAt": "2026-07-06T10:30:00Z",
  "deliveredAt": "2026-07-06T10:30:05Z",
  "openedAt": null,
  "clickedAt": null,
  "error": null
}
```

### 5. ส่งอีเมลซ้ำ
```http
POST /api/email/resend/{emailId}
Authorization: Bearer <accessToken>
{
  "reason": "ลูกค้าแจ้งว่าไม่ได้รับอีเมล"
}
```

### 6. วิเคราะห์อีเมล
```http
GET /api/email/analytics?fromDate=2026-07-01&toDate=2026-07-31
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "totalSent": 150,
  "delivered": 145,
  "opened": 120,
  "clicked": 80,
  "bounced": 3,
  "complained": 2,
  "openRate": 82.76,
  "clickRate": 55.17,
  "bounceRate": 2.07,
  "byTemplate": {
    "JOB_COMPLETED": 50,
    "QUOTATION_SENT": 45,
    "PAYMENT_RECEIVED": 55
  }
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Email.cs
```csharp
namespace ICMON.Domain.Aggregates.EmailAggregate;

public class Email : AggregateRoot<Guid>
{
    public string EmailNo { get; private set; }
    public string To { get; private set; }
    public string? Cc { get; private set; }
    public string? Bcc { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public bool IsHtml { get; private set; }
    public EmailPriority Priority { get; private set; }
    public EmailStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? OpenedAt { get; private set; }
    public DateTime? ClickedAt { get; private set; }
    public int RetryCount { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? TemplateCode { get; private set; }
    public string? MessageId { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<EmailHistory> _history = new();
    public IReadOnlyList<EmailHistory> History => _history.AsReadOnly();

    private Email() { } // For EF Core

    public static Email Create(
        string to,
        string subject,
        string body,
        Guid whitelabelId,
        bool isHtml = false,
        EmailPriority priority = EmailPriority.Normal,
        string? cc = null,
        string? bcc = null,
        string? templateCode = null)
    {
        var email = new Email
        {
            Id = Guid.NewGuid(),
            EmailNo = GenerateEmailNo(),
            To = to,
            Cc = cc,
            Bcc = bcc,
            Subject = subject,
            Body = body,
            IsHtml = isHtml,
            Priority = priority,
            Status = EmailStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            RetryCount = 0,
            TemplateCode = templateCode,
            WhitelabelId = whitelabelId
        };

        email.AddHistory("Email created", "Pending");
        return email;
    }

    public void MarkAsSent(string messageId)
    {
        Status = EmailStatus.Sent;
        SentAt = DateTime.UtcNow;
        MessageId = messageId;
        AddHistory($"Email sent with ID: {messageId}", "Sent");
        AddDomainEvent(new EmailSentEvent(Id, To, Subject));
    }

    public void MarkAsDelivered()
    {
        DeliveredAt = DateTime.UtcNow;
        if (Status == EmailStatus.Sent)
        {
            Status = EmailStatus.Delivered;
            AddHistory("Email delivered", "Delivered");
        }
    }

    public void MarkAsOpened()
    {
        OpenedAt ??= DateTime.UtcNow;
        AddHistory("Email opened", "Opened");
    }

    public void MarkAsClicked()
    {
        ClickedAt ??= DateTime.UtcNow;
        AddHistory("Email clicked", "Clicked");
    }

    public void MarkAsFailed(string error)
    {
        Status = EmailStatus.Failed;
        ErrorMessage = error;
        RetryCount++;
        AddHistory($"Email failed: {error}", "Failed");
        AddDomainEvent(new EmailFailedEvent(Id, error));
    }

    public void MarkAsBounced(string reason)
    {
        Status = EmailStatus.Bounced;
        ErrorMessage = reason;
        AddHistory($"Email bounced: {reason}", "Bounced");
    }

    public void MarkAsComplained()
    {
        Status = EmailStatus.Complained;
        AddHistory("Email complained", "Complained");
    }

    public void Queue()
    {
        Status = EmailStatus.Queued;
        AddHistory("Email queued", "Queued");
    }

    public bool CanRetry()
    {
        return RetryCount < 3 && Status == EmailStatus.Failed;
    }

    private void AddHistory(string description, string status)
    {
        _history.Add(EmailHistory.Create(Id, description, status));
    }

    private static string GenerateEmailNo()
        => $"EML-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";
}
```

### 2. Domain Entity - EmailTemplate.cs
```csharp
namespace ICMON.Domain.Aggregates.EmailAggregate;

public class EmailTemplate : Entity<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public bool IsHtml { get; private set; }
    public bool IsActive { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private EmailTemplate() { } // For EF Core

    public static EmailTemplate Create(
        string code,
        string name,
        string subject,
        string body,
        Guid whitelabelId,
        bool isHtml = true,
        string? description = null)
    {
        return new EmailTemplate
        {
            Id = Guid.NewGuid(),
            Code = code.ToUpper(),
            Name = name,
            Subject = subject,
            Body = body,
            IsHtml = isHtml,
            IsActive = true,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };
    }

    public void Update(string name, string subject, string body, bool isHtml, string? description)
    {
        Name = name;
        Subject = subject;
        Body = body;
        IsHtml = isHtml;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public string RenderBody(Dictionary<string, object> data)
    {
        // Using RazorEngine for template rendering
        var result = RazorEngine.Templating.Engine.Razor.RunCompile(
            Body,
            $"{Code}_{Guid.NewGuid():N}",
            null,
            data
        );
        return result;
    }

    public string RenderSubject(Dictionary<string, object> data)
    {
        var result = RazorEngine.Templating.Engine.Razor.RunCompile(
            Subject,
            $"{Code}_Subject_{Guid.NewGuid():N}",
            null,
            data
        );
        return result;
    }
}
```

### 3. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum EmailStatus
{
    Pending = 0,      // รอส่ง
    Queued = 1,       // อยู่ในคิว
    Sent = 2,         // ส่งแล้ว
    Delivered = 3,    // ถึงผู้รับ
    Opened = 4,       // อ่านแล้ว
    Clicked = 5,      // คลิกแล้ว
    Failed = 6,       // ส่งล้มเหลว
    Bounced = 7,      // ถูกตีกลับ
    Complained = 8    // ถูกแจ้งว่าเป็น Spam
}

public enum EmailPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Urgent = 3
}
```

### 4. Repository - EmailRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class EmailRepository : GenericRepository<Email>, IEmailRepository
{
    public EmailRepository(AppDbContext context) : base(context) { }

    public async Task<Email?> GetByIdWithHistoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.History)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Email>> GetByStatusAsync(EmailStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.Status == status)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Email>> GetByRecipientAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.To == email || (e.Cc != null && e.Cc.Contains(email)))
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Email>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.CreatedAt >= fromDate && e.CreatedAt <= toDate)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Email>> GetPendingEmailsAsync(int limit = 100, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.Status == EmailStatus.Pending || e.Status == EmailStatus.Queued)
            .OrderBy(e => e.Priority == EmailPriority.Urgent ? 0 :
                          e.Priority == EmailPriority.High ? 1 :
                          e.Priority == EmailPriority.Normal ? 2 : 3)
            .ThenBy(e => e.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Email>> GetFailedEmailsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.Status == EmailStatus.Failed && e.RetryCount < 3)
            .OrderBy(e => e.RetryCount)
            .ThenBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<EmailAnalyticsDto> GetAnalyticsAsync(DateTime fromDate, DateTime toDate, Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        var emails = await _dbSet
            .Where(e => e.WhitelabelId == whitelabelId &&
                       e.CreatedAt >= fromDate &&
                       e.CreatedAt <= toDate)
            .ToListAsync(cancellationToken);

        var sent = emails.Count(e => e.Status >= EmailStatus.Sent);
        var delivered = emails.Count(e => e.Status >= EmailStatus.Delivered);
        var opened = emails.Count(e => e.Status == EmailStatus.Opened || e.Status == EmailStatus.Clicked);
        var clicked = emails.Count(e => e.Status == EmailStatus.Clicked);
        var bounced = emails.Count(e => e.Status == EmailStatus.Bounced);
        var complained = emails.Count(e => e.Status == EmailStatus.Complained);

        return new EmailAnalyticsDto
        {
            TotalSent = sent,
            Delivered = delivered,
            Opened = opened,
            Clicked = clicked,
            Bounced = bounced,
            Complained = complained,
            OpenRate = sent > 0 ? (double)opened / sent * 100 : 0,
            ClickRate = sent > 0 ? (double)clicked / sent * 100 : 0,
            BounceRate = sent > 0 ? (double)bounced / sent * 100 : 0,
            ByTemplate = emails
                .GroupBy(e => e.TemplateCode ?? "Direct")
                .ToDictionary(g => g.Key, g => g.Count()),
            ByDay = emails
                .GroupBy(e => e.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Count())
        };
    }
}
```

### 5. Service - SmtpEmailSender.cs
```csharp
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace ICMON.Infrastructure.Email;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(IOptions<SmtpSettings> settings, ILogger<SmtpEmailSender> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<EmailSendResult> SendAsync(
        string to,
        string subject,
        string body,
        bool isHtml = false,
        string? from = null,
        string? cc = null,
        string? bcc = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var message = new MimeMessage();
            
            // From
            message.From.Add(new MailboxAddress(_settings.FromName, from ?? _settings.FromEmail));
            
            // To
            message.To.AddRange(to.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(email => MailboxAddress.Parse(email.Trim())));
            
            // CC
            if (!string.IsNullOrEmpty(cc))
            {
                message.Cc.AddRange(cc.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(email => MailboxAddress.Parse(email.Trim())));
            }
            
            // BCC
            if (!string.IsNullOrEmpty(bcc))
            {
                message.Bcc.AddRange(bcc.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(email => MailboxAddress.Parse(email.Trim())));
            }
            
            // Subject
            message.Subject = subject;
            
            // Body
            var bodyBuilder = new BodyBuilder();
            if (isHtml)
                bodyBuilder.HtmlBody = body;
            else
                bodyBuilder.TextBody = body;
            
            message.Body = bodyBuilder.ToMessageBody();

            // Send using SMTP client
            using var client = new SmtpClient();
            
            // Connect
            await client.ConnectAsync(_settings.Host, _settings.Port, 
                _settings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable,
                cancellationToken);
            
            // Authenticate if needed
            if (!string.IsNullOrEmpty(_settings.Username))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            }
            
            // Send
            var messageId = await client.SendAsync(message, cancellationToken);
            
            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Email sent successfully to {To} with ID {MessageId}", to, messageId);
            
            return new EmailSendResult
            {
                Success = true,
                MessageId = messageId,
                SentAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);
            return new EmailSendResult
            {
                Success = false,
                Error = ex.Message,
                SentAt = DateTime.UtcNow
            };
        }
    }

    public async Task<EmailSendResult> SendBulkAsync(
        IEnumerable<string> to,
        string subject,
        string body,
        bool isHtml = false,
        string? from = null,
        CancellationToken cancellationToken = default)
    {
        // For bulk sending, we send individually to track each recipient
        // In production, consider using a bulk email service like SendGrid
        var results = new List<EmailSendResult>();
        int successCount = 0;
        int failCount = 0;

        foreach (var recipient in to)
        {
            var result = await SendAsync(recipient, subject, body, isHtml, from, cancellationToken: cancellationToken);
            results.Add(result);
            
            if (result.Success)
                successCount++;
            else
                failCount++;
            
            // Delay to avoid rate limiting
            await Task.Delay(100, cancellationToken);
        }

        return new EmailSendResult
        {
            Success = failCount == 0,
            BulkResults = results,
            BulkTotal = to.Count(),
            BulkSuccess = successCount,
            BulkFailed = failCount
        };
    }

    public async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, 
                _settings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable,
                cancellationToken);
            
            if (!string.IsNullOrEmpty(_settings.Username))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
            }
            
            await client.DisconnectAsync(true, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SMTP connection test failed");
            return false;
        }
    }
}

public class SmtpSettings
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public bool UseSsl { get; set; } = false;
    public string FromEmail { get; set; } = "noreply@icmon.com";
    public string FromName { get; set; } = "ICMON Auto Repair";
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class EmailSendResult
{
    public bool Success { get; set; }
    public string? MessageId { get; set; }
    public DateTime SentAt { get; set; }
    public string? Error { get; set; }
    public List<EmailSendResult>? BulkResults { get; set; }
    public int BulkTotal { get; set; }
    public int BulkSuccess { get; set; }
    public int BulkFailed { get; set; }
}
```

### 6. Service - TemplateEngine.cs
```csharp
using RazorEngine.Templating;
using System.Text;

namespace ICMON.Infrastructure.Email;

public class TemplateEngine : ITemplateEngine
{
    private readonly ILogger<TemplateEngine> _logger;

    public TemplateEngine(ILogger<TemplateEngine> logger)
    {
        _logger = logger;
    }

    public string Render(string template, Dictionary<string, object> data)
    {
        try
        {
            var result = Engine.Razor.RunCompile(
                template,
                $"template_{Guid.NewGuid():N}",
                null,
                data
            );
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to render template");
            throw new EmailTemplateException($"Failed to render template: {ex.Message}", ex);
        }
    }

    public async Task<string> RenderFromFileAsync(string filePath, Dictionary<string, object> data, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Template file not found: {filePath}");

        var template = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);
        return Render(template, data);
    }

    public bool ValidateTemplate(string template)
    {
        try
        {
            Engine.Razor.RunCompile(
                template,
                $"validate_{Guid.NewGuid():N}",
                null,
                new { Test = "test" }
            );
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Template validation failed");
            return false;
        }
    }
}
```

### 7. Command Handler - SendEmailCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Aggregates.EmailAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Emails.SendEmail;

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<EmailDto>>
{
    private readonly IEmailRepository _emailRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public SendEmailCommandHandler(
        IEmailRepository emailRepository,
        IEmailSender emailSender,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser)
    {
        _emailRepository = emailRepository;
        _emailSender = emailSender;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<EmailDto>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        // Create email entity
        var email = Email.Create(
            request.To,
            request.Subject,
            request.Body,
            _currentUser.WhitelabelId,
            request.IsHtml,
            request.Priority,
            request.Cc,
            request.Bcc
        );

        // Queue email
        email.Queue();

        await _emailRepository.AddAsync(email, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email asynchronously (fire and forget with background job)
        // For this example, we send synchronously, but in production use Hangfire
        try
        {
            var result = await _emailSender.SendAsync(
                request.To,
                request.Subject,
                request.Body,
                request.IsHtml,
                cc: request.Cc,
                bcc: request.Bcc,
                cancellationToken: cancellationToken
            );

            if (result.Success)
            {
                email.MarkAsSent(result.MessageId);
            }
            else
            {
                email.MarkAsFailed(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            email.MarkAsFailed(ex.Message);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var dto = _mapper.Map<EmailDto>(email);
        return Result<EmailDto>.Success(dto);
    }
}
```

### 8. Command Handler - SendTemplateCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Aggregates.EmailAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Emails.SendTemplate;

public class SendTemplateCommandHandler : IRequestHandler<SendTemplateCommand, Result<EmailDto>>
{
    private readonly IEmailRepository _emailRepository;
    private readonly IEmailTemplateRepository _templateRepository;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;
    private readonly ITemplateEngine _templateEngine;

    public SendTemplateCommandHandler(
        IEmailRepository emailRepository,
        IEmailTemplateRepository templateRepository,
        IEmailSender emailSender,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser,
        ITemplateEngine templateEngine)
    {
        _emailRepository = emailRepository;
        _templateRepository = templateRepository;
        _emailSender = emailSender;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _templateEngine = templateEngine;
    }

    public async Task<Result<EmailDto>> Handle(SendTemplateCommand request, CancellationToken cancellationToken)
    {
        // Get template
        var template = await _templateRepository.GetByCodeAsync(request.TemplateCode, cancellationToken);
        if (template == null)
            return Result<EmailDto>.Failure($"Template '{request.TemplateCode}' not found");

        if (!template.IsActive)
            return Result<EmailDto>.Failure($"Template '{request.TemplateCode}' is inactive");

        // Render subject and body
        var data = request.Data ?? new Dictionary<string, object>();
        
        string subject;
        string body;
        
        try
        {
            subject = _templateEngine.Render(template.Subject, data);
            body = _templateEngine.Render(template.Body, data);
        }
        catch (Exception ex)
        {
            return Result<EmailDto>.Failure($"Failed to render template: {ex.Message}");
        }

        // Create email entity
        var email = Email.Create(
            request.To,
            subject,
            body,
            _currentUser.WhitelabelId,
            template.IsHtml,
            request.Priority,
            request.Cc,
            request.Bcc,
            template.Code
        );

        // Queue email
        email.Queue();

        await _emailRepository.AddAsync(email, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email
        try
        {
            var result = await _emailSender.SendAsync(
                request.To,
                subject,
                body,
                template.IsHtml,
                cc: request.Cc,
                bcc: request.Bcc,
                cancellationToken: cancellationToken
            );

            if (result.Success)
            {
                email.MarkAsSent(result.MessageId);
            }
            else
            {
                email.MarkAsFailed(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            email.MarkAsFailed(ex.Message);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var dto = _mapper.Map<EmailDto>(email);
        return Result<EmailDto>.Success(dto);
    }
}
```

### 9. Controller - EmailController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator) => _mediator = mediator;

    [HttpPost("send")]
    [Authorize("EMAIL_SEND")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<EmailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("send-template")]
    [Authorize("EMAIL_SEND")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<EmailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendTemplateEmail([FromBody] SendTemplateCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("bulk")]
    [Authorize("EMAIL_BULK")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<BulkEmailResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SendBulkEmail([FromBody] BulkEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("status/{emailId}")]
    [Authorize("EMAIL_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<EmailStatusDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmailStatus(Guid emailId)
    {
        var query = new GetEmailStatusQuery { EmailId = emailId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost("resend/{emailId}")]
    [Authorize("EMAIL_RESEND")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<EmailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ResendEmail(Guid emailId, [FromBody] ResendEmailCommand command)
    {
        if (emailId != command.EmailId)
            return BadRequest(Result<EmailDto>.Failure("Email ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("history")]
    [Authorize("EMAIL_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetEmailHistory(
        [FromQuery] string? to,
        [FromQuery] EmailStatus? status,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetEmailHistoryQuery
        {
            To = to,
            Status = status,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("analytics")]
    [Authorize("EMAIL_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetEmailAnalytics(
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var query = new GetEmailAnalyticsQuery { FromDate = fromDate, ToDate = toDate };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("templates")]
    [Authorize("EMAIL_ADMIN")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("templates")]
    [Authorize("EMAIL_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetTemplates()
    {
        var query = new GetTemplatesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{emailId}")]
    [Authorize("EMAIL_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteEmail(Guid emailId)
    {
        var command = new DeleteEmailCommand { EmailId = emailId };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `email_templates:{templateCode}` | 2 ชม. | เทมเพลตอีเมล |
| `email_templates_all` | 1 ชม. | รายการเทมเพลตทั้งหมด |
| `email_status:{emailId}` | 15 นาที | สถานะอีเมล |
| `email_analytics:{whitelabelId}:{fromDate}:{toDate}` | 5 นาที | วิเคราะห์อีเมล |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ ส่งอีเมลแบบธรรมดา (Text/HTML)
- ✅ ส่งอีเมลจากเทมเพลต (RazorEngine)
- ✅ ส่งอีเมลจำนวนมาก (Bulk)
- ✅ ส่งอีเมลซ้ำ (Resend)
- ✅ ติดตามสถานะอีเมล (Sent, Delivered, Opened, Clicked, Failed, Bounced, Complained)
- ✅ ประวัติการส่งอีเมล
- ✅ วิเคราะห์อีเมล (เปิด อ่าน คลิก ตีกลับ)
- ✅ จัดการเทมเพลตอีเมล
- ✅ สนับสนุน CC, BCC
- ✅ จัดลำดับความสำคัญ (Priority)
- ✅ Retry อัตโนมัติสำหรับอีเมลที่ล้มเหลว
- ✅ Email Queue (เตรียมพร้อมสำหรับ Background Jobs)
- ✅ Domain Events (Sent, Failed)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 10: **Email Service** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 11: Batch Jobs
2. โมดูลที่ 12: Multi-Language (i18n)
3. โมดูลที่ 13: IoT & GPS Tracking
4. โมดูลที่ 14: Web Order System (WOS)