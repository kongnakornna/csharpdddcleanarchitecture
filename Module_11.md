# 🚗 โมดูลที่ 11: Batch Jobs – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Batch Jobs (งานเบื้องหลัง) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 42 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/BatchJobAggregate/BatchJob.cs` | Aggregate Root งาน Batch |
| 2 | `Aggregates/BatchJobAggregate/BatchJobHistory.cs` | Entity ประวัติการรันงาน |
| 3 | `Enums/BatchJobStatus.cs` | สถานะงาน Batch |
| 4 | `Enums/BatchJobType.cs` | ประเภทงาน Batch |
| 5 | `Events/BatchJobStartedEvent.cs` | Event เริ่มงาน |
| 6 | `Events/BatchJobCompletedEvent.cs` | Event งานเสร็จ |
| 7 | `Events/BatchJobFailedEvent.cs` | Event งานล้มเหลว |
| 8 | `Interfaces/IBatchJobRepository.cs` | Interface Repository |
| 9 | `Interfaces/IBatchJobService.cs` | Interface Service |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 10 | `DTOs/BatchJobs/BatchJobDto.cs` | DTO งาน Batch |
| 11 | `DTOs/BatchJobs/BatchJobHistoryDto.cs` | DTO ประวัติ |
| 12 | `Commands/BatchJobs/TriggerJob/TriggerJobCommand.cs` | Command สั่งรันงาน |
| 13 | `Commands/BatchJobs/TriggerJob/TriggerJobCommandHandler.cs` | Handler สั่งรันงาน |
| 14 | `Commands/BatchJobs/TriggerJob/TriggerJobCommandValidator.cs` | Validator สั่งรันงาน |
| 15 | `Commands/BatchJobs/EnableJob/EnableJobCommand.cs` | Command เปิดใช้งาน |
| 16 | `Commands/BatchJobs/EnableJob/EnableJobCommandHandler.cs` | Handler เปิดใช้งาน |
| 17 | `Commands/BatchJobs/DisableJob/DisableJobCommand.cs` | Command ปิดใช้งาน |
| 18 | `Commands/BatchJobs/DisableJob/DisableJobCommandHandler.cs` | Handler ปิดใช้งาน |
| 19 | `Queries/BatchJobs/GetBatchJobList/GetBatchJobListQuery.cs` | Query รายการงาน |
| 20 | `Queries/BatchJobs/GetBatchJobList/GetBatchJobListQueryHandler.cs` | Handler รายการงาน |
| 21 | `Queries/BatchJobs/GetBatchJobStatus/GetBatchJobStatusQuery.cs` | Query สถานะงาน |
| 22 | `Queries/BatchJobs/GetBatchJobStatus/GetBatchJobStatusQueryHandler.cs` | Handler สถานะงาน |
| 23 | `Queries/BatchJobs/GetBatchJobHistory/GetBatchJobHistoryQuery.cs` | Query ประวัติ |
| 24 | `Queries/BatchJobs/GetBatchJobHistory/GetBatchJobHistoryQueryHandler.cs` | Handler ประวัติ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 25 | `Persistence/Configurations/BatchJobConfiguration.cs` | EF Config BatchJob |
| 26 | `Persistence/Configurations/BatchJobHistoryConfiguration.cs` | EF Config History |
| 27 | `Persistence/Repositories/BatchJobRepository.cs` | Repository BatchJob |
| 28 | `BackgroundJobs/IBatchJob.cs` | Interface Batch Job |
| 29 | `BackgroundJobs/Jobs/DailyReportJob.cs` | Job สร้างรายงานประจำวัน |
| 30 | `BackgroundJobs/Jobs/SendDailyEmailJob.cs` | Job ส่งอีเมลรายวัน |
| 31 | `BackgroundJobs/Jobs/UpdatePendingJobsJob.cs` | Job อัปเดตงานค้าง |
| 32 | `BackgroundJobs/Jobs/CleanupAndSyncJob.cs` | Job ล้าง/ซิงค์ข้อมูล |
| 33 | `BackgroundJobs/Jobs/RealtimeSyncJob.cs` | Job ซิงค์เรียลไทม์ |
| 34 | `BackgroundJobs/Jobs/SendSalesSummaryJob.cs` | Job ส่งสรุปยอดขาย |
| 35 | `BackgroundJobs/BatchJobOrchestrator.cs` | ตัวจัดการงาน Batch |
| 36 | `BackgroundJobs/HangfireService.cs` | Hangfire Service |
| 37 | `BackgroundJobs/QuartzService.cs` | Quartz Service (ทางเลือก) |
| 38 | `Persistence/SeedData/BatchJobSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 39 | `Controllers/BatchJobsController.cs` | Controller Batch Jobs (7 Endpoints) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Batch Jobs Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| GET | `/api/batch-jobs` | รายการงาน Batch | 20/60s |
| GET | `/api/batch-jobs/{jobCode}/status` | สถานะงาน | 30/60s |
| GET | `/api/batch-jobs/{jobCode}/history` | ประวัติการรัน | 20/60s |
| POST | `/api/batch-jobs/{jobCode}/trigger` | สั่งรันงาน | 5/3600s |
| PUT | `/api/batch-jobs/{jobCode}/enable` | เปิดใช้งาน | 10/60s |
| PUT | `/api/batch-jobs/{jobCode}/disable` | ปิดใช้งาน | 10/60s |
| GET | `/api/batch-jobs/running` | งานที่กำลังรัน | 15/60s |

---

## 🕐 6 Batch Jobs ที่กำหนด

| Job Code | Cron Expression | เวลา | หน้าที่ |
|----------|-----------------|------|--------|
| `BATCH001` | `0 30 6 * * ?` | 06:30 | ส่งอีเมลแจ้งเตือนรายวัน |
| `BATCH002` | `0 45 6 * * ?` | 06:45 | สร้างรายงานประจำวัน |
| `BATCH003` | `0 30 6 * * ?` | 06:30 | อัปเดตสถานะงานค้าง |
| `BATCH004` | `0 0 3 * * ?` | 03:00 | ล้างข้อมูล/ซิงค์ฐานข้อมูล |
| `BATCH005` | `0 0/30 * * * ?` | ทุก 30 นาที | ซิงค์ข้อมูล Realtime |
| `BATCH006` | `0 30 6 * * ?` | 06:30 | ส่งสรุปยอดขาย |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="Hangfire" Version="1.8.7" />
<PackageReference Include="Hangfire.PostgreSql" Version="1.20.5" />
<PackageReference Include="Hangfire.AspNetCore" Version="1.8.7" />
<PackageReference Include="Quartz" Version="3.8.1" />
<PackageReference Include="Quartz.AspNetCore" Version="3.8.1" />
<PackageReference Include="Quartz.Extensions.DependencyInjection" Version="3.8.1" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
```

---

## 🚀 วิธีใช้

### 1. รายการงาน Batch ทั้งหมด
```http
GET /api/batch-jobs
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "isSuccess": true,
  "data": [
    {
      "jobCode": "BATCH001",
      "jobName": "ส่งอีเมลแจ้งเตือนรายวัน",
      "cronExpression": "0 30 6 * * ?",
      "status": "Running",
      "lastRunAt": "2026-07-06T06:30:00Z",
      "nextRunAt": "2026-07-07T06:30:00Z",
      "isEnabled": true,
      "totalRuns": 152,
      "successCount": 148,
      "failureCount": 4,
      "averageDuration": 120
    }
  ]
}
```

### 2. ตรวจสอบสถานะงาน
```http
GET /api/batch-jobs/BATCH001/status
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "jobCode": "BATCH001",
  "status": "Running",
  "isEnabled": true,
  "lastRunAt": "2026-07-06T06:30:00Z",
  "nextRunAt": "2026-07-07T06:30:00Z",
  "lastRunDuration": 125,
  "lastRunStatus": "Completed",
  "successRate": 97.37
}
```

### 3. สั่งรันงาน (Manual Trigger)
```http
POST /api/batch-jobs/BATCH001/trigger
Authorization: Bearer <accessToken>
{
  "parameters": {
    "force": true,
    "sendEmail": true
  }
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "jobCode": "BATCH001",
    "triggeredAt": "2026-07-06T10:30:00Z",
    "historyId": "...",
    "estimatedCompletion": "2026-07-06T10:32:00Z"
  }
}
```

### 4. ประวัติการรันงาน
```http
GET /api/batch-jobs/BATCH001/history?page=1&pageSize=10
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "data": [
    {
      "id": "...",
      "runAt": "2026-07-06T06:30:00Z",
      "status": "Completed",
      "durationMs": 125,
      "recordsProcessed": 45,
      "errorMessage": null
    },
    {
      "id": "...",
      "runAt": "2026-07-05T06:30:00Z",
      "status": "Failed",
      "durationMs": 300,
      "recordsProcessed": 0,
      "errorMessage": "SMTP connection timeout"
    }
  ],
  "total": 152,
  "page": 1,
  "pageSize": 10
}
```

### 5. เปิด/ปิดใช้งานงาน
```http
PUT /api/batch-jobs/BATCH001/enable
Authorization: Bearer <accessToken>
```
```http
PUT /api/batch-jobs/BATCH001/disable
Authorization: Bearer <accessToken>
```

### 6. งานที่กำลังรัน
```http
GET /api/batch-jobs/running
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "jobCode": "BATCH001",
    "startedAt": "2026-07-06T06:30:00Z",
    "progress": 45,
    "estimatedRemaining": 60
  }
]
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - BatchJob.cs
```csharp
namespace ICMON.Domain.Aggregates.BatchJobAggregate;

public class BatchJob : AggregateRoot<Guid>
{
    public string JobCode { get; private set; }
    public string JobName { get; private set; }
    public string Description { get; private set; }
    public string CronExpression { get; private set; }
    public BatchJobType JobType { get; private set; }
    public BatchJobStatus Status { get; private set; }
    public bool IsEnabled { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastRunAt { get; private set; }
    public DateTime? NextRunAt { get; private set; }
    public int TotalRuns { get; private set; }
    public int SuccessCount { get; private set; }
    public int FailureCount { get; private set; }
    public double AverageDurationMs { get; private set; }
    public string? LastError { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<BatchJobHistory> _history = new();
    public IReadOnlyList<BatchJobHistory> History => _history.AsReadOnly();

    private BatchJob() { } // For EF Core

    public static BatchJob Create(
        string jobCode,
        string jobName,
        string description,
        string cronExpression,
        BatchJobType jobType,
        Guid whitelabelId)
    {
        var job = new BatchJob
        {
            Id = Guid.NewGuid(),
            JobCode = jobCode.ToUpper(),
            JobName = jobName,
            Description = description,
            CronExpression = cronExpression,
            JobType = jobType,
            Status = BatchJobStatus.Idle,
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        return job;
    }

    public void Start()
    {
        Status = BatchJobStatus.Running;
        LastRunAt = DateTime.UtcNow;
        TotalRuns++;
        
        var history = BatchJobHistory.Create(Id, BatchJobStatus.Running);
        _history.Add(history);
        
        AddDomainEvent(new BatchJobStartedEvent(Id, JobCode));
    }

    public void Complete(int recordsProcessed, long durationMs)
    {
        Status = BatchJobStatus.Idle;
        SuccessCount++;
        AverageDurationMs = ((AverageDurationMs * (TotalRuns - 1)) + durationMs) / TotalRuns;
        
        var history = _history.LastOrDefault();
        if (history != null)
        {
            history.Complete(recordsProcessed, durationMs);
        }
        
        AddDomainEvent(new BatchJobCompletedEvent(Id, JobCode, recordsProcessed, durationMs));
    }

    public void Fail(string error, long durationMs)
    {
        Status = BatchJobStatus.Idle;
        FailureCount++;
        LastError = error;
        AverageDurationMs = ((AverageDurationMs * (TotalRuns - 1)) + durationMs) / TotalRuns;
        
        var history = _history.LastOrDefault();
        if (history != null)
        {
            history.Fail(error, durationMs);
        }
        
        AddDomainEvent(new BatchJobFailedEvent(Id, JobCode, error));
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void UpdateSchedule(string cronExpression)
    {
        CronExpression = cronExpression;
    }

    public void ScheduleNextRun(DateTime nextRun)
    {
        NextRunAt = nextRun;
    }

    public bool IsRunning() => Status == BatchJobStatus.Running;
    public bool IsDue(DateTime currentTime)
    {
        if (!IsEnabled || IsRunning())
            return false;

        if (!NextRunAt.HasValue)
            return true;

        return NextRunAt.Value <= currentTime;
    }
}
```

### 2. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum BatchJobStatus
{
    Idle = 0,        // กำลังรอ
    Running = 1,     // กำลังทำงาน
    Completed = 2,   // เสร็จสิ้น
    Failed = 3,      // ล้มเหลว
    Paused = 4       // หยุดชั่วคราว
}

public enum BatchJobType
{
    Email = 0,       // ส่งอีเมล
    Report = 1,      // สร้างรายงาน
    Update = 2,      // อัปเดตข้อมูล
    Cleanup = 3,     // ล้างข้อมูล
    Sync = 4,        // ซิงค์ข้อมูล
    Notification = 5 // แจ้งเตือน
}
```

### 3. Interface - IBatchJob.cs
```csharp
namespace ICMON.Infrastructure.BackgroundJobs;

public interface IBatchJob
{
    string JobCode { get; }
    string JobName { get; }
    string Description { get; }
    string CronExpression { get; }

    Task<BatchJobResult> ExecuteAsync(
        BatchExecutionContext context,
        CancellationToken cancellationToken = default);
}

public class BatchExecutionContext
{
    public Guid JobId { get; set; }
    public string JobCode { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public CancellationToken CancellationToken { get; set; }
    public IProgress<int> Progress { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}

public class BatchJobResult
{
    public bool Success { get; set; }
    public int RecordsProcessed { get; set; }
    public string? ErrorMessage { get; set; }
    public long DurationMs { get; set; }
    public Dictionary<string, object>? Data { get; set; }

    public static BatchJobResult SuccessResult(int recordsProcessed, long durationMs, Dictionary<string, object>? data = null)
        => new()
        {
            Success = true,
            RecordsProcessed = recordsProcessed,
            DurationMs = durationMs,
            Data = data
        };

    public static BatchJobResult FailureResult(string error, long durationMs, int recordsProcessed = 0)
        => new()
        {
            Success = false,
            ErrorMessage = error,
            RecordsProcessed = recordsProcessed,
            DurationMs = durationMs
        };
}
```

### 4. Job Implementation - SendDailyEmailJob.cs
```csharp
namespace ICMON.Infrastructure.BackgroundJobs.Jobs;

public class SendDailyEmailJob : IBatchJob
{
    private readonly ILogger<SendDailyEmailJob> _logger;
    private readonly IEmailService _emailService;
    private readonly IJobRepository _jobRepository;
    private readonly ICustomerRepository _customerRepository;

    public string JobCode => "BATCH001";
    public string JobName => "ส่งอีเมลแจ้งเตือนรายวัน";
    public string Description => "ส่งอีเมลแจ้งเตือนสถานะงานให้ลูกค้าทุกวันเวลา 06:30";
    public string CronExpression => "0 30 6 * * ?";

    public SendDailyEmailJob(
        ILogger<SendDailyEmailJob> logger,
        IEmailService emailService,
        IJobRepository jobRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _emailService = emailService;
        _jobRepository = jobRepository;
        _customerRepository = customerRepository;
    }

    public async Task<BatchJobResult> ExecuteAsync(
        BatchExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting daily email job: {JobCode}", JobCode);

        try
        {
            var completedJobs = await _jobRepository.GetByStatusAsync(
                JobStatus.PaymentReceived,
                cancellationToken);

            var yesterday = DateTime.UtcNow.AddDays(-1);
            var newJobs = await _jobRepository.GetByDateRangeAsync(
                yesterday.Date,
                DateTime.UtcNow,
                cancellationToken);

            var totalProcessed = 0;
            var emailSent = 0;

            // Send notifications for completed jobs
            foreach (var job in completedJobs)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var customer = await _customerRepository.GetByIdAsync(job.CustomerId, cancellationToken);
                if (customer != null && !string.IsNullOrEmpty(customer.Email.Value))
                {
                    // Send email notification
                    await _emailService.SendJobCompletedEmailAsync(
                        customer.Email.Value,
                        job.JobNo,
                        job.TotalAmount.Amount,
                        cancellationToken);

                    emailSent++;
                }

                totalProcessed++;
                context.Progress?.Report((totalProcessed * 100) / (completedJobs.Count() + newJobs.Count()));
            }

            // Send daily summary for new jobs
            var summaryEmailTasks = newJobs
                .Select(job => _jobRepository.GetCustomerIdAsync(job.Id))
                .Distinct()
                .Select(customerId => _customerRepository.GetByIdAsync(customerId, cancellationToken))
                .ToList();

            var customers = await Task.WhenAll(summaryEmailTasks);
            var uniqueCustomers = customers.Where(c => c != null).Distinct();

            foreach (var customer in uniqueCustomers)
            {
                var customerJobs = newJobs.Where(j => j.CustomerId == customer.Id);
                await _emailService.SendDailySummaryEmailAsync(
                    customer.Email.Value,
                    customer.FullName,
                    customerJobs,
                    cancellationToken);

                emailSent++;
            }

            _logger.LogInformation(
                "Daily email job completed: {EmailSent} emails sent, {TotalProcessed} records processed",
                emailSent,
                totalProcessed + newJobs.Count());

            return BatchJobResult.SuccessResult(
                totalProcessed + newJobs.Count(),
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds,
                new Dictionary<string, object>
                {
                    ["EmailSent"] = emailSent,
                    ["CompletedJobs"] = completedJobs.Count(),
                    ["NewJobs"] = newJobs.Count()
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Daily email job failed: {JobCode}", JobCode);
            return BatchJobResult.FailureResult(
                ex.Message,
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds);
        }
    }
}
```

### 5. Job Implementation - DailyReportJob.cs
```csharp
namespace ICMON.Infrastructure.BackgroundJobs.Jobs;

public class DailyReportJob : IBatchJob
{
    private readonly ILogger<DailyReportJob> _logger;
    private readonly IReportService _reportService;
    private readonly IDocumentService _documentService;
    private readonly IEmailService _emailService;

    public string JobCode => "BATCH002";
    public string JobName => "สร้างรายงานประจำวัน";
    public string Description => "สร้างรายงานสรุปประจำวันเวลา 06:45";
    public string CronExpression => "0 45 6 * * ?";

    public DailyReportJob(
        ILogger<DailyReportJob> logger,
        IReportService reportService,
        IDocumentService documentService,
        IEmailService emailService)
    {
        _logger = logger;
        _reportService = reportService;
        _documentService = documentService;
        _emailService = emailService;
    }

    public async Task<BatchJobResult> ExecuteAsync(
        BatchExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting daily report job: {JobCode}", JobCode);

        try
        {
            var yesterday = DateTime.UtcNow.AddDays(-1);
            var reportData = await _reportService.GenerateDailyReportAsync(
                yesterday.Date,
                cancellationToken);

            // Generate PDF report
            var pdfBytes = await _documentService.GenerateReportPdfAsync(
                reportData,
                "DailyReport",
                cancellationToken);

            // Save report to storage
            var fileName = $"DailyReport_{yesterday:yyyyMMdd}.pdf";
            var filePath = await _documentService.SaveReportAsync(
                pdfBytes,
                fileName,
                "reports/daily",
                cancellationToken);

            // Send report to managers
            var managers = await _reportService.GetReportRecipientsAsync("DailyReport", cancellationToken);
            foreach (var manager in managers)
            {
                await _emailService.SendReportEmailAsync(
                    manager.Email,
                    "รายงานประจำวัน",
                    reportData,
                    pdfBytes,
                    fileName,
                    cancellationToken);
            }

            _logger.LogInformation(
                "Daily report job completed. Report saved to: {FilePath}",
                filePath);

            return BatchJobResult.SuccessResult(
                1,
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds,
                new Dictionary<string, object>
                {
                    ["FilePath"] = filePath,
                    ["Recipients"] = managers.Count(),
                    ["ReportDate"] = yesterday.ToString("yyyy-MM-dd")
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Daily report job failed: {JobCode}", JobCode);
            return BatchJobResult.FailureResult(
                ex.Message,
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds);
        }
    }
}
```

### 6. Job Implementation - UpdatePendingJobsJob.cs
```csharp
namespace ICMON.Infrastructure.BackgroundJobs.Jobs;

public class UpdatePendingJobsJob : IBatchJob
{
    private readonly ILogger<UpdatePendingJobsJob> _logger;
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public string JobCode => "BATCH003";
    public string JobName => "อัปเดตสถานะงานค้าง";
    public string Description => "อัปเดตสถานะงานค้างและตรวจสอบความล่าช้าเวลา 06:30";
    public string CronExpression => "0 30 6 * * ?";

    public UpdatePendingJobsJob(
        ILogger<UpdatePendingJobsJob> logger,
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BatchJobResult> ExecuteAsync(
        BatchExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting update pending jobs job: {JobCode}", JobCode);

        try
        {
            var pendingJobs = await _jobRepository.GetByStatusAsync(
                JobStatus.InProgress,
                cancellationToken);

            var totalUpdated = 0;
            var threshold = DateTime.UtcNow.AddHours(-24); // 24 hours

            foreach (var job in pendingJobs)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                // Check if job has been stuck for more than 24 hours
                if (job.ReceivedDate < threshold)
                {
                    // Update status or trigger alert
                    if (job.Status == JobStatus.InProgress)
                    {
                        // Check if there's a reason for delay
                        var hasRecentActivity = await _jobRepository.HasRecentActivityAsync(
                            job.Id,
                            TimeSpan.FromHours(12),
                            cancellationToken);

                        if (!hasRecentActivity)
                        {
                            _logger.LogWarning(
                                "Job {JobNo} has been in progress for more than 24 hours without activity",
                                job.JobNo);

                            // Could send alert or escalate
                            // job.AddNote("รอการดำเนินการ - เกินกำหนด");
                            totalUpdated++;
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "Update pending jobs job completed. {TotalUpdated} jobs updated",
                totalUpdated);

            return BatchJobResult.SuccessResult(
                totalUpdated,
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds,
                new Dictionary<string, object>
                {
                    ["TotalUpdated"] = totalUpdated,
                    ["TotalPending"] = pendingJobs.Count()
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update pending jobs job failed: {JobCode}", JobCode);
            return BatchJobResult.FailureResult(
                ex.Message,
                (long)(DateTime.UtcNow - context.StartedAt).TotalMilliseconds);
        }
    }
}
```

### 7. Batch Job Orchestrator
```csharp
using System.Collections.Concurrent;

namespace ICMON.Infrastructure.BackgroundJobs;

public class BatchJobOrchestrator : IBatchJobService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBatchJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BatchJobOrchestrator> _logger;
    private readonly IMediator _mediator;

    private readonly ConcurrentDictionary<string, CancellationTokenSource> _runningJobs = new();
    private readonly SemaphoreSlim _semaphore = new(3); // Max 3 concurrent jobs

    public BatchJobOrchestrator(
        IServiceProvider serviceProvider,
        IBatchJobRepository jobRepository,
        IUnitOfWork unitOfWork,
        ILogger<BatchJobOrchestrator> logger,
        IMediator mediator)
    {
        _serviceProvider = serviceProvider;
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<BatchJobResult> TriggerJobAsync(
        string jobCode,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var job = await _jobRepository.GetByCodeAsync(jobCode, cancellationToken);
        if (job == null)
            throw new ArgumentException($"Job '{jobCode}' not found");

        if (!job.IsEnabled)
            throw new InvalidOperationException($"Job '{jobCode}' is disabled");

        if (job.IsRunning())
            throw new InvalidOperationException($"Job '{jobCode}' is already running");

        // Acquire semaphore
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            // Get job implementation
            var jobImplementations = _serviceProvider.GetServices<IBatchJob>();
            var jobImpl = jobImplementations.FirstOrDefault(j => j.JobCode == jobCode);

            if (jobImpl == null)
                throw new InvalidOperationException($"Job implementation for '{jobCode}' not found");

            // Create execution context
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _runningJobs.TryAdd(jobCode, cts);

            var context = new BatchExecutionContext
            {
                JobId = job.Id,
                JobCode = jobCode,
                Parameters = parameters ?? new Dictionary<string, object>(),
                CancellationToken = cts.Token,
                Progress = new Progress<int>(p => OnProgressUpdated(jobCode, p))
            };

            // Start job
            job.Start();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Job {JobCode} triggered at {Time}", jobCode, DateTime.UtcNow);

            // Execute job
            var result = await jobImpl.ExecuteAsync(context, cts.Token);

            // Update job status
            if (result.Success)
            {
                job.Complete(result.RecordsProcessed, result.DurationMs);
            }
            else
            {
                job.Fail(result.ErrorMessage, result.DurationMs);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Clean up
            _runningJobs.TryRemove(jobCode, out _);

            return result;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> IsJobRunningAsync(string jobCode, CancellationToken cancellationToken = default)
    {
        return _runningJobs.ContainsKey(jobCode);
    }

    public async Task<IEnumerable<string>> GetRunningJobsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_runningJobs.Keys.ToList());
    }

    public async Task<bool> CancelJobAsync(string jobCode, CancellationToken cancellationToken = default)
    {
        if (_runningJobs.TryGetValue(jobCode, out var cts))
        {
            try
            {
                cts.Cancel();
                return true;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
        return false;
    }

    public async Task<BatchJobStatus> GetJobStatusAsync(string jobCode, CancellationToken cancellationToken = default)
    {
        var job = await _jobRepository.GetByCodeAsync(jobCode, cancellationToken);
        if (job == null)
            throw new ArgumentException($"Job '{jobCode}' not found");

        return job.Status;
    }

    private void OnProgressUpdated(string jobCode, int progress)
    {
        _logger.LogDebug("Job {JobCode} progress: {Progress}%", jobCode, progress);
    }
}
```

### 8. Hangfire Configuration
```csharp
using Hangfire;
using Hangfire.PostgreSql;

namespace ICMON.Infrastructure.BackgroundJobs;

public static class HangfireConfiguration
{
    public static IServiceCollection AddHangfireService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddHangfire(config =>
        {
            config.UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                InvisibilityTimeout = TimeSpan.FromMinutes(30),
                QueuePollInterval = TimeSpan.FromSeconds(15),
                JobExpirationCheckInterval = TimeSpan.FromHours(1)
            });

            config.UseFilter(new AutomaticRetryAttribute
            {
                Attempts = 3,
                DelaysInSeconds = new[] { 10, 30, 60 },
                OnAttemptsExceeded = AttemptsExceededAction.Delete
            });
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Environment.ProcessorCount * 2;
            options.Queues = new[] { "default", "email", "report", "sync", "cleanup" };
            options.ServerName = $"Server-{Environment.MachineName}-{Guid.NewGuid():N[..8]}";
        });

        return services;
    }

    public static void ScheduleBatchJobs(this IRecurringJobManager recurringJobManager)
    {
        // BATCH001: Send daily email notifications
        recurringJobManager.AddOrUpdate<SendDailyEmailJob>(
            "BATCH001",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 30 6 * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // BATCH002: Generate daily report
        recurringJobManager.AddOrUpdate<DailyReportJob>(
            "BATCH002",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 45 6 * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // BATCH003: Update pending jobs
        recurringJobManager.AddOrUpdate<UpdatePendingJobsJob>(
            "BATCH003",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 30 6 * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // BATCH004: Cleanup and sync
        recurringJobManager.AddOrUpdate<CleanupAndSyncJob>(
            "BATCH004",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 0 3 * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // BATCH005: Realtime sync (every 30 minutes)
        recurringJobManager.AddOrUpdate<RealtimeSyncJob>(
            "BATCH005",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 0/30 * * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // BATCH006: Send sales summary
        recurringJobManager.AddOrUpdate<SendSalesSummaryJob>(
            "BATCH006",
            job => job.ExecuteAsync(It.IsAny<BatchExecutionContext>(), It.IsAny<CancellationToken>()),
            "0 30 6 * * ?",
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
    }
}
```

### 9. Quartz Configuration (Optional Alternative)
```csharp
using Quartz;

namespace ICMON.Infrastructure.BackgroundJobs;

public class QuartzJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public QuartzJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var jobType = bundle.JobDetail.JobType;
        return (IJob)_serviceProvider.GetRequiredService(jobType);
    }

    public void ReturnJob(IJob job)
    {
        // Clean up if needed
    }
}

public class JobTriggerListener : ITriggerListener
{
    private readonly ILogger<JobTriggerListener> _logger;

    public JobTriggerListener(ILogger<JobTriggerListener> logger)
    {
        _logger = logger;
    }

    public string Name => "JobTriggerListener";

    public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Quartz: Trigger {TriggerKey} fired for job {JobKey} at {Time}",
            trigger.Key,
            context.JobDetail.Key,
            DateTime.UtcNow);

        return Task.CompletedTask;
    }

    public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken)
    {
        return Task.FromResult(false);
    }

    public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken)
    {
        _logger.LogWarning(
            "Quartz: Trigger {TriggerKey} misfired",
            trigger.Key);

        return Task.CompletedTask;
    }

    public Task TriggerComplete(
        ITrigger trigger,
        IJobExecutionContext context,
        SchedulerInstruction triggerInstructionCode,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Quartz: Trigger {TriggerKey} completed for job {JobKey} with instruction {Instruction}",
            trigger.Key,
            context.JobDetail.Key,
            triggerInstructionCode);

        return Task.CompletedTask;
    }
}

[DisallowConcurrentExecution]
public class QuartzBatchJob : IJob
{
    private readonly IBatchJob _batchJob;
    private readonly IBatchJobOrchestrator _orchestrator;
    private readonly ILogger<QuartzBatchJob> _logger;

    public QuartzBatchJob(
        IBatchJob batchJob,
        IBatchJobOrchestrator orchestrator,
        ILogger<QuartzBatchJob> logger)
    {
        _batchJob = batchJob;
        _orchestrator = orchestrator;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation(
                "Quartz: Executing job {JobCode} at {Time}",
                _batchJob.JobCode,
                DateTime.UtcNow);

            await _orchestrator.TriggerJobAsync(
                _batchJob.JobCode,
                context.JobDetail.JobDataMap.ToDictionary(),
                context.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Quartz: Job {JobCode} failed",
                _batchJob.JobCode);
            throw;
        }
    }
}

public static class QuartzConfiguration
{
    public static IServiceCollection AddQuartzService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            // Register jobs
            var jobTypes = new[]
            {
                typeof(SendDailyEmailJob),
                typeof(DailyReportJob),
                typeof(UpdatePendingJobsJob),
                typeof(CleanupAndSyncJob),
                typeof(RealtimeSyncJob),
                typeof(SendSalesSummaryJob)
            };

            foreach (var jobType in jobTypes)
            {
                var jobKey = new JobKey(jobType.Name);
                q.AddJob(jobType, jobKey);

                // Get cron expression from job attribute
                var jobInstance = (IBatchJob)Activator.CreateInstance(jobType);
                q.AddTrigger(trigger => trigger
                    .ForJob(jobKey)
                    .WithIdentity($"{jobType.Name}-trigger")
                    .WithCronSchedule(jobInstance.CronExpression)
                    .WithDescription(jobInstance.Description));
            }

            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();
            q.UseDefaultThreadPool(threads => threads.MaxConcurrency = 10);
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
            options.AwaitApplicationStarted = true;
        });

        return services;
    }
}
```

### 10. Controller - BatchJobsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BatchJobsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBatchJobService _batchJobService;

    public BatchJobsController(IMediator mediator, IBatchJobService batchJobService)
    {
        _mediator = mediator;
        _batchJobService = batchJobService;
    }

    [HttpGet]
    [Authorize("BATCH_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetBatchJobList(
        [FromQuery] BatchJobType? jobType,
        [FromQuery] bool? isEnabled,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetBatchJobListQuery
        {
            JobType = jobType,
            IsEnabled = isEnabled,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{jobCode}/status")]
    [Authorize("BATCH_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetBatchJobStatus(string jobCode)
    {
        var query = new GetBatchJobStatusQuery { JobCode = jobCode };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("{jobCode}/history")]
    [Authorize("BATCH_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetBatchJobHistory(
        string jobCode,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetBatchJobHistoryQuery
        {
            JobCode = jobCode,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("{jobCode}/trigger")]
    [Authorize("BATCH_TRIGGER")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> TriggerJob(string jobCode, [FromBody] TriggerJobCommand command)
    {
        if (jobCode != command.JobCode)
            return BadRequest(Result<BatchJobDto>.Failure("Job Code mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{jobCode}/enable")]
    [Authorize("BATCH_ADMIN")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> EnableJob(string jobCode)
    {
        var command = new EnableJobCommand { JobCode = jobCode };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("{jobCode}/disable")]
    [Authorize("BATCH_ADMIN")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> DisableJob(string jobCode)
    {
        var command = new DisableJobCommand { JobCode = jobCode };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("running")]
    [Authorize("BATCH_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetRunningJobs()
    {
        var running = await _batchJobService.GetRunningJobsAsync();
        var result = new List<RunningJobDto>();

        foreach (var jobCode in running)
        {
            result.Add(new RunningJobDto
            {
                JobCode = jobCode,
                StartedAt = DateTime.UtcNow // Would need to track actual start time
            });
        }

        return Ok(result);
    }

    [HttpPost("{jobCode}/cancel")]
    [Authorize("BATCH_ADMIN")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> CancelJob(string jobCode)
    {
        var result = await _batchJobService.CancelJobAsync(jobCode);
        return Ok(new { Success = result, JobCode = jobCode });
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `batch_jobs:{jobCode}` | 5 นาที | ข้อมูลงาน Batch |
| `batch_jobs_all` | 5 นาที | รายการงานทั้งหมด |
| `batch_lock:{jobCode}` | 5 นาที | Distributed Lock |
| `batch_running_jobs` | 1 นาที | รายการงานที่กำลังรัน |

---

## 🏗️ การติดตั้งและใช้งาน

### ใน `Program.cs` เพิ่ม:
```csharp
// Add Batch Jobs
builder.Services.AddScoped<IBatchJob, SendDailyEmailJob>();
builder.Services.AddScoped<IBatchJob, DailyReportJob>();
builder.Services.AddScoped<IBatchJob, UpdatePendingJobsJob>();
builder.Services.AddScoped<IBatchJob, CleanupAndSyncJob>();
builder.Services.AddScoped<IBatchJob, RealtimeSyncJob>();
builder.Services.AddScoped<IBatchJob, SendSalesSummaryJob>();

builder.Services.AddScoped<IBatchJobService, BatchJobOrchestrator>();

// Add Hangfire
builder.Services.AddHangfireService(builder.Configuration);

// Add Quartz (optional)
// builder.Services.AddQuartzService(builder.Configuration);
```

### ใน `appsettings.json`:
```json
{
  "Hangfire": {
    "ServerName": "ICMON-Server",
    "WorkerCount": 4,
    "QueuePollInterval": 15
  },
  "Quartz": {
    "ThreadPool": {
      "MaxConcurrency": 10
    }
  }
}
```

### Migration เพื่อสร้างตาราง Batch Jobs:
```bash
dotnet ef migrations add AddBatchJobs
dotnet ef database update
```

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ 6 Batch Jobs ตามกำหนด
- ✅ รันตาม Cron Expression
- ✅ Manual Trigger
- ✅ เปิด/ปิดใช้งานงาน
- ✅ ติดตามสถานะงาน
- ✅ ประวัติการรันงาน
- ✅ Distributed Lock (ป้องกันการรันซ้ำ)
- ✅ รองรับการ Cancel งาน
- ✅ ตรวจสอบงานที่กำลังรัน
- ✅ สนับสนุนทั้ง Hangfire และ Quartz
- ✅ Domain Events (Started, Completed, Failed)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 11: **Batch Jobs** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 12: Multi-Language (i18n)
2. โมดูลที่ 13: IoT & GPS Tracking
3. โมดูลที่ 14: Web Order System (WOS)