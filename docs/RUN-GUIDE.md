# คู่มือการรันโปรเจกต์ C# (Clean Architecture + DDD)

โปรเจกต์นี้ประกอบด้วย 2 ส่วนหลัก:

| ส่วน | โฟลเดอร์ | คำอธิบาย |
|------|----------|----------|
| **ICMON** | `ICMON/` | แอปพลิเคชันจริง (Auto Repair Shop Management) เป็น Web API .NET 8 พร้อม Docker |
| **Template** | `content/` | ซอร์สเทมเพลต `CA.And.DDD.Template` (net10.0) สำหรับ `dotnet new` |

คู่มือนี้เน้นการรันแอป **ICMON** เพราะเป็นโปรเจกต์ที่รันได้จริงในโฟลเดอร์นี้ และมีส่วนของ Template ไว้ท้ายสุด

---

## 1. สิ่งที่ต้องติดตั้งก่อน (Prerequisites)

- **.NET 8 SDK** — สำหรับรันแอป ICMON (target `net8.0`)
  - ตรวจสอบด้วยคำสั่ง: `dotnet --list-sdks`
- **.NET 10 SDK** — สำหรับเทมเพลต (target `net10.0`, มี `global.json` บังคับที่ `content/`)
- **Docker Desktop** — ใช้รัน PostgreSQL, Redis, Kafka, Elasticsearch
  - Windows ควรใช้ WSL2 backend
- **Visual Studio 2022+ / VS Code** (ตามสะดวก) หรือใช้ CLI อย่างเดียวก็ได้

> หมายเหตุ: บนเครื่องนี้มี SDK 8.0.423, 10.0.301/303 ติดตั้งอยู่แล้วจึงรันได้ทั้ง 2 ส่วน

---

## 2. วิธีรันแอป ICMON (หลัก)

```
ICMON/
├── src/
│   ├── ICMON.Domain          # Entities, Value Objects, Aggregates
│   ├── ICMON.Application     # Commands, Queries, Handlers (CQRS + MediatR)
│   ├── ICMON.Infrastructure  # EF Core, Redis, Kafka, Hangfire
│   ├── ICMON.Api             # Web API Controller + JWT Auth + Swagger
│   └── ICMON.Shared
└── tests/
```

### ขั้นตอนที่ 1: เริ่ม infrastructure ทั้งหมดด้วย Docker

เปิด Terminal แล้วเข้าไปที่โฟลเดอร์ `ICMON`:

```bash
cd ICMON
docker compose up -d
```

คำสั่งนี้จะเริ่ม container เหล่านี้:

| Service | URL / Port | ใช้เพื่อ |
|---------|-----------|---------|
| PostgreSQL 15 | `localhost:5432` | ฐานข้อมูลหลัก |
| Redis 7 | `localhost:6379` | Cache |
| Zookeeper | `localhost:2181` | Kafka จำเป็น |
| Kafka | `localhost:9092` | Message Queue |
| Elasticsearch | `localhost:9200` | Logging (Serilog) |

ตรวจสอบว่า container ทำงานครบ:

```bash
docker compose ps
```

> **ค่า config เริ่มต้น** (ดูได้จาก `ICMON/.env` + `ICMON/src/ICMON.Api/appsettings.json`):
> - Database: `icmonappcdd` / user `postgres` / password `postgres`
> - Redis: `localhost:6379`

### ขั้นตอนที่ 2: รันแอปพลิเคชัน

```bash
dotnet run --project src/ICMON.Api
```

หรือระบุพอร์ตเองตาม README ของโปรเจกต์:

```bash
dotnet run --project src/ICMON.Api --urls "http://localhost:1080"
```

แอปจะ:

- **สร้างฐานข้อมูล + Schema อัตโนมัติ** ด้วย `EnsureCreatedAsync()` (`ICMON/src/ICMON.Infrastructure/Persistence/SeedData/AppDbInitializer.cs`)
- **Seed ข้อมูลเริ่มต้น**: Roles, Permissions และบัญชีผู้ใช้ **admin**
- เปิด Swagger ขึ้นเบราว์เซอร์โดยอัตโนมัติ (โหมด Development)

### ขั้นตอนที่ 3: เข้าใช้งาน

- **Swagger UI:** http://localhost:5009/swagger
- **API Base URL:** http://localhost:5009

**บัญชีที่ seed มาให้ (ใช้งานได้ทันที):**

| Username | Password |
|----------|----------|
| `admin` | `Admin@123` |

**ทดสอบ Login ผ่าน API:**

```bash
curl -X POST http://localhost:5009/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"usernameOrEmail":"admin","password":"Admin@123"}'
```

จะได้ response พร้อม `accessToken` / `refreshToken` จากนั้นส่ง token ต่อในส่วน **Authorize** ของ Swagger (หรือ header `Authorization: Bearer <token>`) สำหรับเรียก endpoint อื่น เช่น `/api/auth/me`

### การตั้งค่าที่แก้ไขได้ (Configuration)

ไฟล์คอนฟิกหลัก: `ICMON/src/ICMON.Api/appsettings.json`

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=icmonappcdd;Username=postgres;Password=postgres;SSL Mode=Disable",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Issuer": "ICMON",
    "Audience": "ICMON-API",
    "SecretKey": "ThisIsASuperSecretKeyForJwtTokenGeneration12345678"  // เปลี่ยนใน production
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092"
  }
}
```

> `appsettings.Development.json` ใช้สำหรับเพิ่มระดับ logging (Debug) ในโหมด Development

---

## 3. รัน Test ของ ICMON

รันทีละโปรเจกต์ (เพราะ `ICMON.slnx` มีเฉพาะ Domain + ArchitectureTests):

```bash
cd ICMON

dotnet test tests/ICMON.UnitTests
dotnet test tests/ICMON.ArchitectureTests
dotnet test tests/ICMON.IntegrationTests
```

หมายเหตุ: `ICMON.slnx` (solution ใหม่) ต้องใช้ .NET SDK 9.0.200+ ขึ้นไป ถ้าต้องการ `dotnet test ICMON.slnx`

---

## 4. วิธีรัน Template ตัวอย่าง (CA.And.DDD.Template)

เทมเพลตใน `content/` ต้องการ **.NET 10 SDK** และ stack ต่างจาก ICMON (ใช้ RabbitMQ + Keycloak + MailHog + Aspire Dashboard)

### วิธีที่ 1: รันตรงจาก source (ไม่แนะนำ)

```bash
cd content
docker compose up -d          # RabbitMQ, Keycloak, Redis, Postgres, MailHog, Aspire Dashboard
dotnet run --project src/CA.And.DDD.Template.WebApi   # http://localhost:5008
```

- Keycloak: http://localhost:8080 (user `admin` / `admin`)
- Aspire Dashboard: http://localhost:18888
- RabbitMQ Management: http://localhost:15672 (guest/guest)
- Swagger: http://localhost:5008/swagger

### วิธีที่ 2: สร้างโปรเจกต์ใหม่จาก Template (แนะนำ)

```bash
# 1) ติดตั้ง template จาก repo นี้
dotnet new install .          # รันจาก root ของ repo (แพ็กจ์ nuget-package.csproj)

# 2) สร้างโปรเจกต์ใหม่ (แทนที่ MyDreamProject ด้วยชื่อโปรเจกต์ตัวเอง)
dotnet new ca-and-ddd -o MyDreamProject

# 3) รัน infrastructure
cd MyDreamProject
docker compose up

# 4) รันแอป
dotnet run --project src/MyDreamProject.WebApi
```

Migration จะถูกสร้างด้วย EF Core โหมด Development รวมถึง Keycloak realm ถูก import ให้อัตโนมัติ

---

## 5. ตรวจสอบเงื่อนไขการรัน (Cheat Sheet)

| งาน | คำสั่ง |
|-----|-------|
| รัน API (ICMON) | `cd ICMON && dotnet run --project src/ICMON.Api` |
| รัน infrastructure | `cd ICMON && docker compose up -d` |
| หยุด infrastructure | `cd ICMON && docker compose down` |
| รัน test ICMON | `dotnet test tests/ICMON.UnitTests` (ทีละโปรเจกต์) |
| สร้าง migration ใหม่ | `dotnet ef migrations add "ชื่อ" --project src/ICMON.Infrastructure --startup-project src/ICMON.Api` |
| ติดตั้ง template | `dotnet new install .` (จาก root) |
| แพ็ก template เป็น nuget | `dotnet pack` (จาก root) |

---

## 6. ปัญหาที่พบบ่อย (Troubleshooting)

| อาการ | สาเหตุ / วิธีแก้ |
|-------|------------------|
| `connection refused` ตอนรัน | ยังไม่ได้ `docker compose up -d` หรือ Docker ยังไม่พร้อม |
| ไม่สามารถเชื่อมต่อ PostgreSQL `localhost:5432` | มี container/postgres ตัวอื่นครอบ port 5432 อยู่ — หยุดหรือเปลี่ยน `DB_PORT` ใน `ICMON/.env` |
| Redis connect error ครั้งแรกที่เรียก cache | Redis ต้องรันอยู่ (`docker compose up -d`) หรือแก้ `ConnectionStrings:Redis` ใน `appsettings.json` |
| `EnsureCreatedAsync` ไม่สร้าง DB | ระวัง: `EnsureCreatedAsync` ไม่เคยอัปเดต schema ของ DB ที่มีอยู่แล้ว — ถ้าเปลี่ยน model ต้องลบ DB หรือใช้ migration แทน |
| ติดตั้ง template แล้วหาคำสั่ง `ca-and-ddd` ไม่เจอ | รัน `dotnet new install .` จาก **root** ของ repo (มีไฟล์ `nuget-package.csproj`) และตรวจด้วย `dotnet new list` |
| สร้างโปรเจกต์จาก template แล้ว build ไม่ผ่าน | ต้องใช้ **.NET 10 SDK** ตาม `content/global.json` |
| รัน `ICMON.slnx` ไม่ได้ | ต้องใช้ .NET SDK 9.0.200+ ขึ้นไป หรือรันทีละ `.csproj` แทน |
| Token หมดอายุ (401) | เรียก `POST /api/auth/refresh` เพื่อขอ token ใหม่ หรือ login ใหม่ |
| Rate limit 429 บน `/api/auth/login` | ระบบจำกัด 5 ครั้ง/5 นาที ของ `.env` การตั้งค่าใน `Program.cs` (`RateLimiter`) |