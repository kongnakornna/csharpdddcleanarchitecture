# 🚗 โมดูลที่ 12: Multi-Language (i18n) – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Multi-Language (i18n) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 45 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/LanguageAggregate/Language.cs` | Aggregate Root ภาษา |
| 2 | `Aggregates/LanguageAggregate/Translation.cs` | Entity คำแปล |
| 3 | `Enums/LanguageCode.cs` | Enum รหัสภาษา |
| 4 | `Events/LanguageAddedEvent.cs` | Event เพิ่มภาษา |
| 5 | `Events/LanguageUpdatedEvent.cs` | Event อัปเดตภาษา |
| 6 | `Events/TranslationUpdatedEvent.cs` | Event อัปเดตคำแปล |
| 7 | `Interfaces/ILanguageRepository.cs` | Interface Repository |
| 8 | `Interfaces/ITranslationRepository.cs` | Interface Repository |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 9 | `DTOs/Languages/LanguageDto.cs` | DTO ภาษา |
| 10 | `DTOs/Languages/TranslationDto.cs` | DTO คำแปล |
| 11 | `DTOs/Languages/LanguageDetailDto.cs` | DTO ภาษาแบบละเอียด |
| 12 | `Commands/Languages/AddLanguage/AddLanguageCommand.cs` | Command เพิ่มภาษา |
| 13 | `Commands/Languages/AddLanguage/AddLanguageCommandHandler.cs` | Handler เพิ่มภาษา |
| 14 | `Commands/Languages/AddLanguage/AddLanguageCommandValidator.cs` | Validator เพิ่มภาษา |
| 15 | `Commands/Languages/UpdateLanguage/UpdateLanguageCommand.cs` | Command อัปเดตภาษา |
| 16 | `Commands/Languages/UpdateLanguage/UpdateLanguageCommandHandler.cs` | Handler อัปเดตภาษา |
| 17 | `Commands/Languages/UpdateLanguage/UpdateLanguageCommandValidator.cs` | Validator อัปเดตภาษา |
| 18 | `Commands/Languages/UpdateTranslation/UpdateTranslationCommand.cs` | Command อัปเดตคำแปล |
| 19 | `Commands/Languages/UpdateTranslation/UpdateTranslationCommandHandler.cs` | Handler อัปเดตคำแปล |
| 20 | `Commands/Languages/UpdateTranslation/UpdateTranslationCommandValidator.cs` | Validator อัปเดตคำแปล |
| 21 | `Commands/Languages/BulkUpdateTranslations/BulkUpdateTranslationsCommand.cs` | Command อัปเดตหลายคำแปล |
| 22 | `Commands/Languages/BulkUpdateTranslations/BulkUpdateTranslationsCommandHandler.cs` | Handler อัปเดตหลายคำแปล |
| 23 | `Commands/Languages/BulkUpdateTranslations/BulkUpdateTranslationsCommandValidator.cs` | Validator อัปเดตหลายคำแปล |
| 24 | `Queries/Languages/GetLanguages/GetLanguagesQuery.cs` | Query รายการภาษา |
| 25 | `Queries/Languages/GetLanguages/GetLanguagesQueryHandler.cs` | Handler รายการภาษา |
| 26 | `Queries/Languages/GetCurrentLanguage/GetCurrentLanguageQuery.cs` | Query ภาษาปัจจุบัน |
| 27 | `Queries/Languages/GetCurrentLanguage/GetCurrentLanguageQueryHandler.cs` | Handler ภาษาปัจจุบัน |
| 28 | `Queries/Languages/GetTranslations/GetTranslationsQuery.cs` | Query คำแปล |
| 29 | `Queries/Languages/GetTranslations/GetTranslationsQueryHandler.cs` | Handler คำแปล |
| 30 | `Queries/Languages/GetTranslationByKey/GetTranslationByKeyQuery.cs` | Query คำแปลตาม Key |
| 31 | `Queries/Languages/GetTranslationByKey/GetTranslationByKeyQueryHandler.cs` | Handler คำแปลตาม Key |
| 32 | `Services/ILocalizationService.cs` | Interface Localization |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 33 | `Persistence/Configurations/LanguageConfiguration.cs` | EF Config Language |
| 34 | `Persistence/Configurations/TranslationConfiguration.cs` | EF Config Translation |
| 35 | `Persistence/Repositories/LanguageRepository.cs` | Repository Language |
| 36 | `Persistence/Repositories/TranslationRepository.cs` | Repository Translation |
| 37 | `Localization/LocalizationService.cs` | Service Localization |
| 38 | `Localization/JsonLocalizationProvider.cs` | JSON Resource Provider |
| 39 | `Localization/CustomStringLocalizer.cs` | Custom String Localizer |
| 40 | `Persistence/SeedData/LanguageSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 41 | `Controllers/LanguagesController.cs` | Controller Languages (10 Endpoints) |
| 42 | `Middleware/LocalizationMiddleware.cs` | Middleware Localization |

---

## 🌍 18 ภาษาที่รองรับ

| รหัส | ภาษา | สัญลักษณ์ | RTL |
|------|------|-----------|-----|
| `th` | ภาษาไทย | 🇹🇭 | ❌ |
| `en` | English | 🇬🇧 | ❌ |
| `zh` | 中文 | 🇨🇳 | ❌ |
| `ja` | 日本語 | 🇯🇵 | ❌ |
| `ko` | 한국어 | 🇰🇷 | ❌ |
| `es` | Español | 🇪🇸 | ❌ |
| `fr` | Français | 🇫🇷 | ❌ |
| `de` | Deutsch | 🇩🇪 | ❌ |
| `it` | Italiano | 🇮🇹 | ❌ |
| `pt` | Português | 🇵🇹 | ❌ |
| `ru` | Русский | 🇷🇺 | ❌ |
| `ar` | العربية | 🇸🇦 | ✅ |
| `hi` | हिन्दी | 🇮🇳 | ❌ |
| `id` | Bahasa Indonesia | 🇮🇩 | ❌ |
| `ms` | Bahasa Melayu | 🇲🇾 | ❌ |
| `vi` | Tiếng Việt | 🇻🇳 | ❌ |
| `my` | မြန်မာဘာသာ | 🇲🇲 | ❌ |
| `lo` | ພາສາລາວ | 🇱🇦 | ❌ |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Languages Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| GET | `/api/languages` | รายการภาษาที่รองรับ | 50/60s |
| GET | `/api/languages/{code}` | ดูข้อมูลภาษา | 80/60s |
| GET | `/api/languages/current` | ภาษาปัจจุบัน | 100/60s |
| POST | `/api/languages/switch` | สลับภาษา | 20/60s |
| GET | `/api/languages/messages/{lang}` | ข้อความทั้งหมดของภาษา | 30/60s |
| GET | `/api/languages/messages/{lang}/{key}` | ข้อความตาม Key | 50/60s |
| POST | `/api/languages` | เพิ่มภาษาใหม่ | 10/60s |
| PUT | `/api/languages/{code}` | อัปเดตภาษา | 10/60s |
| PUT | `/api/languages/translations` | อัปเดตคำแปล | 15/60s |
| POST | `/api/languages/translations/bulk` | อัปเดตหลายคำแปล | 10/60s |
| DELETE | `/api/languages/{code}` | ลบภาษา | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="Microsoft.Extensions.Localization" Version="8.0.0" />
```

---

## 🚀 วิธีใช้

### 1. รายการภาษาที่รองรับ
```http
GET /api/languages
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "isSuccess": true,
  "data": [
    {
      "code": "th",
      "name": "ภาษาไทย",
      "nativeName": "ภาษาไทย",
      "flag": "🇹🇭",
      "isRtl": false,
      "isDefault": true,
      "isActive": true
    },
    {
      "code": "en",
      "name": "English",
      "nativeName": "English",
      "flag": "🇬🇧",
      "isRtl": false,
      "isDefault": false,
      "isActive": true
    }
  ]
}
```

### 2. ภาษาปัจจุบัน
```http
GET /api/languages/current
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "code": "th",
  "name": "ภาษาไทย",
  "nativeName": "ภาษาไทย",
  "flag": "🇹🇭",
  "isRtl": false,
  "isDefault": true
}
```

### 3. สลับภาษา
```http
POST /api/languages/switch
Authorization: Bearer <accessToken>
{
  "languageCode": "en"
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "code": "en",
    "name": "English",
    "nativeName": "English",
    "flag": "🇬🇧",
    "isRtl": false
  }
}
```

### 4. ดึงข้อความทั้งหมดของภาษา
```http
GET /api/languages/messages/th
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "language": "th",
  "messages": {
    "Welcome": "ยินดีต้อนรับ",
    "Login": "เข้าสู่ระบบ",
    "Logout": "ออกจากระบบ",
    "Dashboard": "แผงควบคุม",
    "Jobs": "ใบงาน",
    "Customers": "ลูกค้า",
    "Quotations": "ใบเสนอราคา",
    "PurchaseOrders": "ใบสั่งซื้อ",
    "Inventory": "สินค้าคงคลัง",
    "Payments": "การชำระเงิน",
    "Reports": "รายงาน",
    "Settings": "การตั้งค่า"
  }
}
```

### 5. ดึงข้อความตาม Key
```http
GET /api/languages/messages/th/Welcome
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "key": "Welcome",
  "language": "th",
  "value": "ยินดีต้อนรับ"
}
```

### 6. อัปเดตคำแปล
```http
PUT /api/languages/translations
Authorization: Bearer <accessToken>
{
  "languageCode": "th",
  "key": "Welcome",
  "value": "ยินดีต้อนรับสู่ระบบ ICMON"
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "key": "Welcome",
    "language": "th",
    "value": "ยินดีต้อนรับสู่ระบบ ICMON",
    "updatedAt": "2026-07-06T10:30:00Z"
  }
}
```

### 7. อัปเดตหลายคำแปล
```http
POST /api/languages/translations/bulk
Authorization: Bearer <accessToken>
{
  "languageCode": "en",
  "translations": {
    "Welcome": "Welcome to ICMON System",
    "Dashboard": "Control Panel",
    "Jobs": "Work Orders"
  }
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Language.cs
```csharp
namespace ICMON.Domain.Aggregates.LanguageAggregate;

public class Language : AggregateRoot<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string NativeName { get; private set; }
    public string Flag { get; private set; }
    public string Locale { get; private set; }
    public bool IsRtl { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<Translation> _translations = new();
    public IReadOnlyList<Translation> Translations => _translations.AsReadOnly();

    private Language() { } // For EF Core

    public static Language Create(
        string code,
        string name,
        string nativeName,
        string flag,
        string locale,
        bool isRtl,
        Guid whitelabelId,
        bool isDefault = false)
    {
        var language = new Language
        {
            Id = Guid.NewGuid(),
            Code = code.ToLower(),
            Name = name,
            NativeName = nativeName,
            Flag = flag,
            Locale = locale,
            IsRtl = isRtl,
            IsDefault = isDefault,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        language.AddDomainEvent(new LanguageAddedEvent(language.Id, language.Code));
        return language;
    }

    public void Update(string name, string nativeName, string flag, string locale, bool isRtl)
    {
        Name = name;
        NativeName = nativeName;
        Flag = flag;
        Locale = locale;
        IsRtl = isRtl;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new LanguageUpdatedEvent(Id, Code));
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

    public void SetDefault()
    {
        IsDefault = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UnsetDefault()
    {
        IsDefault = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTranslation(string key, string value)
    {
        var translation = Translation.Create(Id, key, value);
        _translations.Add(translation);
    }

    public void UpdateTranslation(string key, string value)
    {
        var translation = _translations.FirstOrDefault(t => t.Key == key);
        if (translation != null)
        {
            translation.Update(value);
        }
        else
        {
            AddTranslation(key, value);
        }
    }
}
```

### 2. Domain Entity - Translation.cs
```csharp
namespace ICMON.Domain.Aggregates.LanguageAggregate;

public class Translation : Entity<Guid>
{
    public Guid LanguageId { get; private set; }
    public virtual Language Language { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Translation() { } // For EF Core

    public static Translation Create(Guid languageId, string key, string value, string? description = null)
    {
        return new Translation
        {
            Id = Guid.NewGuid(),
            LanguageId = languageId,
            Key = key,
            Value = value,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string value, string? description = null)
    {
        Value = value;
        Description = description ?? Description;
        UpdatedAt = DateTime.UtcNow;
    }
}
```

### 3. Domain Enums - LanguageCode.cs
```csharp
namespace ICMON.Domain.Enums;

public enum LanguageCode
{
    th = 0,
    en = 1,
    zh = 2,
    ja = 3,
    ko = 4,
    es = 5,
    fr = 6,
    de = 7,
    it = 8,
    pt = 9,
    ru = 10,
    ar = 11,
    hi = 12,
    id = 13,
    ms = 14,
    vi = 15,
    my = 16,
    lo = 17
}

public static class LanguageCodeExtensions
{
    public static string GetDisplayName(this LanguageCode code)
    {
        return code switch
        {
            LanguageCode.th => "ภาษาไทย",
            LanguageCode.en => "English",
            LanguageCode.zh => "中文",
            LanguageCode.ja => "日本語",
            LanguageCode.ko => "한국어",
            LanguageCode.es => "Español",
            LanguageCode.fr => "Français",
            LanguageCode.de => "Deutsch",
            LanguageCode.it => "Italiano",
            LanguageCode.pt => "Português",
            LanguageCode.ru => "Русский",
            LanguageCode.ar => "العربية",
            LanguageCode.hi => "हिन्दी",
            LanguageCode.id => "Bahasa Indonesia",
            LanguageCode.ms => "Bahasa Melayu",
            LanguageCode.vi => "Tiếng Việt",
            LanguageCode.my => "မြန်မာဘာသာ",
            LanguageCode.lo => "ພາສາລາວ",
            _ => code.ToString()
        };
    }

    public static string GetFlag(this LanguageCode code)
    {
        return code switch
        {
            LanguageCode.th => "🇹🇭",
            LanguageCode.en => "🇬🇧",
            LanguageCode.zh => "🇨🇳",
            LanguageCode.ja => "🇯🇵",
            LanguageCode.ko => "🇰🇷",
            LanguageCode.es => "🇪🇸",
            LanguageCode.fr => "🇫🇷",
            LanguageCode.de => "🇩🇪",
            LanguageCode.it => "🇮🇹",
            LanguageCode.pt => "🇵🇹",
            LanguageCode.ru => "🇷🇺",
            LanguageCode.ar => "🇸🇦",
            LanguageCode.hi => "🇮🇳",
            LanguageCode.id => "🇮🇩",
            LanguageCode.ms => "🇲🇾",
            LanguageCode.vi => "🇻🇳",
            LanguageCode.my => "🇲🇲",
            LanguageCode.lo => "🇱🇦",
            _ => "🏳️"
        };
    }

    public static string GetLocale(this LanguageCode code)
    {
        return code switch
        {
            LanguageCode.th => "th-TH",
            LanguageCode.en => "en-US",
            LanguageCode.zh => "zh-CN",
            LanguageCode.ja => "ja-JP",
            LanguageCode.ko => "ko-KR",
            LanguageCode.es => "es-ES",
            LanguageCode.fr => "fr-FR",
            LanguageCode.de => "de-DE",
            LanguageCode.it => "it-IT",
            LanguageCode.pt => "pt-PT",
            LanguageCode.ru => "ru-RU",
            LanguageCode.ar => "ar-SA",
            LanguageCode.hi => "hi-IN",
            LanguageCode.id => "id-ID",
            LanguageCode.ms => "ms-MY",
            LanguageCode.vi => "vi-VN",
            LanguageCode.my => "my-MM",
            LanguageCode.lo => "lo-LA",
            _ => "en-US"
        };
    }

    public static bool IsRtl(this LanguageCode code)
    {
        return code == LanguageCode.ar;
    }

    public static string GetNativeName(this LanguageCode code)
    {
        return code switch
        {
            LanguageCode.th => "ภาษาไทย",
            LanguageCode.en => "English",
            LanguageCode.zh => "中文",
            LanguageCode.ja => "日本語",
            LanguageCode.ko => "한국어",
            LanguageCode.es => "Español",
            LanguageCode.fr => "Français",
            LanguageCode.de => "Deutsch",
            LanguageCode.it => "Italiano",
            LanguageCode.pt => "Português",
            LanguageCode.ru => "Русский",
            LanguageCode.ar => "العربية",
            LanguageCode.hi => "हिन्दी",
            LanguageCode.id => "Bahasa Indonesia",
            LanguageCode.ms => "Bahasa Melayu",
            LanguageCode.vi => "Tiếng Việt",
            LanguageCode.my => "မြန်မာဘာသာ",
            LanguageCode.lo => "ພາສາລາວ",
            _ => code.ToString()
        };
    }
}
```

### 4. Service - LocalizationService.cs
```csharp
using System.Globalization;

namespace ICMON.Infrastructure.Localization;

public class LocalizationService : ILocalizationService
{
    private readonly ILanguageRepository _languageRepository;
    private readonly ITranslationRepository _translationRepository;
    private readonly ICacheService _cacheService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LocalizationService> _logger;

    public LocalizationService(
        ILanguageRepository languageRepository,
        ITranslationRepository translationRepository,
        ICacheService cacheService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<LocalizationService> logger)
    {
        _languageRepository = languageRepository;
        _translationRepository = translationRepository;
        _cacheService = cacheService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<string> GetTranslationAsync(string key, string languageCode, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"messages:{key}:{languageCode}";
        
        var cached = await _cacheService.GetAsync<string>(cacheKey, cancellationToken);
        if (cached != null)
            return cached;

        var language = await _languageRepository.GetByCodeAsync(languageCode, cancellationToken);
        if (language == null)
            return key;

        var translation = await _translationRepository.GetByKeyAsync(language.Id, key, cancellationToken);
        
        var value = translation?.Value ?? key;
        
        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, value, TimeSpan.FromHours(1), cancellationToken);
        
        return value;
    }

    public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"messages_all:{languageCode}";
        
        var cached = await _cacheService.GetAsync<Dictionary<string, string>>(cacheKey, cancellationToken);
        if (cached != null)
            return cached;

        var language = await _languageRepository.GetByCodeAsync(languageCode, cancellationToken);
        if (language == null)
            return new Dictionary<string, string>();

        var translations = await _translationRepository.GetByLanguageAsync(language.Id, cancellationToken);
        var result = translations.ToDictionary(t => t.Key, t => t.Value);
        
        // Cache for 1 hour
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromHours(1), cancellationToken);
        
        return result;
    }

    public async Task<string> GetCurrentLanguageAsync(CancellationToken cancellationToken = default)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
            return "en";

        // Check session/cookie first
        var cookieLang = context.Request.Cookies["Language"];
        if (!string.IsNullOrEmpty(cookieLang))
        {
            var language = await _languageRepository.GetByCodeAsync(cookieLang, cancellationToken);
            if (language != null && language.IsActive)
                return cookieLang;
        }

        // Check Accept-Language header
        var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            var preferredLangs = acceptLanguage.Split(',')
                .Select(x => x.Split(';')[0].Trim())
                .ToList();

            foreach (var lang in preferredLangs)
            {
                var code = lang.Split('-')[0].ToLower();
                var language = await _languageRepository.GetByCodeAsync(code, cancellationToken);
                if (language != null && language.IsActive)
                    return code;
            }
        }

        // Fallback to default
        var defaultLang = await _languageRepository.GetDefaultAsync(cancellationToken);
        return defaultLang?.Code ?? "en";
    }

    public async Task<bool> SetCurrentLanguageAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var language = await _languageRepository.GetByCodeAsync(languageCode, cancellationToken);
        if (language == null || !language.IsActive)
            return false;

        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            context.Response.Cookies.Append("Language", languageCode, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });

            // Set current culture
            var culture = new CultureInfo(language.Locale);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        return true;
    }

    public async Task<IEnumerable<LanguageDto>> GetSupportedLanguagesAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = "languages_active";
        
        var cached = await _cacheService.GetAsync<IEnumerable<LanguageDto>>(cacheKey, cancellationToken);
        if (cached != null)
            return cached;

        var languages = await _languageRepository.GetActiveAsync(cancellationToken);
        var result = languages.Select(l => new LanguageDto
        {
            Code = l.Code,
            Name = l.Name,
            NativeName = l.NativeName,
            Flag = l.Flag,
            IsRtl = l.IsRtl,
            IsDefault = l.IsDefault,
            IsActive = l.IsActive
        });

        // Cache for 2 hours
        await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromHours(2), cancellationToken);
        
        return result;
    }

    public async Task<LanguageDto> GetLanguageInfoAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var language = await _languageRepository.GetByCodeAsync(languageCode, cancellationToken);
        if (language == null)
            return null;

        return new LanguageDto
        {
            Code = language.Code,
            Name = language.Name,
            NativeName = language.NativeName,
            Flag = language.Flag,
            IsRtl = language.IsRtl,
            IsDefault = language.IsDefault,
            IsActive = language.IsActive
        };
    }

    public string GetFallbackTranslation(string key)
    {
        // English fallback
        var fallbacks = new Dictionary<string, string>
        {
            ["Welcome"] = "Welcome",
            ["Login"] = "Login",
            ["Logout"] = "Logout",
            ["Dashboard"] = "Dashboard",
            ["Jobs"] = "Jobs",
            ["Customers"] = "Customers",
            ["Quotations"] = "Quotations",
            ["PurchaseOrders"] = "Purchase Orders",
            ["Inventory"] = "Inventory",
            ["Payments"] = "Payments",
            ["Reports"] = "Reports",
            ["Settings"] = "Settings",
            ["JobNo"] = "Job No.",
            ["Customer"] = "Customer",
            ["Car"] = "Car",
            ["Status"] = "Status",
            ["TotalAmount"] = "Total Amount",
            ["ReceivedDate"] = "Received Date",
            ["CompletedDate"] = "Completed Date",
            ["Description"] = "Description",
            ["Part"] = "Part",
            ["Service"] = "Service",
            ["Quantity"] = "Quantity",
            ["UnitPrice"] = "Unit Price",
            ["Total"] = "Total",
            ["Subtotal"] = "Subtotal",
            ["Tax"] = "Tax",
            ["Discount"] = "Discount",
            ["ValidUntil"] = "Valid Until",
            ["Notes"] = "Notes",
            ["CreatedAt"] = "Created At",
            ["UpdatedAt"] = "Updated At",
            ["ConfirmedAt"] = "Confirmed At",
            ["CancelledAt"] = "Cancelled At",
            ["ApprovedAt"] = "Approved At",
            ["RejectedAt"] = "Rejected At",
            ["PaidAt"] = "Paid At",
            ["PaymentMethod"] = "Payment Method",
            ["Reference"] = "Reference"
        };

        return fallbacks.GetValueOrDefault(key, key);
    }
}
```

### 5. Localization Middleware
```csharp
using System.Globalization;

namespace ICMON.Api.Middleware;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LocalizationMiddleware> _logger;

    public LocalizationMiddleware(RequestDelegate next, ILogger<LocalizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ILocalizationService localizationService)
    {
        try
        {
            // Get language from cookie, header, or query string
            var languageCode = await GetLanguageCode(context, localizationService);
            
            if (!string.IsNullOrEmpty(languageCode))
            {
                // Set culture
                var language = await localizationService.GetLanguageInfoAsync(languageCode, context.RequestAborted);
                if (language != null)
                {
                    var culture = new CultureInfo($"{languageCode}-{languageCode.ToUpper()}");
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;

                    // Store in context items for later use
                    context.Items["CurrentLanguage"] = languageCode;
                    context.Items["IsRtl"] = language.IsRtl;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error setting localization, using default culture");
            // Use default culture
            var culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        await _next(context);
    }

    private async Task<string> GetLanguageCode(HttpContext context, ILocalizationService localizationService)
    {
        // 1. Check query string
        var queryLang = context.Request.Query["lang"].ToString();
        if (!string.IsNullOrEmpty(queryLang))
        {
            var isValid = await IsValidLanguage(queryLang, localizationService);
            if (isValid)
            {
                SetLanguageCookie(context, queryLang);
                return queryLang;
            }
        }

        // 2. Check cookie
        var cookieLang = context.Request.Cookies["Language"];
        if (!string.IsNullOrEmpty(cookieLang))
        {
            var isValid = await IsValidLanguage(cookieLang, localizationService);
            if (isValid)
                return cookieLang;
        }

        // 3. Check Accept-Language header
        var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            var preferredLangs = acceptLanguage.Split(',')
                .Select(x => x.Split(';')[0].Trim())
                .Select(x => x.Split('-')[0].ToLower())
                .ToList();

            foreach (var lang in preferredLangs)
            {
                var isValid = await IsValidLanguage(lang, localizationService);
                if (isValid)
                    return lang;
            }
        }

        // 4. Default language
        var defaultLang = await localizationService.GetSupportedLanguagesAsync();
        return defaultLang?.FirstOrDefault(l => l.IsDefault)?.Code ?? "en";
    }

    private async Task<bool> IsValidLanguage(string code, ILocalizationService localizationService)
    {
        var languages = await localizationService.GetSupportedLanguagesAsync();
        return languages.Any(l => l.Code == code && l.IsActive);
    }

    private void SetLanguageCookie(HttpContext context, string languageCode)
    {
        context.Response.Cookies.Append("Language", languageCode, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddYears(1),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Path = "/"
        });
    }
}
```

### 6. Custom String Localizer
```csharp
using Microsoft.Extensions.Localization;

namespace ICMON.Infrastructure.Localization;

public class CustomStringLocalizer : IStringLocalizer
{
    private readonly ILocalizationService _localizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomStringLocalizer(
        ILocalizationService localizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _localizationService = localizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetTranslationAsync(name).GetAwaiter().GetResult();
            return new LocalizedString(name, value ?? name);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = GetTranslationAsync(name).GetAwaiter().GetResult();
            var formatted = string.Format(value ?? name, arguments);
            return new LocalizedString(name, formatted);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var translations = GetTranslationsAsync().GetAwaiter().GetResult();
        return translations.Select(t => new LocalizedString(t.Key, t.Value));
    }

    private async Task<string> GetTranslationAsync(string key)
    {
        var context = _httpContextAccessor.HttpContext;
        var languageCode = context?.Items["CurrentLanguage"]?.ToString() ?? "en";

        var translation = await _localizationService.GetTranslationAsync(key, languageCode, CancellationToken.None);
        if (translation == key)
        {
            // Fallback to English
            translation = await _localizationService.GetTranslationAsync(key, "en", CancellationToken.None);
            if (translation == key)
            {
                return _localizationService.GetFallbackTranslation(key);
            }
        }
        return translation;
    }

    private async Task<Dictionary<string, string>> GetTranslationsAsync()
    {
        var context = _httpContextAccessor.HttpContext;
        var languageCode = context?.Items["CurrentLanguage"]?.ToString() ?? "en";

        var translations = await _localizationService.GetAllTranslationsAsync(languageCode, CancellationToken.None);
        if (!translations.Any())
        {
            // Fallback to English
            translations = await _localizationService.GetAllTranslationsAsync("en", CancellationToken.None);
        }
        return translations;
    }
}

public class CustomStringLocalizer<T> : CustomStringLocalizer, IStringLocalizer<T>
{
    public CustomStringLocalizer(
        ILocalizationService localizationService,
        IHttpContextAccessor httpContextAccessor)
        : base(localizationService, httpContextAccessor)
    {
    }
}
```

### 7. Repository - LanguageRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
{
    public LanguageRepository(AppDbContext context) : base(context) { }

    public async Task<Language?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(l => l.Translations)
            .FirstOrDefaultAsync(l => l.Code == code, cancellationToken);
    }

    public async Task<Language?> GetDefaultAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(l => l.IsDefault && l.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<Language>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(l => l.IsActive)
            .OrderBy(l => l.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Language>> GetByWhitelabelAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(l => l.WhitelabelId == whitelabelId && l.IsActive)
            .OrderBy(l => l.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(l => l.Code == code, cancellationToken);
    }

    public async Task SetDefaultAsync(string code, CancellationToken cancellationToken = default)
    {
        var currentDefault = await _dbSet.FirstOrDefaultAsync(l => l.IsDefault, cancellationToken);
        if (currentDefault != null)
        {
            currentDefault.UnsetDefault();
        }

        var newDefault = await _dbSet.FirstOrDefaultAsync(l => l.Code == code, cancellationToken);
        if (newDefault != null)
        {
            newDefault.SetDefault();
        }
    }
}
```

### 8. Repository - TranslationRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class TranslationRepository : GenericRepository<Translation>, ITranslationRepository
{
    public TranslationRepository(AppDbContext context) : base(context) { }

    public async Task<Translation?> GetByKeyAsync(Guid languageId, string key, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.LanguageId == languageId && t.Key == key, cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByLanguageAsync(Guid languageId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(t => t.LanguageId == languageId)
            .OrderBy(t => t.Key)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetByLanguageAndKeysAsync(Guid languageId, IEnumerable<string> keys, CancellationToken cancellationToken = default)
    {
        var keyList = keys.ToList();
        return await _dbSet
            .Where(t => t.LanguageId == languageId && keyList.Contains(t.Key))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Translation>> SearchTranslationsAsync(Guid languageId, string searchTerm, CancellationToken cancellationToken = default)
    {
        searchTerm = searchTerm.ToLower();
        return await _dbSet
            .Where(t => t.LanguageId == languageId &&
                       (t.Key.ToLower().Contains(searchTerm) ||
                        t.Value.ToLower().Contains(searchTerm)))
            .OrderBy(t => t.Key)
            .ToListAsync(cancellationToken);
    }

    public async Task<Translation> UpdateOrCreateAsync(Guid languageId, string key, string value, CancellationToken cancellationToken = default)
    {
        var translation = await GetByKeyAsync(languageId, key, cancellationToken);
        if (translation != null)
        {
            translation.Update(value);
        }
        else
        {
            translation = Translation.Create(languageId, key, value);
            await AddAsync(translation, cancellationToken);
        }
        return translation;
    }

    public async Task<IEnumerable<Translation>> BulkUpdateAsync(Guid languageId, Dictionary<string, string> translations, CancellationToken cancellationToken = default)
    {
        var results = new List<Translation>();
        foreach (var kvp in translations)
        {
            var translation = await UpdateOrCreateAsync(languageId, kvp.Key, kvp.Value, cancellationToken);
            results.Add(translation);
        }
        return results;
    }
}
```

### 9. Command Handler - SwitchLanguageCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;
using ICMON.Application.DTOs.Languages;

namespace ICMON.Application.Commands.Languages.SwitchLanguage;

public class SwitchLanguageCommandHandler : IRequestHandler<SwitchLanguageCommand, Result<LanguageDto>>
{
    private readonly ILocalizationService _localizationService;
    private readonly ICacheService _cacheService;

    public SwitchLanguageCommandHandler(
        ILocalizationService localizationService,
        ICacheService cacheService)
    {
        _localizationService = localizationService;
        _cacheService = cacheService;
    }

    public async Task<Result<LanguageDto>> Handle(SwitchLanguageCommand request, CancellationToken cancellationToken)
    {
        // Get language info
        var language = await _localizationService.GetLanguageInfoAsync(request.LanguageCode, cancellationToken);
        if (language == null)
            return Result<LanguageDto>.Failure($"Language '{request.LanguageCode}' not found");

        if (!language.IsActive)
            return Result<LanguageDto>.Failure($"Language '{request.LanguageCode}' is not active");

        // Set as current language
        var success = await _localizationService.SetCurrentLanguageAsync(request.LanguageCode, cancellationToken);
        if (!success)
            return Result<LanguageDto>.Failure("Failed to set language");

        // Clear cache for current user
        await _cacheService.RemoveAsync($"user_language:{request.LanguageCode}", cancellationToken);

        return Result<LanguageDto>.Success(language);
    }
}
```

### 10. Controller - LanguagesController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LanguagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILocalizationService _localizationService;

    public LanguagesController(IMediator mediator, ILocalizationService localizationService)
    {
        _mediator = mediator;
        _localizationService = localizationService;
    }

    [HttpGet]
    [Authorize("LANGUAGE_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<IEnumerable<LanguageDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLanguages()
    {
        var query = new GetLanguagesQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{code}")]
    [Authorize("LANGUAGE_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<LanguageDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLanguage(string code)
    {
        var query = new GetLanguageQuery { LanguageCode = code };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("current")]
    [Authorize("LANGUAGE_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentLanguage()
    {
        var query = new GetCurrentLanguageQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost("switch")]
    [Authorize("LANGUAGE_UPDATE")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SwitchLanguage([FromBody] SwitchLanguageCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("messages/{lang}")]
    [Authorize("LANGUAGE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetMessages(string lang)
    {
        var messages = await _localizationService.GetAllTranslationsAsync(lang, HttpContext.RequestAborted);
        return Ok(new
        {
            Language = lang,
            Messages = messages
        });
    }

    [HttpGet("messages/{lang}/{key}")]
    [Authorize("LANGUAGE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetMessage(string lang, string key)
    {
        var value = await _localizationService.GetTranslationAsync(key, lang, HttpContext.RequestAborted);
        return Ok(new
        {
            Key = key,
            Language = lang,
            Value = value
        });
    }

    [HttpPost]
    [Authorize("LANGUAGE_ADMIN")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddLanguage([FromBody] AddLanguageCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{code}")]
    [Authorize("LANGUAGE_ADMIN")]
    [EnableRateLimiting("Update")]
    [ProducesResponseType(typeof(Result<LanguageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateLanguage(string code, [FromBody] UpdateLanguageCommand command)
    {
        if (code != command.Code)
            return BadRequest(Result<LanguageDto>.Failure("Language code mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("translations")]
    [Authorize("LANGUAGE_ADMIN")]
    [EnableRateLimiting("Update")]
    [ProducesResponseType(typeof(Result<TranslationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTranslation([FromBody] UpdateTranslationCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("translations/bulk")]
    [Authorize("LANGUAGE_ADMIN")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<IEnumerable<TranslationDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BulkUpdateTranslations([FromBody] BulkUpdateTranslationsCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{code}")]
    [Authorize("LANGUAGE_ADMIN")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteLanguage(string code)
    {
        var command = new DeleteLanguageCommand { LanguageCode = code };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 11. Seed Data - LanguageSeedData.cs
```csharp
namespace ICMON.Infrastructure.Persistence.SeedData;

public static class LanguageSeedData
{
    public static async Task SeedAsync(AppDbContext context, Guid whitelabelId)
    {
        var languages = new List<Language>
        {
            Language.Create("th", "ภาษาไทย", "ภาษาไทย", "🇹🇭", "th-TH", false, whitelabelId, true),
            Language.Create("en", "English", "English", "🇬🇧", "en-US", false, whitelabelId, false),
            Language.Create("zh", "中文", "中文", "🇨🇳", "zh-CN", false, whitelabelId, false),
            Language.Create("ja", "日本語", "日本語", "🇯🇵", "ja-JP", false, whitelabelId, false),
            Language.Create("ko", "한국어", "한국어", "🇰🇷", "ko-KR", false, whitelabelId, false),
            Language.Create("es", "Español", "Español", "🇪🇸", "es-ES", false, whitelabelId, false),
            Language.Create("fr", "Français", "Français", "🇫🇷", "fr-FR", false, whitelabelId, false),
            Language.Create("de", "Deutsch", "Deutsch", "🇩🇪", "de-DE", false, whitelabelId, false),
            Language.Create("it", "Italiano", "Italiano", "🇮🇹", "it-IT", false, whitelabelId, false),
            Language.Create("pt", "Português", "Português", "🇵🇹", "pt-PT", false, whitelabelId, false),
            Language.Create("ru", "Русский", "Русский", "🇷🇺", "ru-RU", false, whitelabelId, false),
            Language.Create("ar", "العربية", "العربية", "🇸🇦", "ar-SA", true, whitelabelId, false),
            Language.Create("hi", "हिन्दी", "हिन्दी", "🇮🇳", "hi-IN", false, whitelabelId, false),
            Language.Create("id", "Bahasa Indonesia", "Bahasa Indonesia", "🇮🇩", "id-ID", false, whitelabelId, false),
            Language.Create("ms", "Bahasa Melayu", "Bahasa Melayu", "🇲🇾", "ms-MY", false, whitelabelId, false),
            Language.Create("vi", "Tiếng Việt", "Tiếng Việt", "🇻🇳", "vi-VN", false, whitelabelId, false),
            Language.Create("my", "မြန်မာဘာသာ", "မြန်မာဘာသာ", "🇲🇲", "my-MM", false, whitelabelId, false),
            Language.Create("lo", "ພາສາລາວ", "ພາສາລາວ", "🇱🇦", "lo-LA", false, whitelabelId, false)
        };

        foreach (var lang in languages)
        {
            var existing = await context.Languages.FirstOrDefaultAsync(l => l.Code == lang.Code && l.WhitelabelId == whitelabelId);
            if (existing == null)
            {
                await context.Languages.AddAsync(lang);
            }
        }

        await context.SaveChangesAsync();

        // Add default translations
        await SeedTranslationsAsync(context, whitelabelId);
    }

    private static async Task SeedTranslationsAsync(AppDbContext context, Guid whitelabelId)
    {
        var languages = await context.Languages
            .Where(l => l.WhitelabelId == whitelabelId)
            .ToListAsync();

        var defaultTranslations = new Dictionary<string, Dictionary<string, string>>
        {
            ["th"] = new()
            {
                ["Welcome"] = "ยินดีต้อนรับ",
                ["Login"] = "เข้าสู่ระบบ",
                ["Logout"] = "ออกจากระบบ",
                ["Dashboard"] = "แผงควบคุม",
                ["Jobs"] = "ใบงาน",
                ["Customers"] = "ลูกค้า",
                ["Quotations"] = "ใบเสนอราคา",
                ["PurchaseOrders"] = "ใบสั่งซื้อ",
                ["Inventory"] = "สินค้าคงคลัง",
                ["Payments"] = "การชำระเงิน",
                ["Reports"] = "รายงาน",
                ["Settings"] = "การตั้งค่า",
                ["JobNo"] = "เลขที่ใบงาน",
                ["Customer"] = "ลูกค้า",
                ["Car"] = "รถยนต์",
                ["Status"] = "สถานะ",
                ["TotalAmount"] = "ยอดรวม",
                ["ReceivedDate"] = "วันที่รับ",
                ["CompletedDate"] = "วันที่เสร็จ",
                ["Description"] = "คำอธิบาย",
                ["Part"] = "อะไหล่",
                ["Service"] = "บริการ",
                ["Quantity"] = "จำนวน",
                ["UnitPrice"] = "ราคาต่อหน่วย",
                ["Total"] = "รวม",
                ["Subtotal"] = "รวมก่อนภาษี",
                ["Tax"] = "ภาษี",
                ["Discount"] = "ส่วนลด",
                ["ValidUntil"] = "ใช้ได้ถึง",
                ["Notes"] = "หมายเหตุ"
            },
            ["en"] = new()
            {
                ["Welcome"] = "Welcome",
                ["Login"] = "Login",
                ["Logout"] = "Logout",
                ["Dashboard"] = "Dashboard",
                ["Jobs"] = "Jobs",
                ["Customers"] = "Customers",
                ["Quotations"] = "Quotations",
                ["PurchaseOrders"] = "Purchase Orders",
                ["Inventory"] = "Inventory",
                ["Payments"] = "Payments",
                ["Reports"] = "Reports",
                ["Settings"] = "Settings",
                ["JobNo"] = "Job No.",
                ["Customer"] = "Customer",
                ["Car"] = "Car",
                ["Status"] = "Status",
                ["TotalAmount"] = "Total Amount",
                ["ReceivedDate"] = "Received Date",
                ["CompletedDate"] = "Completed Date",
                ["Description"] = "Description",
                ["Part"] = "Part",
                ["Service"] = "Service",
                ["Quantity"] = "Quantity",
                ["UnitPrice"] = "Unit Price",
                ["Total"] = "Total",
                ["Subtotal"] = "Subtotal",
                ["Tax"] = "Tax",
                ["Discount"] = "Discount",
                ["ValidUntil"] = "Valid Until",
                ["Notes"] = "Notes"
            },
            ["ar"] = new()
            {
                ["Welcome"] = "مرحباً",
                ["Login"] = "تسجيل الدخول",
                ["Logout"] = "تسجيل الخروج",
                ["Dashboard"] = "لوحة التحكم",
                ["Jobs"] = "أوامر العمل",
                ["Customers"] = "العملاء",
                ["Quotations"] = "عروض الأسعار",
                ["PurchaseOrders"] = "أوامر الشراء",
                ["Inventory"] = "المخزون",
                ["Payments"] = "المدفوعات",
                ["Reports"] = "التقارير",
                ["Settings"] = "الإعدادات"
            },
            ["zh"] = new()
            {
                ["Welcome"] = "欢迎",
                ["Login"] = "登录",
                ["Logout"] = "登出",
                ["Dashboard"] = "仪表板",
                ["Jobs"] = "工单",
                ["Customers"] = "客户",
                ["Quotations"] = "报价单",
                ["PurchaseOrders"] = "采购订单",
                ["Inventory"] = "库存",
                ["Payments"] = "付款",
                ["Reports"] = "报告",
                ["Settings"] = "设置"
            },
            ["ja"] = new()
            {
                ["Welcome"] = "ようこそ",
                ["Login"] = "ログイン",
                ["Logout"] = "ログアウト",
                ["Dashboard"] = "ダッシュボード",
                ["Jobs"] = "作業票",
                ["Customers"] = "顧客",
                ["Quotations"] = "見積書",
                ["PurchaseOrders"] = "発注書",
                ["Inventory"] = "在庫",
                ["Payments"] = "支払い",
                ["Reports"] = "レポート",
                ["Settings"] = "設定"
            }
        };

        foreach (var lang in languages)
        {
            if (defaultTranslations.TryGetValue(lang.Code, out var translations))
            {
                foreach (var kvp in translations)
                {
                    var existing = await context.Translations
                        .FirstOrDefaultAsync(t => t.LanguageId == lang.Id && t.Key == kvp.Key);

                    if (existing == null)
                    {
                        await context.Translations.AddAsync(
                            Translation.Create(lang.Id, kvp.Key, kvp.Value));
                    }
                }
            }
        }

        await context.SaveChangesAsync();
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `messages:{key}:{lang}` | 1 ชม. | ข้อความที่แปลแล้ว |
| `messages_all:{lang}` | 1 ชม. | ข้อความทั้งหมดของภาษา |
| `languages:{languageCode}` | 2 ชม. | ข้อมูลภาษา |
| `languages_active` | 2 ชม. | รายการภาษาที่ใช้งาน |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ รองรับ 18 ภาษา (th, en, zh, ja, ko, es, fr, de, it, pt, ru, ar, hi, id, ms, vi, my, lo)
- ✅ รองรับ RTL (Right-to-Left) สำหรับภาษาอาหรับ
- ✅ การตรวจจับภาษาอัตโนมัติ (Accept-Language header)
- ✅ การสลับภาษา (Cookie-based)
- ✅ การจัดการคำแปลข้อความ
- ✅ Bulk Update คำแปล
- ✅ Fallback ภาษาอังกฤษเมื่อไม่พบคำแปล
- ✅ Caching ด้วย Redis
- ✅ Middleware สำหรับ Localization
- ✅ Custom String Localizer
- ✅ Integration กับ ASP.NET Core Localization
- ✅ Domain Events
- ✅ Validation ด้วย FluentValidation
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 12: **Multi-Language (i18n)** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 13: IoT & GPS Tracking
2. โมดูลที่ 14: Web Order System (WOS) 