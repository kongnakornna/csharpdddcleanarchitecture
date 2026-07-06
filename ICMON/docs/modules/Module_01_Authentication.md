# Module 1: Authentication & Permission

## Overview
ระบบจัดการผู้ใช้ บทบาท สิทธิ์ การยืนยันตัวตนด้วย JWT และการตรวจสอบสิทธิ์แบบละเอียด (Permission-based Authorization)

## Database Tables

| Table | Description |
|-------|-------------|
| `m_user` | ผู้ใช้ระบบ (Username, Email, PasswordHash, Status) |
| `m_role` | บทบาท (ADMIN, MANAGER, USER) |
| `m_permission` | สิทธิ์การใช้งาน (JOB_CREATE, INVENTORY_READ, etc.) |
| `m_user_role` | เชื่อม User-Role |
| `m_role_permission` | เชื่อม Role-Permission |
| `m_user_token` | Token ที่ออกให้ (JWT, Refresh) |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/auth/login` | 5/300s |
| POST | `/api/auth/register` | 3/3600s |
| POST | `/api/auth/refresh` | 20/3600s |
| POST | `/api/auth/logout` | 10/60s |
| GET | `/api/auth/me` | 50/60s |

## Redis Cache Keys

| Key | TTL | Description |
|-----|-----|-------------|
| `user-permissions:{userId}` | 1 hr | User permissions |
| `user-sessions:{userId}` | 15 min | User session |
| `token-blacklist:{token}` | 1 day | Revoked tokens |

## Project Structure

```
ICMON.Domain/Entities/
├── User.cs
├── Role.cs
├── Permission.cs
├── UserRole.cs
├── RolePermission.cs
└── UserToken.cs

ICMON.Domain/Enums/
└── UserStatus.cs

ICMON.Domain/ValueObjects/
└── Email.cs

ICMON.Domain/Interfaces/
└── IUserRepository.cs

ICMON.Application/Commands/Auth/
├── Login/     (Command, Handler, Validator)
├── Register/  (Command, Handler, Validator)
├── Refresh/   (Command, Handler)
└── Logout/    (Command, Handler)

ICMON.Application/Queries/Auth/
└── GetCurrentUser/ (Query, Handler)

ICMON.Infrastructure/Persistence/Configurations/
├── UserConfiguration.cs
├── RoleConfiguration.cs
├── PermissionConfiguration.cs
├── UserRoleConfiguration.cs
├── RolePermissionConfiguration.cs
└── UserTokenConfiguration.cs

ICMON.Infrastructure/Authentication/
├── JwtTokenService.cs
├── PermissionService.cs
└── CurrentUserService.cs

ICMON.Api/Controllers/
└── AuthController.cs

ICMON.Api/Authorization/
├── PermissionRequirement.cs
├── PermissionHandler.cs
└── PermissionPolicyProvider.cs
```

## Domain Entities

### User
- `Id`, `Username`, `Email`, `PasswordHash`, `FirstName`, `LastName`
- `Status` (Active, Inactive, Suspended)
- `Roles` (many-to-many via UserRole)
- `WhitelabelId`

### Role
- `Id`, `Name`, `Description`
- `Permissions` (many-to-many via RolePermission)

### Permission
- `Id`, `Code` (e.g., JOB_CREATE), `Name`, `GroupName`

## Security Features

- ✅ JWT Authentication (Access + Refresh Token)
- ✅ Password hashing with BCrypt
- ✅ Role-based Authorization
- ✅ Permission-based Authorization (Policy)
- ✅ Token blacklisting on logout
- ✅ Rate limiting on auth endpoints
- ✅ Multi-language support (i18n)
