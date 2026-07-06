# 🚗 ระบบบริหารจัดการอู่ซ่อมรถ – การออกแบบสำหรับภาษา C# (.NET)

**Auto Repair Shop Management System – C# (.NET) Design Specification**

---

| รายการ | รายละเอียด |
|--------|-----------|
| **ชื่อโครงการ** | ระบบบริหารจัดการอู่ซ่อมรถ (Auto Repair Shop Management System) |
| **ภาษาที่ใช้** | C# (.NET 8 / .NET 9) |
| **สถาปัตยกรรม** | Clean Architecture + DDD + Event‑Driven |
| **ผู้เขียน** | (Adapted from Kongnakorn Jantakun) |
| **วันที่** | 2026-07-06 |
| **เวอร์ชัน** | 1.0 (C# Adaptation) |
| **สถานะ** | ฉบับออกแบบ (Design Specification) |

---

## 1. บทนำและภาพรวมระบบ

### 1.1 วัตถุประสงค์ (เหมือนเดิม)

### 1.2 เทคโนโลยีหลัก (Tech Stack – .NET Edition)

| หมวดหมู่ | เทคโนโลยี | เวอร์ชัน / ไลบรารี |
|---------|-----------|-------------------|
| **ภาษา** | C# | 12+ (.NET 8 / .NET 9) |
| **Framework** | ASP.NET Core | 8.0+ |
| **ORM** | Entity Framework Core | 8.0+ |
| **ฐานข้อมูลหลัก** | PostgreSQL | 15+ (Npgsql) |
| **Cache** | Redis | 7+ (StackExchange.Redis) |
| **Message Queue** | Apache Kafka | 3.4+ (Confluent.Kafka) |
| **Logging & Monitoring** | Serilog + Elasticsearch + Grafana | - |
| **การจัดการเอกสาร** | QuestPDF / iTextSharp / PDFSharp | - |
| **IoT** | MQTTnet (MQTT), InfluxDB.Client | - |
| **Documentation** | Swashbuckle (Swagger) | - |
| **Build Tool** | .NET CLI / MSBuild | - |
| **Dependency Injection** | Built-in (Microsoft.Extensions.DependencyInjection) | - |
| **Mapping** | AutoMapper | 12.0+ |
| **Validation** | FluentValidation | 11.0+ |
| **Background Jobs** | Hangfire / Quartz.NET | - |
| **Real-time** | SignalR (optional) | - |

---

## 2. สถาปัตยกรรมระบบ (Clean Architecture + DDD)

### 2.1 แผนภาพสถาปัตยกรรม (ปรับเป็น .NET)

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           PRESENTATION LAYER                               │
│                    (ASP.NET Core Web API + Swagger)                         │
│                   Controllers, Filters, Middleware                          │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                           APPLICATION LAYER                                │
│              (CQRS with MediatR / Use Cases / DTOs)                        │
│         Commands, Queries, Handlers, Validators, Mappers                   │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                            DOMAIN LAYER                                    │
│              (Entities, Value Objects, Aggregates, Interfaces)             │
│         Domain Events, Repository Interfaces, Business Rules               │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                         INFRASTRUCTURE LAYER                               │
│    (EF Core Repositories, Unit of Work, Redis Cache, Kafka Producer/Consumer)│
│    MQTT Client, Email Sender, File Storage, PDF Generation, Hangfire Jobs   │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
                      ┌───────────────┼───────────────┐
                      ▼               ▼               ▼
               ┌───────────┐  ┌───────────┐  ┌───────────┐
               │PostgreSQL │  │  Redis    │  │  Kafka    │
               │(EF Core)  │  │(Cache)    │  │(Event Bus)│
               └───────────┘  └───────────┘  └─────┬─────┘
                                                    │
                              ┌─────────────────────┼─────────────┐
                              ▼                     ▼             ▼
                       ┌───────────┐         ┌───────────┐ ┌───────────┐
                       │Elasticsearch│         │ InfluxDB  │ │ Grafana   │
                       │ (Logs)     │         │ (IoT)     │ │ (Metrics) │
                       └───────────┘         └───────────┘ └───────────┘
```

### 2.2 หลักการออกแบบ (เหมือนเดิม)
- **Separation of Concerns**
- **Dependency Inversion**: Domain ไม่ขึ้นกับ Infrastructure
- **CQRS + Event Sourcing** (บางส่วน)
- **Repository + Unit of Work**
- **Domain Events** เพื่อแยกเหตุการณ์

---

## 3. ตัวอย่างโครงสร้างโปรเจกต์ (Solution Structure)

```
AutoRepairShop.sln
├── AutoRepairShop.Domain                     (Class Library)
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── Job.cs
│   │   ├── Quotation.cs
│   │   ├── Customer.cs
│   │   └── ...
│   ├── ValueObjects/
│   │   ├── Money.cs
│   │   ├── Address.cs
│   │   └── ...
│   ├── Aggregates/
│   │   ├── JobAggregate.cs
│   │   └── ...
│   ├── Enums/
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   ├── IUnitOfWork.cs
│   │   └── IEventPublisher.cs
│   ├── Events/
│   │   ├── JobCreatedEvent.cs
│   │   ├── QuotationApprovedEvent.cs
│   │   └── ...
│   └── Exceptions/
├── AutoRepairShop.Application               (Class Library)
│   ├── Commands/
│   │   ├── Jobs/
│   │   │   ├── CreateJobCommand.cs
│   │   │   ├── CreateJobCommandHandler.cs
│   │   │   └── CreateJobCommandValidator.cs
│   │   ├── Quotations/
│   │   └── ...
│   ├── Queries/
│   │   ├── Jobs/
│   │   │   ├── GetJobByIdQuery.cs
│   │   │   ├── GetJobByIdQueryHandler.cs
│   │   │   └── GetJobListQuery.cs
│   │   └── ...
│   ├── DTOs/
│   │   ├── JobDto.cs
│   │   ├── QuotationDto.cs
│   │   └── ...
│   ├── Mappings/
│   │   └── AutoMapperProfile.cs
│   ├── Validators/
│   ├── Services/
│   │   ├── IEmailService.cs
│   │   ├── IDocumentGenerator.cs
│   │   └── ...
│   └── Common/
│       ├── BaseCommand.cs
│       ├── BaseQuery.cs
│       └── Result.cs
├── AutoRepairShop.Infrastructure             (Class Library)
│   ├── Persistence/
│   │   ├── AppDbContext.cs
│   │   ├── Configurations/
│   │   │   ├── JobConfiguration.cs
│   │   │   └── ...
│   │   ├── Repositories/
│   │   │   ├── GenericRepository.cs
│   │   │   ├── JobRepository.cs
│   │   │   └── ...
│   │   ├── UnitOfWork.cs
│   │   └── Migrations/
│   ├── Cache/
│   │   ├── RedisCacheService.cs
│   │   └── ICacheService.cs
│   ├── Messaging/
│   │   ├── KafkaProducer.cs
│   │   ├── KafkaConsumer.cs
│   │   └── EventPublisher.cs
│   ├── IoT/
│   │   ├── MqttClientService.cs
│   │   └── InfluxDbService.cs
│   ├── BackgroundJobs/
│   │   ├── HangfireJobs.cs
│   │   └── QuartzJobs.cs
│   ├── DocumentGeneration/
│   │   ├── PdfGenerator.cs (using QuestPDF)
│   │   └── ExcelGenerator.cs (using EPPlus)
│   ├── Email/
│   │   └── SmtpEmailService.cs
│   ├── Authentication/
│   │   ├── JwtTokenService.cs
│   │   └── CustomIdentityService.cs
│   └── Extensions/
│       └── ServiceCollectionExtensions.cs
├── AutoRepairShop.Api                       (ASP.NET Core Web API)
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── JobsController.cs
│   │   ├── QuotationsController.cs
│   │   └── ...
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   ├── RateLimitingMiddleware.cs
│   │   └── RequestLoggingMiddleware.cs
│   ├── Filters/
│   │   └── ApiKeyFilter.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Properties/
│       └── launchSettings.json
├── AutoRepairShop.Shared                    (Class Library)
│   ├── Constants/
│   ├── Helpers/
│   ├── Extensions/
│   └── Resources/                           (สำหรับ i18n)
│       ├── Messages.th.resx
│       ├── Messages.en.resx
│       └── ...
└── AutoRepairShop.Tests                     (xUnit / NUnit)
    ├── UnitTests/
    ├── IntegrationTests/
    └── Mock/
```

---

## 4. การออกแบบ Domain Models (Entities หลัก)

### 4.1 ตัวอย่าง Entity – `Job` (C# Class)

```csharp
using AutoRepairShop.Domain.Enums;
using AutoRepairShop.Domain.Events;
using AutoRepairShop.Domain.ValueObjects;

namespace AutoRepairShop.Domain.Entities;

public class Job : AggregateRoot<Guid>
{
    public string JobNo { get; private set; }
    public Guid CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public Guid CarId { get; private set; }
    public virtual Car Car { get; private set; }
    public Guid? MechanicId { get; private set; }
    public virtual Staff Mechanic { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime ReceivedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public string Description { get; private set; }
    public Money TotalAmount { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private List<JobService> _services = new();
    public IReadOnlyList<JobService> Services => _services.AsReadOnly();

    private List<JobPartSales> _parts = new();
    public IReadOnlyList<JobPartSales> Parts => _parts.AsReadOnly();

    private List<JobStatusHistory> _statusHistory = new();
    public IReadOnlyList<JobStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    // Private constructor for EF Core
    private Job() { }

    // Factory method
    public static Job Create(Guid customerId, Guid carId, string description, Guid whitelabelId, Guid? mechanicId = null)
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

    public void ChangeStatus(JobStatus newStatus, string note = null)
    {
        if (!CanTransitionTo(newStatus))
            throw new DomainException($"Cannot transition from {Status} to {newStatus}");

        Status = newStatus;
        _statusHistory.Add(JobStatusHistory.Create(Id, newStatus, note));

        if (newStatus == JobStatus.Closed)
            CompletedDate = DateTime.UtcNow;

        AddDomainEvent(new JobStatusChangedEvent(Id, newStatus));
    }

    public void AddService(Service service, int quantity, Money unitPrice)
    {
        var jobService = JobService.Create(Id, service.Id, quantity, unitPrice);
        _services.Add(jobService);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        var serviceTotal = _services.Sum(s => s.TotalAmount);
        var partTotal = _parts.Sum(p => p.TotalAmount);
        TotalAmount = serviceTotal + partTotal;
    }

    private static string GenerateJobNo() => $"JOB-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6]}";

    private bool CanTransitionTo(JobStatus newStatus)
    {
        // Business rules for state transitions
        return newStatus switch
        {
            JobStatus.InProgress => Status == JobStatus.Open || Status == JobStatus.QuotationPending,
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

### 4.2 Value Object – `Money`

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Zero => new Money(0, "THB");

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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
```

---

## 5. Application Layer – CQRS with MediatR

### 5.1 การติดตั้งแพ็กเกจหลัก

```xml
<PackageReference Include="MediatR" Version="12.2.0" />
<PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="11.1.0" />
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
```

### 5.2 ตัวอย่าง Command + Handler

```csharp
// Command
public class CreateJobCommand : IRequest<Result<JobDto>>
{
    public Guid CustomerId { get; set; }
    public Guid CarId { get; set; }
    public string Description { get; set; }
    public Guid? MechanicId { get; set; }
    public List<ServiceItemDto> Services { get; set; }
}

// Handler
public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<JobDto>>
{
    private readonly IJobRepository _jobRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public CreateJobCommandHandler(
        IJobRepository jobRepo,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEventPublisher eventPublisher)
    {
        _jobRepo = jobRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        // Validate customer/car existence, etc.
        var job = Job.Create(
            request.CustomerId,
            request.CarId,
            request.Description,
            whitelabelId: Guid.Parse("..."),
            request.MechanicId
        );

        // Add services
        foreach (var svc in request.Services)
        {
            // fetch service entity
            job.AddService(service, svc.Quantity, new Money(svc.UnitPrice));
        }

        await _jobRepo.AddAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish domain events
        await _eventPublisher.Publish(job.DomainEvents);

        var dto = _mapper.Map<JobDto>(job);
        return Result<JobDto>.Success(dto);
    }
}

// Validator
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
        });
    }
}
```

### 5.3 Query Example

```csharp
public class GetJobByIdQuery : IRequest<Result<JobDetailDto>>
{
    public Guid JobId { get; set; }
}

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Result<JobDetailDto>>
{
    private readonly IJobRepository _jobRepo;
    private readonly IMapper _mapper;

    public async Task<Result<JobDetailDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await _jobRepo.GetByIdWithDetailsAsync(request.JobId, cancellationToken);
        if (job == null)
            return Result<JobDetailDto>.Failure("Job not found");

        var dto = _mapper.Map<JobDetailDto>(job);
        return Result<JobDetailDto>.Success(dto);
    }
}
```

---

## 6. Infrastructure Layer – Persistence (EF Core)

### 6.1 DbContext

```csharp
public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    // ... other DbSets

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Handle domain events before save
        var entities = ChangeTracker.Entries<AggregateRoot<Guid>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();

        foreach (var entity in entities)
            entity.ClearEvents();

        var result = await base.SaveChangesAsync(cancellationToken);

        // Publish events after save (can be done via outbox pattern)
        // Or we can use a separate EventDispatcher

        return result;
    }
}
```

### 6.2 Entity Configurations (Fluent API)

```csharp
public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("t_job");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.JobNo).HasMaxLength(20).IsRequired();
        builder.HasIndex(j => j.JobNo).IsUnique();
        builder.Property(j => j.Status).HasConversion<string>();
        builder.OwnsOne(j => j.TotalAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("TotalAmount");
            money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3);
        });
        builder.HasMany(j => j.Services).WithOne().HasForeignKey("JobId");
        builder.HasMany(j => j.Parts).WithOne().HasForeignKey("JobId");
        builder.HasMany(j => j.StatusHistory).WithOne().HasForeignKey("JobId");
        // relationships
        builder.HasOne(j => j.Customer).WithMany().HasForeignKey(j => j.CustomerId);
        builder.HasOne(j => j.Car).WithMany().HasForeignKey(j => j.CarId);
    }
}
```

### 6.3 Generic Repository

```csharp
public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}
```

---

## 7. Authentication & Authorization

### 7.1 JWT Authentication (ASP.NET Core Identity + JWT)

```csharp
// Program.cs
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("JobCreate", policy => policy.RequireClaim("permission", "JOB_CREATE"));
    // ...
});
```

### 7.2 Custom Permission-based Authorization

```csharp
public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission) => Permission = permission;
}

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserPermissionService _permissionService;

    public PermissionHandler(IUserPermissionService permissionService)
        => _permissionService = permissionService;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null && await _permissionService.HasPermissionAsync(Guid.Parse(userId), requirement.Permission))
            context.Succeed(requirement);
        else
            context.Fail();
    }
}
```

### 7.3 ใช้ Attribute

```csharp
[Authorize(Policy = "JobCreate")]
[HttpPost("create")]
public async Task<IActionResult> CreateJob(CreateJobCommand command)
{
    // ...
}
```

---

## 8. Caching with Redis

### 8.1 Interface

```csharp
public interface ICacheService
{
    Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);
}
```

### 8.2 Implementation using StackExchange.Redis

```csharp
public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCacheService> logger)
    {
        _database = connectionMultiplexer.GetDatabase();
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, json, expiry);
    }

    // ... other methods
}
```

### 8.3 ตัวอย่างการใช้ Cache ใน Repository

```csharp
public async Task<Job> GetByIdAsync(Guid id, CancellationToken cancellationToken)
{
    var cacheKey = $"jobs:{id}";
    var cached = await _cacheService.GetAsync<Job>(cacheKey, cancellationToken);
    if (cached != null)
        return cached;

    var job = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    if (job != null)
        await _cacheService.SetAsync(cacheKey, job, TimeSpan.FromMinutes(30), cancellationToken);

    return job;
}
```

---

## 9. Event-Driven with Kafka

### 9.1 Producer

```csharp
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
        _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent
    {
        var message = JsonSerializer.Serialize(@event, _jsonOptions);
        await _producer.ProduceAsync(
            topic: @event.GetType().Name,
            message: new Message<string, string> { Key = @event.AggregateId.ToString(), Value = message },
            cancellationToken
        );
    }
}
```

### 9.2 Consumer (Background Service)

```csharp
public class JobEventConsumer : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<JobEventConsumer> _logger;

    public JobEventConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<JobEventConsumer> logger)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "job-event-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _consumer.Subscribe("JobCreatedEvent");
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult != null)
                {
                    await ProcessEvent(consumeResult.Message.Value, stoppingToken);
                    _consumer.Commit(consumeResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Kafka message");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    private async Task ProcessEvent(string json, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var jobEvent = JsonSerializer.Deserialize<JobCreatedEvent>(json);
        // handle event (e.g., send email, update dashboard, etc.)
    }
}
```

---

## 10. Background Jobs (Hangfire / Quartz.NET)

### 10.1 ใช้ Hangfire

```csharp
// Program.cs
builder.Services.AddHangfire(config => config
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

// Job definition
public class DailyReportJob
{
    private readonly ILogger<DailyReportJob> _logger;
    private readonly IReportService _reportService;

    public DailyReportJob(ILogger<DailyReportJob> logger, IReportService reportService)
    {
        _logger = logger;
        _reportService = reportService;
    }

    public async Task Execute()
    {
        _logger.LogInformation("Running daily report job at {Time}", DateTime.UtcNow);
        await _reportService.GenerateDailyReportAsync();
    }
}

// Schedule job (in Startup or Program)
RecurringJob.AddOrUpdate<DailyReportJob>("daily-report", job => job.Execute(), Cron.Daily(6, 30));
```

### 10.2 6 Batch Jobs (ตามเอกสาร)

| Job ID | Cron Expression | หน้าที่ |
|--------|-----------------|--------|
| batch001 | `0 30 6 * * ?` | ส่งอีเมลแจ้งเตือนรายวัน |
| batch002 | `0 45 6 * * ?` | สร้างรายงานประจำวัน |
| batch003 | `0 30 6 * * ?` | อัปเดตสถานะงานค้าง |
| batch004 | `0 0 3 * * ?` | ล้างข้อมูล/ซิงค์ฐานข้อมูล |
| batch005 | `0 0/30 * * * ?` | ซิงค์ข้อมูล Realtime |
| batch006 | `0 30 6 * * ?` | ส่งสรุปยอดขาย |

---

## 11. Multi-Language (i18n) – Resource Files

### 11.1 ใช้ Resource (.resx) ไฟล์

สร้างไฟล์ใน `AutoRepairShop.Shared/Resources/`

- `Messages.th.resx` (ไทย)
- `Messages.en.resx` (อังกฤษ)
- `Messages.zh.resx` (จีน)
- ... 18 ภาษา

### 11.2 ตั้งค่า Localization

```csharp
// Program.cs
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("th"),
        new CultureInfo("en"),
        new CultureInfo("zh"),
        // ...
    };
    options.DefaultRequestCulture = new RequestCulture("th");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

app.UseRequestLocalization();
```

### 11.3 ใช้ IStringLocalizer

```csharp
public class HomeController : ControllerBase
{
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    [HttpGet("greeting")]
    public IActionResult GetGreeting()
    {
        return Ok(new { message = _localizer["WelcomeMessage"] });
    }
}
```

---

## 12. Document Generation (PDF, Excel)

### 12.1 PDF – ใช้ QuestPDF (แทน JasperReports)

```csharp
public class PdfGenerator : IDocumentGenerator
{
    public async Task<byte[]> GenerateJobReportAsync(Job job, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Header().Text($"Job Report: {job.JobNo}").FontSize(18).Bold();
                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        col.Item().Text($"Customer: {job.Customer.FullName}");
                        col.Item().Text($"Car: {job.Car.LicensePlate}");
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            // ... add rows
                        });
                    });
                    page.Footer().AlignCenter().Text("Generated by Auto Repair System");
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }, cancellationToken);
    }
}
```

### 12.2 Excel – ใช้ EPPlus

```csharp
public class ExcelGenerator : IExcelGenerator
{
    public async Task<byte[]> GenerateInventoryReportAsync(IEnumerable<PartMaster> parts, CancellationToken cancellationToken)
    {
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Inventory");
        worksheet.Cells[1, 1].Value = "Part Code";
        worksheet.Cells[1, 2].Value = "Part Name";
        worksheet.Cells[1, 3].Value = "Stock Quantity";
        // ... fill data

        int row = 2;
        foreach (var part in parts)
        {
            worksheet.Cells[row, 1].Value = part.PartCode;
            worksheet.Cells[row, 2].Value = part.PartName;
            worksheet.Cells[row, 3].Value = part.StockQuantity;
            row++;
        }

        return await Task.FromResult(package.GetAsByteArray());
    }
}
```

---

## 13. IoT & GPS Tracking (MQTT)

### 13.1 ใช้ MQTTnet

```csharp
public class MqttClientService : IHostedService
{
    private readonly IMqttClient _mqttClient;
    private readonly MqttClientOptions _options;
    private readonly ILogger<MqttClientService> _logger;

    public MqttClientService(IConfiguration configuration, ILogger<MqttClientService> logger)
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();
        _options = new MqttClientOptionsBuilder()
            .WithTcpServer(configuration["Mqtt:Broker"], int.Parse(configuration["Mqtt:Port"]))
            .WithClientId(Guid.NewGuid().ToString())
            .Build();
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _mqttClient.ConnectedAsync += OnConnectedAsync;
        _mqttClient.DisconnectedAsync += OnDisconnectedAsync;
        _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
        await _mqttClient.ConnectAsync(_options, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
    }

    private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
    {
        var payload = Encoding.UTF8.GetString(args.ApplicationMessage.PayloadSegment);
        var topic = args.ApplicationMessage.Topic;
        _logger.LogInformation("Received message on {Topic}: {Payload}", topic, payload);

        // Process GPS data, device status, etc.
        // e.g., parse JSON and store in InfluxDB
    }
}
```

---

## 14. API Controllers & Rate Limiting

### 14.1 Controller Example

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("create")]
    [ProducesResponseType(typeof(Result<JobDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<JobDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJob(Guid id)
    {
        var result = await _mediator.Send(new GetJobByIdQuery { JobId = id });
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}
```

### 14.2 Rate Limiting – ใช้ AspNetCoreRateLimit

```csharp
// Program.cs
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
app.UseIpRateLimiting();
```

หรือใช้ **ASP.NET Core Rate Limiting Middleware** (ใน .NET 7+)

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Login", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(300);
    });
    // ... others
});

app.UseRateLimiter();

[HttpPost("login")]
[EnableRateLimiting("Login")]
public async Task<IActionResult> Login(LoginCommand command) { ... }
```

---

## 15. Logging & Monitoring (Serilog + ELK)

```csharp
// Program.cs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "autorepair-{0:yyyy.MM.dd}"
    })
    .CreateLogger();

builder.Host.UseSerilog();

app.UseSerilogRequestLogging();
```

---

## 16. ฐานข้อมูล – Entity Framework Core + Migrations

### 16.1 ตารางทั้งหมด (อ้างอิงจากเอกสาร) – สร้างเป็น DbSet และ Configurations

### 16.2 การ Migrate

```bash
dotnet ef migrations add InitialCreate --project AutoRepairShop.Infrastructure --startup-project AutoRepairShop.Api
dotnet ef database update --project AutoRepairShop.Infrastructure --startup-project AutoRepairShop.Api
```

---

## 17. สรุปการปรับเปลี่ยนจาก Java (Spring Boot) สู่ C# (.NET)

| รายการ | Java (Spring Boot) | C# (.NET) |
|--------|---------------------|-----------|
| **Framework** | Spring Boot 3.4 | ASP.NET Core 8 |
| **ORM** | Spring Data JPA (Hibernate) | Entity Framework Core |
| **Dependency Injection** | @Autowired | Constructor Injection (Built-in) |
| **Validation** | Bean Validation (Hibernate) | FluentValidation |
| **Mapping** | ModelMapper / MapStruct | AutoMapper |
| **Caching** | Spring Cache + Redis | StackExchange.Redis |
| **Messaging** | Spring Kafka | Confluent.Kafka |
| **Auth** | Spring Security + JWT | ASP.NET Core Identity + JWT |
| **Background Jobs** | @Scheduled / Quartz | Hangfire / Quartz.NET |
| **Documentation** | Swagger (springdoc) | Swashbuckle |
| **PDF Generation** | JasperReports | QuestPDF / iTextSharp |
| **Excel** | Apache POI | EPPlus |
| **MQTT** | Eclipse Paho | MQTTnet |
| **Logging** | Logback / ELK | Serilog / ELK |

---

## 18. สรุป API Endpoints (เหมือนเดิม) – แต่ใช้ C# Attribute Routing

ตัวอย่าง Route Prefix: `[Route("api/[controller]")]` และใช้ `[HttpGet]`, `[HttpPost]` ตามมาตรฐาน

---

## 19. สรุป Redis Cache Keys (เหมือนเดิม) – ใช้ใน `ICacheService`

---

## 20. สรุป Rate Limit Policy (เหมือนเดิม) – ใช้ `IRateLimiter`

---

## 21. การติดตั้งและการใช้งาน (สำหรับ .NET)

### 21.1 ข้อกำหนดเบื้องต้น
- .NET 8 SDK
- Docker Desktop (สำหรับ PostgreSQL, Redis, Kafka, Elasticsearch)
- IDE: Visual Studio 2022 / Rider / VS Code

### 21.2 ขั้นตอน
```bash
# 1. Clone repository
git clone https://github.com/your-org/auto-repair-dotnet.git
cd auto-repair-dotnet

# 2. ตั้งค่า appsettings.json และ Docker Compose
docker-compose up -d

# 3. Restore packages
dotnet restore

# 4. Run migrations
dotnet ef database update --project AutoRepairShop.Infrastructure --startup-project AutoRepairShop.Api

# 5. Run the API
dotnet run --project AutoRepairShop.Api --urls "http://localhost:1080"

# 6. เปิด Swagger UI
# http://localhost:1080/swagger
```

---

## ✅ สรุป

เอกสารนี้เป็นการปรับเปลี่ยนการออกแบบระบบบริหารจัดการอู่ซ่อมรถจาก Java (Spring Boot) สู่ **C# (.NET 8)** โดยยังคงฟังก์ชันการทำงานครบถ้วนตามโมดูลเดิม แต่ใช้เทคโนโลยีและแนวทางปฏิบัติที่ดีที่สุดของ .NET เช่น:

- Clean Architecture + DDD
- CQRS with MediatR
- Entity Framework Core
- Redis Caching
- Kafka Event Bus
- Hangfire for Background Jobs
- MQTTnet for IoT
- QuestPDF for PDF generation
- Serilog for logging
- FluentValidation
- AutoMapper

ระบบพร้อมใช้งานเป็นเทมเพลตสำหรับพัฒนาแอปพลิเคชันจริงใน ecosystem .NET

---
**Adapted by:** (ผู้พัฒนา)  
**วันที่:** 2026-07-06  
**สถานะ:** ฉบับออกแบบสมบูรณ์ ✅