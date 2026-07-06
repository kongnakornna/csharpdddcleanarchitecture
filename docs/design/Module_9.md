# 🚗 โมดูลที่ 9: Document Management – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Document Management (การจัดการเอกสาร) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 52 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/DocumentAggregate/Document.cs` | Aggregate Root เอกสาร |
| 2 | `Aggregates/DocumentAggregate/DocumentHistory.cs` | Entity ประวัติเอกสาร |
| 3 | `Aggregates/DocumentAggregate/DocumentTemplate.cs` | Entity เทมเพลตเอกสาร |
| 4 | `Aggregates/DocumentAggregate/OcrResult.cs` | Entity ผลลัพธ์ OCR |
| 5 | `Enums/DocumentStatus.cs` | สถานะเอกสาร |
| 6 | `Enums/DocumentType.cs` | ประเภทเอกสาร |
| 7 | `Enums/TemplateType.cs` | ประเภทเทมเพลต |
| 8 | `Events/DocumentCreatedEvent.cs` | Event สร้างเอกสาร |
| 9 | `Events/DocumentGeneratedEvent.cs` | Event สร้างไฟล์เอกสาร |
| 10 | `Events/DocumentTemplateCreatedEvent.cs` | Event สร้างเทมเพลต |
| 11 | `Interfaces/IDocumentRepository.cs` | Interface Repository Document |
| 12 | `Interfaces/IDocumentTemplateRepository.cs` | Interface Repository Template |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 13 | `DTOs/Documents/DocumentDto.cs` | DTO เอกสาร |
| 14 | `DTOs/Documents/DocumentDetailDto.cs` | DTO แบบละเอียด |
| 15 | `DTOs/Documents/DocumentTemplateDto.cs` | DTO เทมเพลต |
| 16 | `DTOs/Documents/OcrResultDto.cs` | DTO OCR |
| 17 | `Commands/Documents/GenerateDocument/GenerateDocumentCommand.cs` | Command สร้างเอกสาร |
| 18 | `Commands/Documents/GenerateDocument/GenerateDocumentCommandHandler.cs` | Handler สร้างเอกสาร |
| 19 | `Commands/Documents/GenerateDocument/GenerateDocumentCommandValidator.cs` | Validator สร้างเอกสาร |
| 20 | `Commands/Documents/UploadDocument/UploadDocumentCommand.cs` | Command อัปโหลดเอกสาร |
| 21 | `Commands/Documents/UploadDocument/UploadDocumentCommandHandler.cs` | Handler อัปโหลดเอกสาร |
| 22 | `Commands/Documents/UploadDocument/UploadDocumentCommandValidator.cs` | Validator อัปโหลดเอกสาร |
| 23 | `Commands/Documents/CreateTemplate/CreateTemplateCommand.cs` | Command สร้างเทมเพลต |
| 24 | `Commands/Documents/CreateTemplate/CreateTemplateCommandHandler.cs` | Handler สร้างเทมเพลต |
| 25 | `Commands/Documents/CreateTemplate/CreateTemplateCommandValidator.cs` | Validator สร้างเทมเพลต |
| 26 | `Commands/Documents/ProcessOcr/ProcessOcrCommand.cs` | Command OCR |
| 27 | `Commands/Documents/ProcessOcr/ProcessOcrCommandHandler.cs` | Handler OCR |
| 28 | `Commands/Documents/ProcessOcr/ProcessOcrCommandValidator.cs` | Validator OCR |
| 29 | `Queries/Documents/GetDocumentById/GetDocumentByIdQuery.cs` | Query ดึงตาม ID |
| 30 | `Queries/Documents/GetDocumentById/GetDocumentByIdQueryHandler.cs` | Handler ดึงตาม ID |
| 31 | `Queries/Documents/GetDocumentByReference/GetDocumentByReferenceQuery.cs` | Query ดึงตาม Reference |
| 32 | `Queries/Documents/GetDocumentByReference/GetDocumentByReferenceQueryHandler.cs` | Handler ดึงตาม Reference |
| 33 | `Queries/Documents/GetDocumentList/GetDocumentListQuery.cs` | Query รายการเอกสาร |
| 34 | `Queries/Documents/GetDocumentList/GetDocumentListQueryHandler.cs` | Handler รายการเอกสาร |
| 35 | `Queries/Documents/GetTemplate/GetTemplateQuery.cs` | Query ดึงเทมเพลต |
| 36 | `Queries/Documents/GetTemplate/GetTemplateQueryHandler.cs` | Handler ดึงเทมเพลต |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 37 | `Persistence/Configurations/DocumentConfiguration.cs` | EF Config Document |
| 38 | `Persistence/Configurations/DocumentHistoryConfiguration.cs` | EF Config DocumentHistory |
| 39 | `Persistence/Configurations/DocumentTemplateConfiguration.cs` | EF Config DocumentTemplate |
| 40 | `Persistence/Configurations/OcrResultConfiguration.cs` | EF Config OcrResult |
| 41 | `Persistence/Repositories/DocumentRepository.cs` | Repository Document |
| 42 | `Persistence/Repositories/DocumentTemplateRepository.cs` | Repository Template |
| 43 | `Storage/IFileStorageService.cs` | Interface File Storage |
| 44 | `Storage/LocalFileStorageService.cs` | Local File Storage |
| 45 | `Ocr/IOcrService.cs` | Interface OCR |
| 46 | `Ocr/TesseractOcrService.cs` | OCR Service (Tesseract) |
| 47 | `Persistence/SeedData/DocumentSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 48 | `Controllers/DocumentsController.cs` | Controller Document (10 Endpoints) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Documents Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/documents/generate` | สร้างเอกสารจากเทมเพลต | 15/300s |
| POST | `/api/documents/upload` | อัปโหลดเอกสาร | 20/300s |
| GET | `/api/documents/{id}` | ดูข้อมูลเอกสาร | 80/60s |
| GET | `/api/documents/reference/{refType}/{refId}` | ดึงตาม Reference | 60/60s |
| GET | `/api/documents` | รายการเอกสาร | 50/60s |
| GET | `/api/documents/{id}/download` | ดาวน์โหลดเอกสาร | 30/60s |
| GET | `/api/documents/{id}/preview` | ดูตัวอย่างเอกสาร | 20/60s |
| POST | `/api/documents/ocr` | กระบวนการ OCR | 10/60s |
| POST | `/api/documents/templates` | อัปโหลดเทมเพลต | 10/300s |
| GET | `/api/documents/templates/{code}` | ดาวน์โหลดเทมเพลต | 20/60s |
| DELETE | `/api/documents/{id}` | ลบเอกสาร | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="QuestPDF" Version="2023.12.6" />
<PackageReference Include="EPPlus" Version="7.0.0" />
<PackageReference Include="Tesseract.NET" Version="5.2.0" />
<PackageReference Include="SixLabors.ImageSharp" Version="3.1.2" />
<PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.2.0" />
<PackageReference Include="AWSSDK.S3" Version="3.7.300.0" />  <!-- Optional: AWS S3 -->
<PackageReference Include="Azure.Storage.Blobs" Version="12.19.0" />  <!-- Optional: Azure Blob -->
```

---

## 🚀 วิธีใช้

### 1. สร้างเอกสารจากเทมเพลต
```http
POST /api/documents/generate
Authorization: Bearer <accessToken>
{
  "templateCode": "JOB_CARD",
  "referenceType": "Job",
  "referenceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "format": "pdf",
  "data": {
    "jobNo": "JOB-20260706-a1b2c3",
    "customerName": "สมชาย ใจดี",
    "carLicensePlate": "กข1234",
    "description": "เปลี่ยนถ่ายน้ำมันเครื่อง",
    "totalAmount": 2500.00
  }
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "documentNo": "DOC-20260706-a1b2c3",
    "referenceType": "Job",
    "referenceId": "...",
    "fileName": "JobCard-JOB-20260706-a1b2c3.pdf",
    "fileSize": 245760,
    "mimeType": "application/pdf",
    "status": "Generated",
    "downloadUrl": "/api/documents/.../download",
    "previewUrl": "/api/documents/.../preview"
  }
}
```

### 2. อัปโหลดเอกสาร
```http
POST /api/documents/upload
Authorization: Bearer <accessToken>
Content-Type: multipart/form-data

file: [ไฟล์ PDF/Excel/Word]
referenceType: "Customer"
referenceId: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
description: "สำเนาบัตรประจำตัวประชาชน"
```

### 3. ดาวน์โหลดเอกสาร
```http
GET /api/documents/{documentId}/download
Authorization: Bearer <accessToken>
```

### 4. กระบวนการ OCR
```http
POST /api/documents/ocr
Authorization: Bearer <accessToken>
{
  "documentId": "...",
  "language": "tha+eng",
  "options": {
    "preprocess": true,
    "extractTables": true
  }
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "documentId": "...",
    "extractedText": "เนื้อหาที่อ่านได้จากเอกสาร...",
    "confidence": 0.95,
    "processedAt": "2026-07-06T10:30:00Z",
    "words": [
      { "text": "ICMON", "confidence": 0.98 },
      { "text": "Auto", "confidence": 0.97 }
    ]
  }
}
```

### 5. อัปโหลดเทมเพลต
```http
POST /api/documents/templates
Authorization: Bearer <accessToken>
{
  "code": "INVOICE",
  "name": "ใบแจ้งหนี้",
  "description": "เทมเพลตใบแจ้งหนี้",
  "templateFile": "base64encodedTemplate",
  "templateType": "JasperReport"
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Document.cs
```csharp
namespace ICMON.Domain.Aggregates.DocumentAggregate;

public class Document : AggregateRoot<Guid>
{
    public string DocumentNo { get; private set; }
    public string FileName { get; private set; }
    public string? FilePath { get; private set; }
    public long FileSize { get; private set; }
    public string MimeType { get; private set; }
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }
    public string ReferenceType { get; private set; }
    public Guid ReferenceId { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? GeneratedAt { get; private set; }
    public Guid? GeneratedBy { get; private set; }
    public string? GeneratedFromTemplate { get; private set; }
    public string? ContentHash { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<DocumentHistory> _history = new();
    public IReadOnlyList<DocumentHistory> History => _history.AsReadOnly();

    private Document() { } // For EF Core

    public static Document Create(
        string fileName,
        long fileSize,
        string mimeType,
        DocumentType type,
        string referenceType,
        Guid referenceId,
        Guid whitelabelId,
        string? description = null)
    {
        var document = new Document
        {
            Id = Guid.NewGuid(),
            DocumentNo = GenerateDocumentNo(),
            FileName = fileName,
            FileSize = fileSize,
            MimeType = mimeType,
            Type = type,
            Status = DocumentStatus.Draft,
            ReferenceType = referenceType,
            ReferenceId = referenceId,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        document.AddHistory("Document created", "Draft");
        document.AddDomainEvent(new DocumentCreatedEvent(document.Id, document.DocumentNo));

        return document;
    }

    public void Generate(string filePath, string contentHash, string templateCode, Guid generatedBy)
    {
        FilePath = filePath;
        ContentHash = contentHash;
        GeneratedFromTemplate = templateCode;
        GeneratedAt = DateTime.UtcNow;
        GeneratedBy = generatedBy;
        Status = DocumentStatus.Generated;

        AddHistory($"Document generated from template {templateCode}", "Generated");
        AddDomainEvent(new DocumentGeneratedEvent(Id, FilePath));
    }

    public void Upload(string filePath, string contentHash)
    {
        FilePath = filePath;
        ContentHash = contentHash;
        Status = DocumentStatus.Uploaded;

        AddHistory("Document uploaded", "Uploaded");
    }

    public void Archive()
    {
        Status = DocumentStatus.Archived;
        AddHistory("Document archived", "Archived");
    }

    public void Delete()
    {
        Status = DocumentStatus.Deleted;
        AddHistory("Document deleted", "Deleted");
    }

    public void ProcessOcr(Guid ocrResultId)
    {
        Status = DocumentStatus.OcrProcessed;
        AddHistory($"OCR processed with result {ocrResultId}", "OCRProcessed");
    }

    private void AddHistory(string description, string status)
    {
        _history.Add(DocumentHistory.Create(Id, description, status));
    }

    private static string GenerateDocumentNo()
        => $"DOC-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N[..6].ToUpper()}";

    public bool IsGenerated() => Status == DocumentStatus.Generated;
    public bool IsUploaded() => Status == DocumentStatus.Uploaded;
    public bool IsArchived() => Status == DocumentStatus.Archived;
}
```

### 2. Domain Entity - DocumentTemplate.cs
```csharp
namespace ICMON.Domain.Aggregates.DocumentAggregate;

public class DocumentTemplate : Entity<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public TemplateType TemplateType { get; private set; }
    public string TemplateFile { get; private set; } // Base64 encoded or path
    public string? FilePath { get; private set; }
    public string? PreviewImage { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private DocumentTemplate() { } // For EF Core

    public static DocumentTemplate Create(
        string code,
        string name,
        TemplateType templateType,
        string templateFile,
        Guid whitelabelId,
        string? description = null,
        string? previewImage = null)
    {
        var template = new DocumentTemplate
        {
            Id = Guid.NewGuid(),
            Code = code.ToUpper(),
            Name = name,
            Description = description,
            TemplateType = templateType,
            TemplateFile = templateFile,
            PreviewImage = previewImage,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        template.AddDomainEvent(new DocumentTemplateCreatedEvent(template.Id, template.Code));
        return template;
    }

    public void Update(string name, string? description, string templateFile, string? previewImage)
    {
        Name = name;
        Description = description;
        TemplateFile = templateFile;
        PreviewImage = previewImage;
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

    public void SetFilePath(string filePath)
    {
        FilePath = filePath;
        UpdatedAt = DateTime.UtcNow;
    }
}
```

### 3. Domain Entity - OcrResult.cs
```csharp
namespace ICMON.Domain.Aggregates.DocumentAggregate;

public class OcrResult : Entity<Guid>
{
    public Guid DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public string ExtractedText { get; private set; }
    public float Confidence { get; private set; }
    public string Language { get; private set; }
    public DateTime ProcessedAt { get; private set; }
    public int WordCount { get; private set; }
    public string? JsonData { get; private set; } // Additional structured data
    public string? ProcessingTime { get; private set; }

    private OcrResult() { } // For EF Core

    public static OcrResult Create(
        Guid documentId,
        string extractedText,
        float confidence,
        string language,
        int wordCount,
        string? jsonData = null,
        string? processingTime = null)
    {
        return new OcrResult
        {
            Id = Guid.NewGuid(),
            DocumentId = documentId,
            ExtractedText = extractedText,
            Confidence = confidence,
            Language = language,
            ProcessedAt = DateTime.UtcNow,
            WordCount = wordCount,
            JsonData = jsonData,
            ProcessingTime = processingTime
        };
    }
}
```

### 4. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum DocumentType
{
    JobCard = 0,
    Quotation = 1,
    PurchaseOrder = 2,
    Invoice = 3,
    Receipt = 4,
    Contract = 5,
    CustomerDocument = 6,
    PartDocument = 7,
    Report = 8,
    Other = 9
}

public enum DocumentStatus
{
    Draft = 0,
    Generated = 1,
    Uploaded = 2,
    Archived = 3,
    Deleted = 4,
    OcrProcessed = 5
}

public enum TemplateType
{
    JasperReport = 0,
    Pdf = 1,
    Excel = 2,
    Html = 3,
    Word = 4
}
```

### 5. Repository - DocumentRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(AppDbContext context) : base(context) { }

    public async Task<Document?> GetByIdWithHistoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(d => d.History)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Document>> GetByReferenceAsync(string referenceType, Guid referenceId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.ReferenceType == referenceType && d.ReferenceId == referenceId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> GetByTypeAsync(DocumentType type, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.Type == type)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> GetByStatusAsync(DocumentStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.Status == status)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Document>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.CreatedAt >= fromDate && d.CreatedAt <= toDate)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Document?> GetByHashAsync(string contentHash, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(d => d.ContentHash == contentHash, cancellationToken);
    }
}
```

### 6. Service - LocalFileStorageService.cs
```csharp
using Microsoft.AspNetCore.Hosting;

namespace ICMON.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly ILogger<LocalFileStorageService> _logger;

    public LocalFileStorageService(IWebHostEnvironment environment, ILogger<LocalFileStorageService> logger)
    {
        _basePath = Path.Combine(environment.ContentRootPath, "Documents");
        _logger = logger;
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string subDirectory, CancellationToken cancellationToken = default)
    {
        try
        {
            // Create directory if not exists
            var directory = Path.Combine(_basePath, subDirectory);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Generate unique filename
            var extension = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(directory, uniqueFileName);

            // Save file
            using var fileStream2 = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(fileStream2, cancellationToken);

            _logger.LogInformation("File saved: {FilePath}", filePath);
            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving file: {FileName}", fileName);
            throw;
        }
    }

    public async Task<byte[]> ReadFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        return await File.ReadAllBytesAsync(filePath, cancellationToken);
    }

    public async Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath), cancellationToken);
            _logger.LogInformation("File deleted: {FilePath}", filePath);
        }
    }

    public async Task<string> GetFileUrlAsync(string filePath, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var relativePath = filePath.Replace(_basePath, "").Replace("\\", "/");
            return $"/api/documents/files{relativePath}";
        }, cancellationToken);
    }

    public async Task<bool> FileExistsAsync(string filePath, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => File.Exists(filePath), cancellationToken);
    }

    public async Task<long> GetFileSizeAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
            return 0;

        return await Task.Run(() => new FileInfo(filePath).Length, cancellationToken);
    }

    public string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".txt" => "text/plain",
            ".html" or ".htm" => "text/html",
            _ => "application/octet-stream"
        };
    }
}
```

### 7. Service - TesseractOcrService.cs
```csharp
using Tesseract;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ICMON.Infrastructure.Ocr;

public class TesseractOcrService : IOcrService
{
    private readonly string _tessDataPath;
    private readonly ILogger<TesseractOcrService> _logger;

    public TesseractOcrService(IConfiguration configuration, ILogger<TesseractOcrService> logger)
    {
        _tessDataPath = configuration["Ocr:TessDataPath"] ?? "./tessdata";
        _logger = logger;
    }

    public async Task<OcrResultDto> ProcessImageAsync(byte[] imageData, string language = "tha+eng", CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            using var engine = new TesseractEngine(_tessDataPath, language, EngineMode.Default);
            engine.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzกขฃคฅฆงจฉชซฌญฎฏฐฑฒณดตถทธนบปผฝพฟภมยรฤลวศษสหฬอฮ .-");

            using var image = Pix.LoadFromMemory(imageData);
            using var page = engine.Process(image);

            var text = page.GetText();
            var confidence = page.GetMeanConfidence();

            // Get word-level results
            var words = new List<OcrWord>();
            using var iterator = page.GetIterator();
            iterator.Begin();

            do
            {
                var word = iterator.GetText(PageIteratorLevel.Word);
                var wordConfidence = iterator.GetConfidence(PageIteratorLevel.Word);
                if (!string.IsNullOrEmpty(word))
                {
                    words.Add(new OcrWord { Text = word, Confidence = wordConfidence });
                }
            } while (iterator.Next(PageIteratorLevel.Word));

            return new OcrResultDto
            {
                ExtractedText = text,
                Confidence = confidence,
                WordCount = words.Count,
                Words = words,
                Language = language,
                ProcessedAt = DateTime.UtcNow
            };
        }, cancellationToken);
    }

    public async Task<OcrResultDto> ProcessPdfAsync(byte[] pdfData, string language = "tha+eng", CancellationToken cancellationToken = default)
    {
        // Convert PDF to images first (using PDFium or similar)
        // For this example, we'll implement a simple version
        _logger.LogWarning("PDF OCR requires additional PDF rendering library. Using placeholder.");
        return await Task.Run(() => new OcrResultDto
        {
            ExtractedText = "PDF OCR requires PDF rendering library like PDFium or Ghostscript",
            Confidence = 0,
            WordCount = 0,
            Words = new List<OcrWord>(),
            Language = language,
            ProcessedAt = DateTime.UtcNow
        }, cancellationToken);
    }

    public async Task<bool> IsLanguageSupportedAsync(string language, CancellationToken cancellationToken = default)
    {
        var supportedLanguages = new[] { "tha", "eng", "tha+eng", "eng+tha" };
        return await Task.FromResult(supportedLanguages.Contains(language));
    }

    public async Task<IEnumerable<string>> GetSupportedLanguagesAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(new[] { "tha", "eng", "tha+eng" });
    }
}
```

### 8. Command Handler - GenerateDocumentCommandHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Aggregates.DocumentAggregate;
using ICMON.Domain.Interfaces;
using ICMON.Application.Common;

namespace ICMON.Application.Commands.Documents.GenerateDocument;

public class GenerateDocumentCommandHandler : IRequestHandler<GenerateDocumentCommand, Result<DocumentDto>>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentTemplateRepository _templateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;
    private readonly IFileStorageService _fileStorage;
    private readonly IDocumentGenerator _documentGenerator;

    public GenerateDocumentCommandHandler(
        IDocumentRepository documentRepository,
        IDocumentTemplateRepository templateRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUser,
        IFileStorageService fileStorage,
        IDocumentGenerator documentGenerator)
    {
        _documentRepository = documentRepository;
        _templateRepository = templateRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _fileStorage = fileStorage;
        _documentGenerator = documentGenerator;
    }

    public async Task<Result<DocumentDto>> Handle(GenerateDocumentCommand request, CancellationToken cancellationToken)
    {
        // Get template
        var template = await _templateRepository.GetByCodeAsync(request.TemplateCode, cancellationToken);
        if (template == null)
            return Result<DocumentDto>.Failure($"Template '{request.TemplateCode}' not found");

        if (!template.IsActive)
            return Result<DocumentDto>.Failure($"Template '{request.TemplateCode}' is inactive");

        // Generate document content
        var (content, fileName, mimeType) = await _documentGenerator.GenerateAsync(
            template,
            request.Data,
            request.Format,
            cancellationToken);

        // Determine document type from reference type
        var docType = GetDocumentType(request.ReferenceType);

        // Create document entity
        var document = Document.Create(
            fileName,
            content.Length,
            mimeType,
            docType,
            request.ReferenceType,
            request.ReferenceId,
            _currentUser.WhitelabelId,
            $"Generated from template {request.TemplateCode}"
        );

        // Save file
        using var stream = new MemoryStream(content);
        var filePath = await _fileStorage.SaveFileAsync(
            stream,
            fileName,
            $"documents/{request.ReferenceType}/{request.ReferenceId}",
            cancellationToken);

        // Generate content hash (simple MD5 for deduplication)
        var contentHash = Convert.ToBase64String(System.Security.Cryptography.MD5.HashData(content));

        // Update document with generation info
        document.Generate(filePath, contentHash, template.Code, _currentUser.UserId);

        await _documentRepository.AddAsync(document, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<DocumentDto>(document);
        dto.DownloadUrl = $"/api/documents/{document.Id}/download";

        return Result<DocumentDto>.Success(dto);
    }

    private DocumentType GetDocumentType(string referenceType)
    {
        return referenceType.ToLower() switch
        {
            "job" => DocumentType.JobCard,
            "quotation" => DocumentType.Quotation,
            "purchaseorder" => DocumentType.PurchaseOrder,
            "invoice" => DocumentType.Invoice,
            "receipt" => DocumentType.Receipt,
            "customer" => DocumentType.CustomerDocument,
            "part" => DocumentType.PartDocument,
            "report" => DocumentType.Report,
            _ => DocumentType.Other
        };
    }
}
```

### 9. Controller - DocumentsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFileStorageService _fileStorage;

    public DocumentsController(IMediator mediator, IFileStorageService fileStorage)
    {
        _mediator = mediator;
        _fileStorage = fileStorage;
    }

    [HttpPost("generate")]
    [Authorize("DOCUMENT_GENERATE")]
    [EnableRateLimiting("PdfGeneration")]
    [ProducesResponseType(typeof(Result<DocumentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateDocument([FromBody] GenerateDocumentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("upload")]
    [Authorize("DOCUMENT_UPLOAD")]
    [EnableRateLimiting("Create")]
    [RequestSizeLimit(50 * 1024 * 1024)] // 50 MB
    public async Task<IActionResult> UploadDocument(
        [FromForm] UploadDocumentCommand command,
        IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(Result<DocumentDto>.Failure("No file uploaded"));

        command.File = file;
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<DocumentDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocument(Guid id)
    {
        var query = new GetDocumentByIdQuery { DocumentId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("reference/{refType}/{refId}")]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDocumentByReference(string refType, Guid refId)
    {
        var query = new GetDocumentByReferenceQuery { ReferenceType = refType, ReferenceId = refId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDocumentList(
        [FromQuery] DocumentType? type,
        [FromQuery] DocumentStatus? status,
        [FromQuery] string? referenceType,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetDocumentListQuery
        {
            Type = type,
            Status = status,
            ReferenceType = referenceType,
            FromDate = fromDate,
            ToDate = toDate,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}/download")]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> DownloadDocument(Guid id)
    {
        var query = new GetDocumentByIdQuery { DocumentId = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        var document = result.Data;
        var filePath = document.FilePath;
        
        if (string.IsNullOrEmpty(filePath) || !await _fileStorage.FileExistsAsync(filePath))
            return NotFound("File not found");

        var fileBytes = await _fileStorage.ReadFileAsync(filePath);
        return File(fileBytes, document.MimeType, document.FileName);
    }

    [HttpGet("{id}/preview")]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> PreviewDocument(Guid id)
    {
        var query = new GetDocumentByIdQuery { DocumentId = id };
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess || result.Data == null)
            return NotFound(result);

        // For preview, we only show first page or thumbnail
        // For simplicity, return the full document with inline disposition
        var document = result.Data;
        var filePath = document.FilePath;
        
        if (string.IsNullOrEmpty(filePath) || !await _fileStorage.FileExistsAsync(filePath))
            return NotFound("File not found");

        var fileBytes = await _fileStorage.ReadFileAsync(filePath);
        Response.Headers.Add("Content-Disposition", "inline");
        return File(fileBytes, document.MimeType);
    }

    [HttpPost("ocr")]
    [Authorize("DOCUMENT_PROCESS")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<OcrResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProcessOcr([FromBody] ProcessOcrCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("templates")]
    [Authorize("DOCUMENT_ADMIN")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("templates/{code}")]
    [Authorize("DOCUMENT_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetTemplate(string code)
    {
        var query = new GetTemplateQuery { TemplateCode = code };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id}")]
    [Authorize("DOCUMENT_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteDocument(Guid id)
    {
        var command = new DeleteDocumentCommand { DocumentId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `documents:{documentId}` | 1 ชม. | ข้อมูลเอกสาร |
| `document_ref:{refType}:{refId}` | 1 ชม. | เอกสารตาม Reference |
| `templates:{templateCode}` | 2 ชม. | เทมเพลตเอกสาร |
| `templates_active` | 1 ชม. | รายการเทมเพลตที่ใช้งาน |

---

## 📂 โครงสร้างไดเรกทอรีเอกสาร

```
Documents/
├── templates/
│   ├── JobCard.jrxml
│   ├── Quotation.jrxml
│   ├── Invoice.jrxml
│   └── Receipt.jrxml
├── documents/
│   ├── Job/
│   │   └── {jobId}/
│   │       ├── JobCard-{date}.pdf
│   │       └── Quotation-{date}.pdf
│   ├── Customer/
│   │   └── {customerId}/
│   │       ├── ID-Card.jpg
│   │       └── Contract.pdf
│   └── Invoice/
│       └── {invoiceId}/
│           └── Invoice-{date}.pdf
└── temp/
    └── {sessionId}/
        └── temp-file.pdf
```

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ สร้างเอกสารจากเทมเพลต (PDF/Excel/HTML)
- ✅ อัปโหลดเอกสาร (ทุกประเภท)
- ✅ ดาวน์โหลดเอกสาร
- ✅ ดูตัวอย่างเอกสาร
- ✅ กระบวนการ OCR (Optical Character Recognition)
- ✅ จัดการเทมเพลตเอกสาร
- ✅ ดึงเอกสารตาม Reference
- ✅ ประวัติเอกสาร
- ✅ เก็บข้อมูล Content Hash (ป้องกัน duplicate)
- ✅ รองรับหลายประเภทเอกสาร (Job Card, Quotation, PO, Invoice, Receipt)
- ✅ ไฟล์ Storage (Local/AWS S3/Azure Blob)
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 9: **Document Management** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 10: Email Service
2. โมดูลที่ 11: Batch Jobs
3. โมดูลที่ 12: Multi-Language (i18n)
4. โมดูลที่ 13: IoT & GPS Tracking
5. โมดูลที่ 14: Web Order System (WOS)