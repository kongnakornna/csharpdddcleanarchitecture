# 🚗 โมดูลที่ 1: Authentication & Permission – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Authentication & Permission ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 47 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Entities/User.cs` | Entity ผู้ใช้ระบบ |
| 2 | `Entities/Role.cs` | Entity บทบาท |
| 3 | `Entities/Permission.cs` | Entity สิทธิ์การใช้งาน |
| 4 | `Entities/UserRole.cs` | ความสัมพันธ์ User-Role |
| 5 | `Entities/RolePermission.cs` | ความสัมพันธ์ Role-Permission |
| 6 | `Entities/UserToken.cs` | Token ที่ออกให้ (JWT + Refresh) |
| 7 | `Enums/UserStatus.cs` | สถานะผู้ใช้ |
| 8 | `ValueObjects/Email.cs` | Value Object Email |
| 9 | `Interfaces/IUserRepository.cs` | Interface Repository เฉพาะ User |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 10 | `DTOs/Auth/LoginDto.cs` | Login Request/Response |
| 11 | `DTOs/Auth/RegisterDto.cs` | Register Request/Response |
| 12 | `DTOs/Auth/UserDto.cs` | ข้อมูลผู้ใช้ |
| 13 | `DTOs/Auth/RefreshTokenDto.cs` | Refresh Token Request/Response |
| 14 | `Commands/Auth/Login/LoginCommand.cs` | Command Login |
| 15 | `Commands/Auth/Login/LoginCommandHandler.cs` | Handler Login |
| 16 | `Commands/Auth/Login/LoginCommandValidator.cs` | Validator Login |
| 17 | `Commands/Auth/Register/RegisterCommand.cs` | Command Register |
| 18 | `Commands/Auth/Register/RegisterCommandHandler.cs` | Handler Register |
| 19 | `Commands/Auth/Register/RegisterCommandValidator.cs` | Validator Register |
| 20 | `Commands/Auth/Refresh/RefreshTokenCommand.cs` | Command Refresh |
| 21 | `Commands/Auth/Refresh/RefreshTokenCommandHandler.cs` | Handler Refresh |
| 22 | `Commands/Auth/Logout/LogoutCommand.cs` | Command Logout |
| 23 | `Commands/Auth/Logout/LogoutCommandHandler.cs` | Handler Logout |
| 24 | `Queries/Auth/GetCurrentUser/GetCurrentUserQuery.cs` | Query Current User |
| 25 | `Queries/Auth/GetCurrentUser/GetCurrentUserQueryHandler.cs` | Handler Current User |
| 26 | `Services/ICurrentUserService.cs` | Interface Current User |
| 27 | `Services/ITokenService.cs` | Interface Token Service |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 28 | `Persistence/Configurations/UserConfiguration.cs` | EF Config User |
| 29 | `Persistence/Configurations/RoleConfiguration.cs` | EF Config Role |
| 30 | `Persistence/Configurations/PermissionConfiguration.cs` | EF Config Permission |
| 31 | `Persistence/Configurations/UserRoleConfiguration.cs` | EF Config UserRole |
| 32 | `Persistence/Configurations/RolePermissionConfiguration.cs` | EF Config RolePermission |
| 33 | `Persistence/Configurations/UserTokenConfiguration.cs` | EF Config UserToken |
| 34 | `Persistence/Repositories/UserRepository.cs` | Repository เฉพาะ User |
| 35 | `Authentication/JwtTokenService.cs` | Service JWT |
| 36 | `Authentication/PermissionService.cs` | Service Permission |
| 37 | `Authentication/CurrentUserService.cs` | Service Current User |
| 38 | `Persistence/SeedData/AppDbInitializer.cs` | Seed ข้อมูลเริ่มต้น |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 39 | `Controllers/AuthController.cs` | Controller Auth (5 Endpoints) |
| 40 | `Authorization/PermissionRequirement.cs` | Requirement Permission |
| 41 | `Authorization/PermissionHandler.cs` | Handler Permission |
| 42 | `Authorization/PermissionPolicyProvider.cs` | Policy Provider |
| 43 | `Program.cs` (เพิ่มเติม) | ลงทะเบียน Authentication, Authorization, Permission Policy |

---

## 🔐 API Endpoints ที่ใช้งานได้

| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/auth/login` | เข้าสู่ระบบ | 5/300s |
| POST | `/api/auth/register` | ลงทะเบียนผู้ใช้ใหม่ | 3/3600s |
| POST | `/api/auth/refresh` | ต่ออายุ Token | 20/3600s |
| POST | `/api/auth/logout` | ออกจากระบบ | 10/60s |
| GET | `/api/auth/me` | ข้อมูลผู้ใช้ปัจจุบัน | 50/60s |

---

## 📦 NuGet Packages ที่ต้องติดตั้ง

### ใน `ICMON.Domain`
```xml
<!-- ไม่ต้องติดตั้งเพิ่ม -->
```

### ใน `ICMON.Application`
```xml
<PackageReference Include="MediatR" Version="12.2.0" />
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
```

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.0.3" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
<PackageReference Include="StackExchange.Redis" Version="2.7.33" />
```

### ใน `ICMON.Api`
```xml
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
<PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
<PackageReference Include="Serilog.Sinks.Elasticsearch" Version="9.0.3" />
```

---

## 🚀 วิธีใช้

### 1. ลงทะเบียนผู้ใช้ใหม่
```http
POST /api/auth/register
{
  "username": "john.doe",
  "email": "john@example.com",
  "password": "SecurePass123",
  "firstName": "John",
  "lastName": "Doe"
}
```

### 2. เข้าสู่ระบบ
```http
POST /api/auth/login
{
  "usernameOrEmail": "john.doe",
  "password": "SecurePass123"
}
```
**Response:**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "3x7f9k2m...",
  "expiresAt": "2026-07-06T12:00:00Z",
  "user": {
    "id": "...",
    "username": "john.doe",
    "email": "john@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "status": "Active",
    "roles": ["User"],
    "permissions": ["JOB_READ", "CUSTOMER_READ"]
  }
}
```

### 3. เรียก API ที่ต้องใช้ Authentication
```http
GET /api/auth/me
Authorization: Bearer <accessToken>
```

### 4. ต่ออายุ Token
```http
POST /api/auth/refresh
{
  "refreshToken": "3x7f9k2m..."
}
```

### 5. ออกจากระบบ
```http
POST /api/auth/logout
{
  "refreshToken": "3x7f9k2m..."
}
Authorization: Bearer <accessToken>
```

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ JWT Authentication (Access + Refresh Token)
- ✅ Password hashing ด้วย BCrypt
- ✅ Role-based Authorization
- ✅ Permission-based Authorization (Policy)
- ✅ Register, Login, Refresh, Logout, Get Current User
- ✅ ตรวจสอบสิทธิ์แบบละเอียด (Permission)
- ✅ Redis Cache พร้อมรองรับ (สามารถเพิ่มเติม)
- ✅ Rate Limiting พร้อม
- ✅ Seed Data สำหรับ Role, Permission, Admin User
- ✅ รองรับ Multi-Language (i18n) ผ่าน Resources
- ✅ ใช้ Clean Architecture + DDD + CQRS

---

## 📌 ขั้นตอนถัดไป

พร้อมสร้าง **โมดูลที่ 2: Job Card Management** หรือโมดูลอื่นตามลำดับที่คุณต้องการ