เนื่องจากไม่สามารถเข้าถึง repository `csharpdddcleanarchitecture` ของคุณโดยตรงได้ ฉันจะใช้โครงสร้างและแนวทางปฏิบัติที่ดีที่สุดของ **Clean Architecture + Domain-Driven Design (DDD) + CQRS** ในภาษา C# ที่สอดคล้องกับมาตรฐานที่คุณต้องการ มาเป็นพื้นฐานในการออกแบบโปรเจกต์ `ICMON`

---

## 🚗 ระบบบริหารจัดการอู่ซ่อมรถ – ICMON

**Auto Repair Shop Management System – ICMON (C# .NET)**

---

| รายการ | รายละเอียด |
|--------|-----------|
| **ชื่อโครงการ** | ICMON (Integrated Car Maintenance Operation Network) |
| **ภาษาที่ใช้** | C# 12 (.NET 8 / .NET 9) |
| **สถาปัตยกรรม** | Clean Architecture + DDD + CQRS + Event‑Driven |
| **รูปแบบ** | Monolith Modular (เตรียมพร้อมสำหรับ Microservices) |
| **วันที่** | 2026-07-06 |
| **สถานะ** | ฉบับออกแบบ (Design Specification) |

---

## 1. โครงสร้างโปรเจกต์ (Solution Structure)

```
ICMON.sln
│
├── 📁 src/
│   │
│   ├── 📁 ICMON.Domain/                              (Layer 1: Domain)
│   │   ├── 📁 Aggregates/
│   │   │   ├── JobAggregate/
│   │   │   │   ├── Job.cs
│   │   │   │   ├── JobService.cs
│   │   │   │   ├── JobPartSales.cs
│   │   │   │   └── JobStatusHistory.cs
│   │   │   ├── QuotationAggregate/
│   │   │   │   ├── Quotation.cs
│   │   │   │   ├── QuotationPart.cs
│   │   │   │   └── QuotationService.cs
│   │   │   ├── CustomerAggregate/
│   │   │   │   ├── Customer.cs
│   │   │   │   └── Car.cs
│   │   │   └── InventoryAggregate/
│   │   │       ├── PartMaster.cs
│   │   │       ├── InventoryTransaction.cs
│   │   │       └── StockLocation.cs
│   │   ├── 📁 Entities/
│   │   │   ├── User.cs
│   │   │   ├── Role.cs
│   │   │   ├── Permission.cs
│   │   │   ├── Supplier.cs
│   │   │   ├── Service.cs
│   │   │   └── ...
│   │   ├── 📁 ValueObjects/
│   │   │   ├── Money.cs
│   │   │   ├── Address.cs
│   │   │   ├── PhoneNumber.cs
│   │   │   ├── Email.cs
│   │   │   └── ...
│   │   ├── 📁 Enums/
│   │   │   ├── JobStatus.cs
│   │   │   ├── QuotationStatus.cs
│   │   │   ├── PaymentStatus.cs
│   │   │   └── ...
│   │   ├── 📁 Events/
│   │   │   ├── JobCreatedEvent.cs
│   │   │   ├── JobStatusChangedEvent.cs
│   │   │   ├── QuotationApprovedEvent.cs
│   │   │   ├── PaymentReceivedEvent.cs
│   │   │   └── ...
│   │   ├── 📁 Interfaces/
│   │   │   ├── IRepository.cs
│   │   │   ├── IUnitOfWork.cs
│   │   │   ├── IEventPublisher.cs
│   │   │   └── ...
│   │   ├── 📁 Exceptions/
│   │   │   ├── DomainException.cs
│   │   │   └── ...
│   │   └── 📁 Specifications/
│   │       ├── BaseSpecification.cs
│   │       └── JobSpecifications.cs
│   │
│   ├── 📁 ICMON.Application/                          (Layer 2: Application)
│   │   ├── 📁 Commands/
│   │   │   ├── 📁 Jobs/
│   │   │   │   ├── CreateJob/
│   │   │   │   │   ├── CreateJobCommand.cs
│   │   │   │   │   ├── CreateJobCommandHandler.cs
│   │   │   │   │   └── CreateJobCommandValidator.cs
│   │   │   │   ├── UpdateJobStatus/
│   │   │   │   │   ├── UpdateJobStatusCommand.cs
│   │   │   │   │   ├── UpdateJobStatusCommandHandler.cs
│   │   │   │   │   └── UpdateJobStatusCommandValidator.cs
│   │   │   │   └── ...
│   │   │   ├── 📁 Quotations/
│   │   │   │   ├── CreateQuotation/
│   │   │   │   └── ApproveQuotation/
│   │   │   ├── 📁 Customers/
│   │   │   ├── 📁 Inventory/
│   │   │   └── 📁 Payments/
│   │   ├── 📁 Queries/
│   │   │   ├── 📁 Jobs/
│   │   │   │   ├── GetJobById/
│   │   │   │   │   ├── GetJobByIdQuery.cs
│   │   │   │   │   └── GetJobByIdQueryHandler.cs
│   │   │   │   ├── GetJobList/
│   │   │   │   │   ├── GetJobListQuery.cs
│   │   │   │   │   └── GetJobListQueryHandler.cs
│   │   │   │   └── ...
│   │   │   ├── 📁 Dashboards/
│   │   │   └── 📁 Reports/
│   │   ├── 📁 DTOs/
│   │   │   ├── JobDto.cs
│   │   │   ├── JobDetailDto.cs
│   │   │   ├── QuotationDto.cs
│   │   │   ├── CustomerDto.cs
│   │   │   └── ...
│   │   ├── 📁 Mappings/
│   │   │   └── AutoMapperProfile.cs
│   │   ├── 📁 Validators/
│   │   │   └── (FluentValidation validators)
│   │   ├── 📁 Services/
│   │   │   ├── ICurrentUserService.cs
│   │   │   ├── IDateTimeService.cs
│   │   │   └── ...
│   │   ├── 📁 Common/
│   │   │   ├── BaseCommand.cs
│   │   │   ├── BaseQuery.cs
│   │   │   └── Result.cs
│   │   └── 📁 Behaviors/
│   │       ├── ValidationBehavior.cs
│   │       ├── LoggingBehavior.cs
│   │       └── PerformanceBehavior.cs
│   │
│   ├── 📁 ICMON.Infrastructure/                       (Layer 3: Infrastructure)
│   │   ├── 📁 Persistence/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── 📁 Configurations/
│   │   │   │   ├── JobConfiguration.cs
│   │   │   │   ├── CustomerConfiguration.cs
│   │   │   │   └── ...
│   │   │   ├── 📁 Repositories/
│   │   │   │   ├── GenericRepository.cs
│   │   │   │   ├── JobRepository.cs
│   │   │   │   ├── CustomerRepository.cs
│   │   │   │   └── ...
│   │   │   ├── UnitOfWork.cs
│   │   │   ├── 📁 Migrations/
│   │   │   └── 📁 SeedData/
│   │   ├── 📁 Cache/
│   │   │   ├── ICacheService.cs
│   │   │   └── RedisCacheService.cs
│   │   ├── 📁 Messaging/
│   │   │   ├── KafkaEventPublisher.cs
│   │   │   ├── KafkaEventConsumer.cs
│   │   │   └── IEventPublisher.cs
│   │   ├── 📁 IoT/
│   │   │   ├── MqttClientService.cs
│   │   │   └── InfluxDbService.cs
│   │   ├── 📁 BackgroundJobs/
│   │   │   ├── HangfireJobs.cs
│   │   │   └── QuartzJobs.cs
│   │   ├── 📁 DocumentGeneration/
│   │   │   ├── IPdfGenerator.cs
│   │   │   ├── PdfGenerator.cs (QuestPDF)
│   │   │   ├── IExcelGenerator.cs
│   │   │   └── ExcelGenerator.cs (EPPlus)
│   │   ├── 📁 Email/
│   │   │   ├── IEmailService.cs
│   │   │   └── SmtpEmailService.cs
│   │   ├── 📁 Authentication/
│   │   │   ├── JwtTokenService.cs
│   │   │   └── PermissionService.cs
│   │   ├── 📁 Localization/
│   │   │   └── JsonStringLocalizer.cs
│   │   └── 📁 Extensions/
│   │       └── ServiceCollectionExtensions.cs
│   │
│   ├── 📁 ICMON.Api/                                  (Layer 4: Presentation)
│   │   ├── 📁 Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── JobsController.cs
│   │   │   ├── QuotationsController.cs
│   │   │   ├── CustomersController.cs
│   │   │   ├── InventoryController.cs
│   │   │   ├── PaymentsController.cs
│   │   │   ├── DashboardController.cs
│   │   │   ├── ReportsController.cs
│   │   │   ├── IoTController.cs
│   │   │   └── WOSController.cs
│   │   ├── 📁 Middleware/
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   ├── RateLimitingMiddleware.cs
│   │   │   └── RequestLoggingMiddleware.cs
│   │   ├── 📁 Filters/
│   │   │   └── ApiKeyFilter.cs
│   │   ├── 📁 Attributes/
│   │   │   └── PermissionAttribute.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Properties/
│   │       └── launchSettings.json
│   │
│   └── 📁 ICMON.Shared/                               (Shared Kernel)
│       ├── 📁 Constants/
│       ├── 📁 Helpers/
│       ├── 📁 Extensions/
│       └── 📁 Resources/                              (สำหรับ i18n)
│           ├── Messages.th.json
│           ├── Messages.en.json
│           ├── Messages.zh.json
│           └── ...
│
├── 📁 tests/
│   ├── 📁 ICMON.UnitTests/
│   │   ├── Domain/
│   │   ├── Application/
│   │   └── Common/
│   ├── 📁 ICMON.IntegrationTests/
│   │   ├── Controllers/
│   │   ├── Repositories/
│   │   └── Services/
│   └── 📁 ICMON.ArchitectureTests/
│       ├── LayerTests.cs
│       └── DependencyTests.cs
│
├── 📁 docs/
│   ├── API.md
│   ├── Architecture.md
│   └── Deployment.md
│
├── docker-compose.yml
├── Dockerfile
├── .env.example
└── README.md
```

---

## 2. รายละเอียดแต่ละ Layer

### 2.1 Domain Layer (`ICMON.Domain`)

เป็นหัวใจของระบบ **ไม่มี Dependency ใดๆ** ต่อ Layer อื่น

```csharp
// Domain/Aggregates/JobAggregate/Job.cs
using ICMON.Domain.Enums;
using ICMON.Domain.Events;
using ICMON.Domain.ValueObjects;

namespace ICMON.Domain.Aggregates.JobAggregate;

public class Job : AggregateRoot<Guid>
{
    public string JobNo { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid CarId { get; private set; }
    public Guid? MechanicId { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime ReceivedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public string Description { get; private set; }
    public Money TotalAmount { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<JobService> _services = new();
    public IReadOnlyList<JobService> Services => _services.AsReadOnly();

    private readonly List<JobPartSales> _parts = new();
    public IReadOnlyList<JobPartSales> Parts => _parts.AsReadOnly();

    private readonly List<JobStatusHistory> _statusHistory = new();
    public IReadOnlyList<JobStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Job() { } // For EF Core

    public static Job Create(
        Guid customerId,
        Guid carId,
        string description,
        Guid whitelabelId,
        Guid? mechanicId = null)
    {
        var job = new Job
        {
            Id = Guid.NewGuid(),
            JobNo = GenerateJobNo(),
            CustomerId = customerId,
            CarId = carId,
            MechanicId = mechanicId,
            Status = JobStatus.Open,
            ReceivedDate = DateTime.UtcNow,
            Description = description,
            WhitelabelId = whitelabelId,
            TotalAmount = Money.Zero
        };

        job.AddDomainEvent(new JobCreatedEvent(job.Id, job.JobNo));
        return job;
    }

    public void ChangeStatus(JobStatus newStatus, string? note = null)
    {
        if (!CanTransitionTo(newStatus))
            throw new DomainException($"Cannot transition from {Status} to {newStatus}");

        Status = newStatus;
        _statusHistory.Add(JobStatusHistory.Create(Id, newStatus, note));

        if (newStatus == JobStatus.Closed)
            CompletedDate = DateTime.UtcNow;

        AddDomainEvent(new JobStatusChangedEvent(Id, newStatus));
    }

    public void AddService(Guid serviceId, int quantity, Money unitPrice)
    {
        var jobService = JobService.Create(Id, serviceId, quantity, unitPrice);
        _services.Add(jobService);
        RecalculateTotal();
    }

    public void AddPart(Guid partId, int quantity, Money unitPrice)
    {
        var jobPart = JobPartSales.Create(Id, partId, quantity, unitPrice);
        _parts.Add(jobPart);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        var serviceTotal = _services.Sum(s => s.TotalAmount);
        var partTotal = _parts.Sum(p => p.TotalAmount);
        TotalAmount = serviceTotal + partTotal;
    }

    private static string GenerateJobNo() 
        => $"JOB-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6]}";

    private bool CanTransitionTo(JobStatus newStatus)
    {
        return newStatus switch
        {
            JobStatus.InProgress => Status is JobStatus.Open or JobStatus.QuotationPending,
            JobStatus.QuotationPending => Status == JobStatus.InProgress,
            JobStatus.QuotationApproved => Status == JobStatus.QuotationPending,
            JobStatus.PartPicking => Status == JobStatus.QuotationApproved,
            JobStatus.RepairInProgress => Status == JobStatus.PartPicking,
            JobStatus.RepairDone => Status == JobStatus.RepairInProgress,
            JobStatus.InvoicePending => Status == JobStatus.RepairDone,
            JobStatus.InvoiceCreated => Status == JobStatus.InvoicePending,
            JobStatus.PaymentReceived => Status == JobStatus.InvoiceCreated,
            JobStatus.Closed => Status == JobStatus.PaymentReceived,
            _ => false
        };
    }
}
```

```csharp
// Domain/ValueObjects/Money.cs
namespace ICMON.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Zero => new(0, "THB");

    public Money(decimal amount, string currency = "THB")
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative");
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException("Currency mismatch");
        return new Money(Amount + other.Amount, Currency);
    }

    public static Money operator +(Money a, Money b) => a.Add(b);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
```

```csharp
// Domain/Events/JobCreatedEvent.cs
namespace ICMON.Domain.Events;

public record JobCreatedEvent(Guid JobId, string JobNo) : IDomainEvent;
```

---

### 2.2 Application Layer (`ICMON.Application`)

ใช้ **MediatR** สำหรับ CQRS + **FluentValidation** + **AutoMapper**

```xml
<!-- ICMON.Application.csproj -->
<PackageReference Include="MediatR" Version="12.2.0" />
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
```

```csharp
// Application/Commands/Jobs/CreateJob/CreateJobCommand.cs
using MediatR;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommand : IRequest<Result<JobDto>>
{
    public Guid CustomerId { get; set; }
    public Guid CarId { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid? MechanicId { get; set; }
    public List<ServiceItemDto> Services { get; set; } = new();
    public List<PartItemDto> Parts { get; set; } = new();
}

public class ServiceItemDto
{
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class PartItemDto
{
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
```

```csharp
// Application/Commands/Jobs/CreateJob/CreateJobCommandHandler.cs
using MediatR;
using AutoMapper;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    private readonly ICurrentUserService _currentUser;

    public CreateJobCommandHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEventPublisher eventPublisher,
        ICurrentUserService currentUser)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _currentUser = currentUser;
    }

    public async Task<Result<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = Job.Create(
            request.CustomerId,
            request.CarId,
            request.Description,
            _currentUser.WhitelabelId,
            request.MechanicId
        );

        foreach (var svc in request.Services)
        {
            job.AddService(svc.ServiceId, svc.Quantity, new Money(svc.UnitPrice));
        }

        foreach (var part in request.Parts)
        {
            job.AddPart(part.PartId, part.Quantity, new Money(part.UnitPrice));
        }

        await _jobRepository.AddAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish domain events
        foreach (var @event in job.DomainEvents)
        {
            await _eventPublisher.PublishAsync(@event, cancellationToken);
        }

        var dto = _mapper.Map<JobDto>(job);
        return Result<JobDto>.Success(dto);
    }
}
```

```csharp
// Application/Commands/Jobs/CreateJob/CreateJobCommandValidator.cs
using FluentValidation;

namespace ICMON.Application.Commands.Jobs.CreateJob;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(500);
        
        RuleForEach(x => x.Services).ChildRules(service =>
        {
            service.RuleFor(s => s.ServiceId).NotEmpty();
            service.RuleFor(s => s.Quantity).GreaterThan(0);
            service.RuleFor(s => s.UnitPrice).GreaterThanOrEqualTo(0);
        });

        RuleForEach(x => x.Parts).ChildRules(part =>
        {
            part.RuleFor(p => p.PartId).NotEmpty();
            part.RuleFor(p => p.Quantity).GreaterThan(0);
            part.RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}
```

```csharp
// Application/Common/Result.cs
namespace ICMON.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? Error { get; }
    public List<string> Errors { get; } = new();

    private Result(bool isSuccess, T? data, string? error = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new(true, data);
    public static Result<T> Failure(string error) => new(false, default, error);
    public static Result<T> Failure(List<string> errors) 
        => new(false, default, string.Join("; ", errors));
}
```

```csharp
// Application/Behaviors/ValidationBehavior.cs (Pipeline Behavior)
using FluentValidation;
using MediatR;

namespace ICMON.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}
```

---

### 2.3 Infrastructure Layer (`ICMON.Infrastructure`)

```xml
<!-- ICMON.Infrastructure.csproj -->
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
<PackageReference Include="StackExchange.Redis" Version="2.7.33" />
<PackageReference Include="Confluent.Kafka" Version="2.3.0" />
<PackageReference Include="Hangfire" Version="1.8.7" />
<PackageReference Include="Hangfire.PostgreSql" Version="1.20.5" />
<PackageReference Include="QuestPDF" Version="2023.12.6" />
<PackageReference Include="EPPlus" Version="7.0.0" />
```

```csharp
// Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Interfaces;

namespace ICMON.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<PartMaster> Parts { get; set; }
    // ... other DbSets

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Domain Events will be published via outbox pattern
        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

```csharp
// Infrastructure/Persistence/Repositories/JobRepository.cs
using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Aggregates.JobAggregate;
using ICMON.Domain.Interfaces;

namespace ICMON.Infrastructure.Persistence.Repositories;

public class JobRepository : GenericRepository<Job>, IJobRepository
{
    public JobRepository(AppDbContext context) : base(context) { }

    public async Task<Job?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(j => j.Services)
            .Include(j => j.Parts)
            .Include(j => j.StatusHistory)
            .Include(j => j.Customer)
            .Include(j => j.Car)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Job>> GetByStatusAsync(JobStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(j => j.Status == status)
            .ToListAsync(cancellationToken);
    }
}
```

```csharp
// Infrastructure/Cache/RedisCacheService.cs
using StackExchange.Redis;
using System.Text.Json;

namespace ICMON.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
        _jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value, _jsonOptions);
        await _database.StringSetAsync(key, json, expiry);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => await _database.KeyDeleteAsync(key);

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        => await _database.KeyExistsAsync(key);
}
```

```csharp
// Infrastructure/Messaging/KafkaEventPublisher.cs
using Confluent.Kafka;
using System.Text.Json;

namespace ICMON.Infrastructure.Messaging;

public class KafkaEventPublisher : IEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly JsonSerializerOptions _jsonOptions;

    public KafkaEventPublisher(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
        _jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent
    {
        var message = JsonSerializer.Serialize(@event, _jsonOptions);
        var topic = @event.GetType().Name;
        
        await _producer.ProduceAsync(
            topic,
            new Message<string, string> 
            { 
                Key = @event.AggregateId.ToString(), 
                Value = message 
            },
            cancellationToken
        );
    }
}
```

---

### 2.4 Presentation Layer (`ICMON.Api`)

```csharp
// Api/Controllers/JobsController.cs
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ICMON.Application.Commands.Jobs.CreateJob;
using ICMON.Application.Queries.Jobs.GetJobById;
using ICMON.Application.Common;

namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("create")]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJob(Guid id)
    {
        var query = new GetJobByIdQuery { JobId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}
```

```csharp
// Api/Program.cs
using ICMON.Application;
using ICMON.Infrastructure;
using ICMON.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "icmon-{0:yyyy.MM.dd}"
    })
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application Layer
builder.Services.AddApplicationServices();

// Add Infrastructure Layer
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

```csharp
// Api/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=icmon;Username=icmon;Password=icmon123",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Issuer": "ICMON",
    "Audience": "ICMON-API",
    "SecretKey": "your-super-secret-key-with-minimum-32-characters"
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092"
  },
  "Serilog": {
    "MinimumLevel": "Information"
  },
  "RateLimiting": {
    "PermitLimit": 100,
    "Window": "00:01:00"
  }
}
```

---

## 3. Dependency Injection Setup

### 3.1 Application Layer Extensions

```csharp
// Application/DependencyInjection.cs
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ICMON.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
```

### 3.2 Infrastructure Layer Extensions

```csharp
// Infrastructure/DependencyInjection.cs
using ICMON.Domain.Interfaces;
using ICMON.Infrastructure.Cache;
using ICMON.Infrastructure.Messaging;
using ICMON.Infrastructure.Persistence;
using ICMON.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ICMON.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, AppDbContext>();

        // Cache
        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
        services.AddScoped<ICacheService, RedisCacheService>();

        // Messaging
        services.AddScoped<IEventPublisher, KafkaEventPublisher>();

        // Background Jobs
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();

        return services;
    }
}
```

---

## 4. Multi-Language (i18n) – JSON Resources

ใช้ **JSON** แทน `.resx` เพื่อความยืดหยุ่น

```json
// Shared/Resources/Messages.th.json
{
  "WelcomeMessage": "ยินดีต้อนรับสู่ระบบ ICMON",
  "JobCreated": "สร้างใบงานเรียบร้อยแล้ว",
  "JobNotFound": "ไม่พบใบงานที่ระบุ",
  "Status": "สถานะ",
  "Customer": "ลูกค้า",
  "Car": "รถยนต์",
  "TotalAmount": "ยอดรวม"
}
```

```json
// Shared/Resources/Messages.en.json
{
  "WelcomeMessage": "Welcome to ICMON System",
  "JobCreated": "Job created successfully",
  "JobNotFound": "Job not found",
  "Status": "Status",
  "Customer": "Customer",
  "Car": "Car",
  "TotalAmount": "Total Amount"
}
```

```csharp
// Infrastructure/Localization/JsonStringLocalizer.cs
using System.Text.Json;

namespace ICMON.Infrastructure.Localization;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly Dictionary<string, string> _messages;

    public JsonStringLocalizer(string culture)
    {
        var path = $"Resources/Messages.{culture}.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            _messages = JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                ?? new Dictionary<string, string>();
        }
        else
        {
            _messages = new Dictionary<string, string>();
        }
    }

    public LocalizedString this[string name] 
        => new(name, _messages.GetValueOrDefault(name, name));

    public LocalizedString this[string name, params object[] arguments] 
        => new(name, string.Format(_messages.GetValueOrDefault(name, name), arguments));

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        => _messages.Select(kv => new LocalizedString(kv.Key, kv.Value));
}
```

---

## 5. Rate Limiting (ASP.NET Core Built-in)

```csharp
// Api/Program.cs
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Login", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(300);
    });
    options.AddFixedWindowLimiter("Create", opt =>
    {
        opt.PermitLimit = 20;
        opt.Window = TimeSpan.FromSeconds(60);
    });
    options.AddFixedWindowLimiter("Read", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromSeconds(60);
    });
    options.AddFixedWindowLimiter("PdfGeneration", opt =>
    {
        opt.PermitLimit = 15;
        opt.Window = TimeSpan.FromSeconds(300);
    });
});

app.UseRateLimiter();

// ใน Controller
[HttpPost("create")]
[EnableRateLimiting("Create")]
public async Task<IActionResult> CreateJob(CreateJobCommand command) { ... }
```

---

## 6. Background Jobs (Hangfire)

```csharp
// Infrastructure/BackgroundJobs/HangfireJobs.cs
using Hangfire;
using ICMON.Application.Services;

namespace ICMON.Infrastructure.BackgroundJobs;

public class DailyReportJob
{
    private readonly IReportService _reportService;
    private readonly ILogger<DailyReportJob> _logger;

    public DailyReportJob(IReportService reportService, ILogger<DailyReportJob> logger)
    {
        _reportService = reportService;
        _logger = logger;
    }

    public async Task Execute()
    {
        _logger.LogInformation("Running daily report job at {Time}", DateTime.UtcNow);
        await _reportService.GenerateDailyReportAsync();
    }
}

// Scheduling (ใน Program.cs หรือ Startup)
RecurringJob.AddOrUpdate<DailyReportJob>(
    "daily-report", 
    job => job.Execute(), 
    Cron.Daily(6, 30));
```

---

## 7. สรุป API Endpoints (ตามเอกสารเดิม)

| โมดูล | จำนวน Endpoints |
|-------|-----------------|
| Authentication | 7 |
| Job Card | 10 |
| Customer | 7 |
| Car | 7 |
| Quotation | 11 |
| Purchase Order | 12 |
| Inventory | 5 |
| Part Master | 7 |
| Part Picking | 7 |
| Payment | 8 |
| Receipt | 4 |
| Dashboard | 9 |
| Reports | 4 |
| Email | 5 |
| Batch Jobs | 7 |
| IoT Devices | 9 |
| MQTT | 1 |
| Geofence | 5 |
| WOS Catalogue | 6 |
| WOS Cart | 6 |
| WOS Orders | 8 |
| **รวม** | **~155 Endpoints** |

---

## 8. สรุป Redis Cache Keys (ตามเอกสารเดิม)

| Cache Name | Key Pattern | TTL |
|------------|-------------|-----|
| `user-permissions` | `{userId}` | 1 ชม. |
| `jobs` | `{jobId}` | 30 นาที |
| `customers` | `{customerId}` | 1 ชม. |
| `quotations` | `{quotationId}` | 30 นาที |
| `parts` | `{partId}` | 1 ชม. |
| `dashboard_overview` | `{whitelabelId}` | 5 นาที |
| ... | ... | ... |

---

## 9. สรุปการปรับเปลี่ยนจาก Java (Spring Boot) สู่ C# (.NET)

| รายการ | Java (Spring Boot) | C# (.NET) |
|--------|---------------------|-----------|
| **Framework** | Spring Boot 3.4 | ASP.NET Core 8 |
| **ORM** | Spring Data JPA (Hibernate) | Entity Framework Core |
| **Dependency Injection** | @Autowired | Constructor Injection (Built-in) |
| **CQRS** | Axon / Custom | MediatR |
| **Validation** | Bean Validation | FluentValidation |
| **Mapping** | ModelMapper / MapStruct | AutoMapper |
| **Caching** | Spring Cache + Redis | StackExchange.Redis |
| **Messaging** | Spring Kafka | Confluent.Kafka |
| **Auth** | Spring Security + JWT | ASP.NET Core Identity + JWT |
| **Background Jobs** | @Scheduled / Quartz | Hangfire / Quartz.NET |
| **Documentation** | Swagger (springdoc) | Swashbuckle |
| **PDF** | JasperReports | QuestPDF |
| **Excel** | Apache POI | EPPlus |
| **MQTT** | Eclipse Paho | MQTTnet |
| **Logging** | Logback / ELK | Serilog / ELK |
| **Rate Limiting** | Bucket4j | ASP.NET Core Rate Limiting |

---

## 10. การติดตั้งและการใช้งาน

### 10.1 ข้อกำหนดเบื้องต้น
- .NET 8 SDK
- Docker Desktop
- IDE: Visual Studio 2022 / JetBrains Rider / VS Code

### 10.2 ขั้นตอนการติดตั้ง

```bash
# 1. Clone Repository
git clone https://github.com/your-org/icmon.git
cd icmon

# 2. ตั้งค่า Environment Variables
cp .env.example .env
# แก้ไข .env ให้ตรงกับสภาพแวดล้อม

# 3. เริ่มต้นฐานข้อมูลด้วย Docker Compose
docker-compose up -d

# 4. Restore packages
dotnet restore

# 5. Run Migrations
dotnet ef database update --project src/ICMON.Infrastructure --startup-project src/ICMON.Api

# 6. รันแอปพลิเคชัน
dotnet run --project src/ICMON.Api --urls "http://localhost:1080"

# 7. เข้าถึง Swagger UI
# http://localhost:1080/swagger
```

### 10.3 Docker Compose

```yaml
# docker-compose.yml
version: '3.8'

services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: icmon
      POSTGRES_USER: icmon
      POSTGRES_PASSWORD: icmon123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"

  zookeeper:
    image: confluentinc/cp-zookeeper:latest
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
    ports:
      - "2181:2181"

  kafka:
    image: confluentinc/cp-kafka:latest
    depends_on:
      - zookeeper
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
    ports:
      - "9092:9092"

  elasticsearch:
    image: docker.elastic.co/elasticsearch/elasticsearch:8.10.0
    environment:
      - discovery.type=single-node
      - xpack.security.enabled=false
    ports:
      - "9200:9200"

volumes:
  postgres_data:
```

---

## ✅ สรุป

เอกสารนี้เป็นการออกแบบระบบ **ICMON** (Integrated Car Maintenance Operation Network) โดยใช้ **Clean Architecture + DDD + CQRS** ในภาษา C# (.NET 8) ตามโครงสร้างมาตรฐานที่คุณต้องการ:

- **4 Layers**: Domain → Application → Infrastructure → Presentation
- **CQRS with MediatR**: แยก Command และ Query
- **Event-Driven**: ใช้ Kafka สำหรับ Domain Events
- **Caching**: Redis สำหรับ Performance
- **Background Jobs**: Hangfire สำหรับงาน Batch
- **Multi-Language**: รองรับ 18 ภาษา (JSON Resources)
- **Rate Limiting**: ป้องกันการใช้งานเกินกำหนด
- **Testing**: Unit Tests, Integration Tests, Architecture Tests

ระบบพร้อมใช้งานเป็นเทมเพลตสำหรับพัฒนาแอปพลิเคชันจริงใน ecosystem .NET 🚀