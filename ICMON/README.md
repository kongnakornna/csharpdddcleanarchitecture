# ICMON - Integrated Car Maintenance Operation Network

ระบบบริหารจัดการอู่ซ่อมรถ (Auto Repair Shop Management System)

Built with **Clean Architecture + DDD + CQRS + Event-Driven** in C# (.NET 8)

## Architecture

```
ICMON.sln
├── src/
│   ├── ICMON.Domain          (Entities, ValueObjects, Aggregates, Interfaces)
│   ├── ICMON.Application     (Commands, Queries, DTOs, Mappings, Validators)
│   ├── ICMON.Infrastructure  (EF Core, Redis, Kafka, MQTT, Hangfire)
│   ├── ICMON.Api             (Controllers, Middleware, Authorization)
│   └── ICMON.Shared          (Constants, Helpers, Resources)
├── tests/
│   ├── ICMON.UnitTests
│   ├── ICMON.IntegrationTests
│   └── ICMON.ArchitectureTests
└── docs/
    └── modules/
```

## Modules

| # | Module | Status |
|---|--------|--------|
| 1 | [Authentication & Permission](docs/modules/Module_01_Authentication.md) | ✅ |
| 2 | [Job Card Management](docs/modules/Module_02_JobCard.md) | 📝 |
| 3 | [Customer Management](docs/modules/Module_03_Customer.md) | 📝 |
| 4 | [Quotation](docs/modules/Module_04_Quotation.md) | 📝 |
| 5 | [Purchase Order](docs/modules/Module_05_PurchaseOrder.md) | 📝 |
| 6 | [Inventory Management](docs/modules/Module_06_Inventory.md) | 📝 |
| 7 | [Payment Management](docs/modules/Module_07_Payment.md) | 📝 |
| 8 | [Dashboard & Reports](docs/modules/Module_08_Dashboard.md) | 📝 |
| 9 | [Document Management](docs/modules/Module_09_Document.md) | 📝 |
| 10 | [Email Service](docs/modules/Module_10_Email.md) | 📝 |
| 11 | [Batch Jobs](docs/modules/Module_11_BatchJobs.md) | 📝 |
| 12 | [Multi-Language (i18n)](docs/modules/Module_12_i18n.md) | 📝 |
| 13 | [IoT & GPS Tracking](docs/modules/Module_13_IoT.md) | 📝 |
| 14 | [Web Order System (WOS)](docs/modules/Module_14_WOS.md) | 📝 |

## Tech Stack

| Category | Technology |
|----------|-----------|
| Language | C# 12 (.NET 8) |
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core 8 (Npgsql) |
| Database | PostgreSQL 15+ |
| Cache | Redis 7+ (StackExchange.Redis) |
| Message Queue | Apache Kafka (Confluent.Kafka) |
| CQRS | MediatR |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| Auth | JWT (System.IdentityModel.Tokens.Jwt) |
| Background Jobs | Hangfire |
| PDF | QuestPDF |
| Excel | EPPlus |
| IoT | MQTTnet |
| Logging | Serilog + Elasticsearch |
| API Docs | Swagger / Swashbuckle |

## Quick Start

```bash
# Prerequisites: .NET 8 SDK, Docker

# Clone and setup
git clone <repo-url>
cd ICMON

# Start infrastructure
docker-compose up -d

# Run migrations
dotnet ef database update --project src/ICMON.Infrastructure --startup-project src/ICMON.Api

# Run application
dotnet run --project src/ICMON.Api --urls "http://localhost:1080"

# Open Swagger UI
# http://localhost:1080/swagger
```

## Solution Structure Details

### Domain Layer (`ICMON.Domain`)
- No external dependencies
- Contains entities, value objects, aggregates, domain events, interfaces
- Business logic and business rules

### Application Layer (`ICMON.Application`)
- Depends only on Domain layer
- CQRS with MediatR (Commands, Queries, Handlers)
- DTOs, AutoMapper profiles, FluentValidation validators
- Pipeline behaviors (validation, logging, performance)

### Infrastructure Layer (`ICMON.Infrastructure`)
- Implements Domain and Application interfaces
- EF Core DbContext, migrations, repositories
- Redis caching, Kafka messaging, MQTT client
- Hangfire background jobs, PDF/Excel generation

### Presentation Layer (`ICMON.Api`)
- ASP.NET Core Web API controllers
- JWT authentication, permission-based authorization
- Rate limiting, exception handling middleware
- Swagger documentation
