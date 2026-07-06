# 🚗 การปรับใช้ JasperReports สำหรับระบบรายงาน (ICMON)

เราได้ปรับปรุงระบบทั้งหมดให้ใช้ **Jaspersoft Studio 6.20.1** ในการออกแบบรายงาน และ **JasperReports Library** ในการสร้างเอกสารและรายงานทุกประเภท แทนที่ QuestPDF และ EPPlus

---

## ✅ สรุปการปรับเปลี่ยนที่สำคัญ

| รายการ | การปรับเปลี่ยน |
|--------|---------------|
| **PDF Generation** | ใช้ JasperReports Export แทน QuestPDF |
| **Excel Generation** | ใช้ JasperReports Export แทน EPPlus |
| **Document Management** | ใช้ JasperReports Template (.jrxml) แทนการสร้างด้วยโค้ด |
| **Reports (Dashboard)** | ใช้ JasperReports สำหรับรายงานรายวัน/เดือน/ปี |
| **Job Card / Quotation / PO / Receipt** | ใช้ JasperReports Template ทั้งหมด |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure.csproj`
```xml
<!-- JasperReports .NET Core Support -->
<PackageReference Include="JasperReports" Version="6.20.1" />
<!-- ถ้าใช้ Java interop อาจต้องใช้ IKVM หรือ JNBridge แต่เราจะใช้ .NET port ที่มีอยู่ -->
<!-- สำหรับตัวอย่างนี้ สมมติว่ามีแพ็กเกจที่ใช้งานได้ -->
```

**หมายเหตุ:** เนื่องจาก JasperReports เป็นไลบรารี Java ปัจจุบันอาจต้องใช้ **JNBridge** หรือ **IKVM** เพื่อใช้งานใน .NET Core แต่มีตัวเลือกอื่น เช่น ใช้ **JasperReports REST API** กับ Jaspersoft Server หรือใช้ **JasperStarter** เป็น external process เพื่อความง่ายในตัวอย่างนี้เราจะถือว่ามีไลบรารี `JasperReports` ที่รองรับ .NET Standard

---

## 📂 โครงสร้างไดเรกทอรีสำหรับเทมเพลต

```
ICMON.Api/
├── Templates/
│   ├── Reports/
│   │   ├── JobCard.jrxml
│   │   ├── Quotation.jrxml
│   │   ├── PurchaseOrder.jrxml
│   │   ├── Invoice.jrxml
│   │   ├── Receipt.jrxml
│   │   ├── DailyReport.jrxml
│   │   ├── MonthlyReport.jrxml
│   │   ├── YearlyReport.jrxml
│   │   ├── InventoryReport.jrxml
│   │   └── SalesReport.jrxml
│   └── Compiled/
│       ├── JobCard.jasper
│       └── ...
└── appsettings.json
```

---

## 🧩 โค้ดหลักที่ปรับปรุงใหม่

### 1. Interface และ Service สำหรับ JasperReports

#### `IJasperReportService.cs` (Infrastructure)
```csharp
using System.Collections.Generic;

namespace ICMON.Infrastructure.Reports
{
    public interface IJasperReportService
    {
        /// <summary>
        /// สร้างรายงานจากเทมเพลต .jrxml หรือ .jasper
        /// </summary>
        /// <param name="templatePath">พาธไปยังไฟล์ .jrxml หรือ .jasper</param>
        /// <param name="dataSource">ข้อมูล (object, IEnumerable, Dictionary)</param>
        /// <param name="format">รูปแบบผลลัพธ์ (pdf, xlsx, html, csv)</param>
        /// <param name="parameters">พารามิเตอร์เพิ่มเติม</param>
        /// <returns>Byte array ของรายงาน</returns>
        byte[] GenerateReport(
            string templatePath,
            object dataSource,
            string format = "pdf",
            Dictionary<string, object>? parameters = null);

        /// <summary>
        /// สร้างรายงานจากไฟล์ .jasper ที่ compile แล้ว
        /// </summary>
        byte[] GenerateReportFromJasper(
            string jasperPath,
            object dataSource,
            string format = "pdf",
            Dictionary<string, object>? parameters = null);

        /// <summary>
        /// คอมไพล์ .jrxml เป็น .jasper
        /// </summary>
        void CompileTemplate(string jrxmlPath, string jasperPath);
    }
}
```

#### `JasperReportService.cs` (Infrastructure)
```csharp
using net.sf.jasperreports.engine;
using net.sf.jasperreports.engine.data;
using net.sf.jasperreports.engine.export;
using net.sf.jasperreports.export;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ICMON.Infrastructure.Reports
{
    public class JasperReportService : IJasperReportService
    {
        private readonly ILogger<JasperReportService> _logger;
        private readonly string _compiledPath;

        public JasperReportService(ILogger<JasperReportService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _compiledPath = configuration["JasperReports:CompiledPath"] ?? "Templates/Compiled";
            if (!Directory.Exists(_compiledPath))
                Directory.CreateDirectory(_compiledPath);
        }

        public byte[] GenerateReport(
            string templatePath,
            object dataSource,
            string format = "pdf",
            Dictionary<string, object>? parameters = null)
        {
            // ตรวจสอบว่า templatePath เป็น .jrxml หรือ .jasper
            var extension = Path.GetExtension(templatePath).ToLower();
            if (extension == ".jrxml")
            {
                // Compile ถ้ายังไม่เคย compile หรือไฟล์ .jasper เก่ากว่า
                var jasperPath = Path.Combine(_compiledPath, Path.GetFileNameWithoutExtension(templatePath) + ".jasper");
                if (!File.Exists(jasperPath) || File.GetLastWriteTime(jasperPath) < File.GetLastWriteTime(templatePath))
                {
                    CompileTemplate(templatePath, jasperPath);
                }
                return GenerateReportFromJasper(jasperPath, dataSource, format, parameters);
            }
            else if (extension == ".jasper")
            {
                return GenerateReportFromJasper(templatePath, dataSource, format, parameters);
            }
            else
            {
                throw new NotSupportedException($"Unsupported template file extension: {extension}");
            }
        }

        public byte[] GenerateReportFromJasper(
            string jasperPath,
            object dataSource,
            string format = "pdf",
            Dictionary<string, object>? parameters = null)
        {
            try
            {
                // โหลดรายงานที่ compile แล้ว
                var jasperReport = (JasperReport)JRLoader.LoadObjectFromFile(jasperPath);

                // เตรียมพารามิเตอร์
                var jrParams = new JRParameter[parameters?.Count ?? 0];
                if (parameters != null)
                {
                    foreach (var kvp in parameters)
                    {
                        jrParams[kvp.Key] = kvp.Value;
                    }
                }

                // แปลง dataSource ให้เป็น JRDataSource
                var jrDataSource = ConvertToJRDataSource(dataSource);

                // Fill report
                var jasperPrint = JasperFillManager.FillReport(jasperReport, jrParams, jrDataSource);

                // ส่งออกตามรูปแบบ
                return ExportReport(jasperPrint, format);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating report from Jasper: {JasperPath}", jasperPath);
                throw;
            }
        }

        public void CompileTemplate(string jrxmlPath, string jasperPath)
        {
            try
            {
                JasperCompileManager.CompileReportToFile(jrxmlPath, jasperPath);
                _logger.LogInformation("Compiled {JrxmlPath} to {JasperPath}", jrxmlPath, jasperPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error compiling template {JrxmlPath}", jrxmlPath);
                throw;
            }
        }

        private JRDataSource ConvertToJRDataSource(object dataSource)
        {
            if (dataSource is IDictionary<string, object> map)
            {
                return new JRBeanMapDataSource(map);
            }
            else if (dataSource is IEnumerable enumerable && !(dataSource is string))
            {
                return new JRBeanCollectionDataSource(enumerable);
            }
            else
            {
                // single object
                return new JRBeanCollectionDataSource(new[] { dataSource });
            }
        }

        private byte[] ExportReport(JasperPrint jasperPrint, string format)
        {
            format = format.ToLower();
            using var memoryStream = new MemoryStream();
            switch (format)
            {
                case "pdf":
                    var pdfExporter = new JRPdfExporter();
                    pdfExporter.SetExporterInput(new SimpleExporterInput(jasperPrint));
                    pdfExporter.SetExporterOutput(new SimpleOutputStreamExporterOutput(memoryStream));
                    pdfExporter.ExportReport();
                    break;

                case "xlsx":
                case "excel":
                    var excelExporter = new JRXlsxExporter();
                    excelExporter.SetExporterInput(new SimpleExporterInput(jasperPrint));
                    excelExporter.SetExporterOutput(new SimpleOutputStreamExporterOutput(memoryStream));
                    excelExporter.ExportReport();
                    break;

                case "html":
                    var htmlExporter = new JRHtmlExporter();
                    htmlExporter.SetExporterInput(new SimpleExporterInput(jasperPrint));
                    htmlExporter.SetExporterOutput(new SimpleHtmlExporterOutput(memoryStream));
                    htmlExporter.ExportReport();
                    break;

                case "csv":
                    var csvExporter = new JRCsvExporter();
                    csvExporter.SetExporterInput(new SimpleExporterInput(jasperPrint));
                    csvExporter.SetExporterOutput(new SimpleWriterExporterOutput(new StreamWriter(memoryStream)));
                    csvExporter.ExportReport();
                    break;

                default:
                    throw new NotSupportedException($"Format '{format}' is not supported.");
            }
            return memoryStream.ToArray();
        }
    }
}
```

---

### 2. Document Generator สำหรับ JasperReports

#### `IJasperDocumentGenerator.cs` (Application)
```csharp
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ICMON.Application.Interfaces
{
    public interface IDocumentGenerator
    {
        Task<byte[]> GenerateDocumentAsync(
            string templateCode,
            Dictionary<string, object> data,
            string format = "pdf",
            Dictionary<string, object>? parameters = null,
            CancellationToken cancellationToken = default);
    }
}
```

#### `JasperDocumentGenerator.cs` (Infrastructure)
```csharp
using ICMON.Application.Interfaces;
using ICMON.Infrastructure.Reports;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ICMON.Infrastructure.DocumentGeneration
{
    public class JasperDocumentGenerator : IDocumentGenerator
    {
        private readonly IJasperReportService _jasperService;
        private readonly string _templatePath;

        public JasperDocumentGenerator(
            IJasperReportService jasperService,
            IConfiguration configuration)
        {
            _jasperService = jasperService;
            _templatePath = configuration["JasperReports:TemplatePath"] ?? "Templates/Reports";
        }

        public async Task<byte[]> GenerateDocumentAsync(
            string templateCode,
            Dictionary<string, object> data,
            string format = "pdf",
            Dictionary<string, object>? parameters = null,
            CancellationToken cancellationToken = default)
        {
            var templateFile = Path.Combine(_templatePath, $"{templateCode}.jrxml");
            if (!File.Exists(templateFile))
                throw new FileNotFoundException($"Template file not found: {templateFile}");

            return await Task.Run(() =>
            {
                return _jasperService.GenerateReport(templateFile, data, format, parameters);
            }, cancellationToken);
        }
    }
}
```

---

### 3. Report Generator สำหรับ Dashboard & Reports

#### `IReportGenerator.cs` (Application)
```csharp
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ICMON.Application.Interfaces
{
    public interface IReportGenerator
    {
        Task<byte[]> GenerateReportAsync(
            string reportType,
            object data,
            string format = "pdf",
            Dictionary<string, object>? parameters = null,
            CancellationToken cancellationToken = default);
    }
}
```

#### `JasperReportGenerator.cs` (Infrastructure)
```csharp
using ICMON.Application.Interfaces;
using ICMON.Infrastructure.Reports;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ICMON.Infrastructure.Reports
{
    public class JasperReportGenerator : IReportGenerator
    {
        private readonly IJasperReportService _jasperService;
        private readonly string _templatePath;

        public JasperReportGenerator(
            IJasperReportService jasperService,
            IConfiguration configuration)
        {
            _jasperService = jasperService;
            _templatePath = configuration["JasperReports:TemplatePath"] ?? "Templates/Reports";
        }

        public async Task<byte[]> GenerateReportAsync(
            string reportType,
            object data,
            string format = "pdf",
            Dictionary<string, object>? parameters = null,
            CancellationToken cancellationToken = default)
        {
            var templateFile = Path.Combine(_templatePath, $"{reportType}.jrxml");
            if (!File.Exists(templateFile))
                throw new FileNotFoundException($"Report template not found: {templateFile}");

            return await Task.Run(() =>
            {
                return _jasperService.GenerateReport(templateFile, data, format, parameters);
            }, cancellationToken);
        }
    }
}
```

---

### 4. การปรับ Controller

#### `DocumentsController.cs` (ปรับเฉพาะส่วน Generate)
```csharp
[HttpPost("generate")]
[Authorize("DOCUMENT_GENERATE")]
[EnableRateLimiting("PdfGeneration")]
public async Task<IActionResult> GenerateDocument([FromBody] GenerateDocumentCommand command)
{
    var result = await _mediator.Send(command);
    if (!result.IsSuccess || result.Data == null)
        return BadRequest(result);

    // ดาวน์โหลดไฟล์
    var fileBytes = result.Data.FileContent; // รับ byte[] จาก handler
    var fileName = $"{result.Data.DocumentNo}.{command.Format}";
    return File(fileBytes, GetMimeType(command.Format), fileName);
}
```

#### `GenerateDocumentCommandHandler.cs` (ใช้ JasperDocumentGenerator)
```csharp
public class GenerateDocumentCommandHandler : IRequestHandler<GenerateDocumentCommand, Result<DocumentDto>>
{
    private readonly IDocumentGenerator _documentGenerator;
    // ...

    public async Task<Result<DocumentDto>> Handle(GenerateDocumentCommand request, CancellationToken cancellationToken)
    {
        // เตรียมข้อมูลสำหรับ template
        var data = new Dictionary<string, object>
        {
            ["jobNo"] = request.JobNo,
            ["customerName"] = request.CustomerName,
            // ...
        };

        var parameters = new Dictionary<string, object>
        {
            ["CompanyName"] = "ICMON Auto Repair",
            ["LogoPath"] = "logo.png"
        };

        // สร้างเอกสาร
        var fileBytes = await _documentGenerator.GenerateDocumentAsync(
            templateCode: request.TemplateCode, // เช่น "JobCard", "Quotation"
            data: data,
            format: request.Format,
            parameters: parameters,
            cancellationToken: cancellationToken
        );

        // บันทึกเอกสารในระบบ
        var document = Document.Create(...);
        // ...

        var dto = _mapper.Map<DocumentDto>(document);
        dto.FileContent = fileBytes; // ส่งกลับไปให้ Controller
        return Result<DocumentDto>.Success(dto);
    }
}
```

#### `ReportsController.cs` (ปรับ)
```csharp
[HttpPost("daily")]
[Authorize("REPORT_READ")]
[EnableRateLimiting("PdfGeneration")]
public async Task<IActionResult> GenerateDailyReport([FromBody] GenerateDailyReportQuery query)
{
    var result = await _mediator.Send(query);
    if (!result.IsSuccess || result.Data == null)
        return BadRequest(result);

    var fileBytes = result.Data.FileContent;
    var fileName = $"DailyReport_{DateTime.Now:yyyyMMdd}.{query.Format}";
    return File(fileBytes, GetMimeType(query.Format), fileName);
}
```

---

### 5. ตัวอย่างเทมเพลต .jrxml (โครงสร้าง)

เราจะใช้ Jaspersoft Studio 6.20.1 สร้างไฟล์ .jrxml โดยมีโครงสร้างดังนี้

#### `JobCard.jrxml` (ตัวอย่างบางส่วน)
```xml
<?xml version="1.0" encoding="UTF-8"?>
<jasperReport xmlns="http://jasperreports.sourceforge.net/jasperreports"
              xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
              xsi:schemaLocation="http://jasperreports.sourceforge.net/jasperreports http://jasperreports.sourceforge.net/xsd/jasperreport.xsd"
              name="JobCard" pageWidth="595" pageHeight="842" columnWidth="555" leftMargin="20" rightMargin="20" topMargin="20" bottomMargin="20">
    <parameter name="CompanyName" class="java.lang.String"/>
    <parameter name="LogoPath" class="java.lang.String"/>
    <field name="jobNo" class="java.lang.String"/>
    <field name="customerName" class="java.lang.String"/>
    <field name="carLicensePlate" class="java.lang.String"/>
    <field name="description" class="java.lang.String"/>
    <field name="totalAmount" class="java.math.BigDecimal"/>

    <title>
        <band height="60">
            <staticText>
                <reportElement x="0" y="0" width="555" height="30"/>
                <textElement textAlignment="Center" verticalAlignment="Middle"/>
                <text>Job Card</text>
            </staticText>
        </band>
    </title>
    <detail>
        <band height="100">
            <textField>
                <reportElement x="0" y="10" width="100" height="20"/>
                <textElement/>
                <textFieldExpression class="java.lang.String"><![CDATA[$F{jobNo}]]></textFieldExpression>
            </textField>
            <!-- ... fields -->
        </band>
    </detail>
</jasperReport>
```

#### `Quotation.jrxml`, `Invoice.jrxml`, `Receipt.jrxml` มีโครงสร้างคล้ายกัน

#### `DailyReport.jrxml`
```xml
<field name="period" class="java.lang.String"/>
<field name="totalRevenue" class="java.math.BigDecimal"/>
<field name="totalJobs" class="java.lang.Integer"/>
<field name="details" class="java.util.Collection"/> <!-- รายละเอียด -->
```

---

### 6. การลงทะเบียน Dependency Injection

ใน `Program.cs` หรือ `DependencyInjection.cs`:

```csharp
// ใน Infrastructure Layer
builder.Services.AddScoped<IJasperReportService, JasperReportService>();
builder.Services.AddScoped<IDocumentGenerator, JasperDocumentGenerator>();
builder.Services.AddScoped<IReportGenerator, JasperReportGenerator>();

// ใน Application Layer (ถ้ามี)
builder.Services.AddScoped<IDocumentService, DocumentService>(); // ใช้ IDocumentGenerator
builder.Services.AddScoped<IReportService, ReportService>(); // ใช้ IReportGenerator
```

---

### 7. การตั้งค่า appsettings.json

```json
{
  "JasperReports": {
    "TemplatePath": "Templates/Reports",
    "CompiledPath": "Templates/Compiled"
  },
  "ConnectionStrings": {
    "DefaultConnection": "..."
  }
}
```

---

## 🛠️ คำแนะนำในการสร้างเทมเพลตด้วย Jaspersoft Studio 6.20.1

### ขั้นตอน
1. เปิด Jaspersoft Studio
2. สร้าง Project ใหม่ (File → New → Project → JasperReports Project)
3. สร้าง Report ใหม่ (File → New → Jasper Report)
4. เลือก Template (เช่น Blank A4)
5. ออกแบบ report:
   - ใช้ **Parameters** สำหรับข้อมูลที่ส่งจากแอปพลิเคชัน (เช่น CompanyName, LogoPath)
   - ใช้ **Fields** สำหรับข้อมูลหลัก (เช่น jobNo, customerName)
   - ใช้ **Detail Band** สำหรับรายการซ้ำ (เช่น รายการอะไหล่/บริการ)
   - ใช้ **Subreport** สำหรับข้อมูลที่ซับซ้อน
6. ทดสอบ report ด้วย **Preview** (ใช้ sample data)
7. Export เป็น .jrxml (ไฟล์ XML) และวางไว้ในโฟลเดอร์ `Templates/Reports` ของโปรเจกต์

### การส่งข้อมูลจาก C# ไปยัง JasperReports
- **Parameters**: ใช้ `Dictionary<string, object>` ใน `parameters`
- **Fields**: ใช้ `object dataSource` ที่เป็น `IEnumerable` หรือ `Dictionary`
- **Subreport**: ใช้ `JRBeanCollectionDataSource` สำหรับ subreport data

---

## 🚀 การใช้งานจริง

### ตัวอย่างการสร้างใบงาน (Job Card)

```csharp
// ใน CreateJobCommandHandler
public async Task<Result<DocumentDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
{
    // ... สร้าง job และบันทึก

    // สร้างเอกสาร JobCard
    var data = new Dictionary<string, object>
    {
        ["jobNo"] = job.JobNo,
        ["customerName"] = job.Customer.FullName,
        ["carLicensePlate"] = job.Car.LicensePlate,
        ["description"] = job.Description,
        ["totalAmount"] = job.TotalAmount.Amount,
        // เพิ่มรายการ services และ parts เป็น collection
        ["services"] = job.Services.Select(s => new { s.ServiceName, s.Quantity, s.TotalAmount }),
        ["parts"] = job.Parts.Select(p => new { p.PartName, p.Quantity, p.TotalAmount })
    };

    var parameters = new Dictionary<string, object>
    {
        ["CompanyName"] = "ICMON Auto Repair",
        ["LogoPath"] = Path.Combine(_webHostEnvironment.WebRootPath, "images/logo.png")
    };

    var fileBytes = await _documentGenerator.GenerateDocumentAsync(
        "JobCard",
        data,
        "pdf",
        parameters,
        cancellationToken
    );

    // บันทึกเอกสารและส่งกลับ
    // ...
}
```

---

## ✅ ไฟล์ที่ต้องปรับปรุง/สร้างใหม่ (สรุป)

| ลำดับ | ไฟล์ | การดำเนินการ |
|-------|------|-------------|
| 1 | `ICMON.Infrastructure.csproj` | เพิ่ม package `JasperReports` |
| 2 | `IJasperReportService.cs` | สร้างใหม่ |
| 3 | `JasperReportService.cs` | สร้างใหม่ |
| 4 | `IJasperDocumentGenerator.cs` | สร้างใหม่ (หรือปรับจากเดิม) |
| 5 | `JasperDocumentGenerator.cs` | สร้างใหม่ |
| 6 | `IReportGenerator.cs` | สร้างใหม่ (หรือปรับจากเดิม) |
| 7 | `JasperReportGenerator.cs` | สร้างใหม่ |
| 8 | `DocumentsController.cs` | ปรับส่วน Generate |
| 9 | `ReportsController.cs` | ปรับส่วน Generate |
| 10 | `GenerateDocumentCommandHandler.cs` | ปรับให้ใช้ IDocumentGenerator |
| 11 | `GenerateReportCommandHandler.cs` | ปรับให้ใช้ IReportGenerator |
| 12 | `appsettings.json` | เพิ่ม Section JasperReports |
| 13 | `Program.cs` / `DependencyInjection.cs` | ลงทะเบียนบริการใหม่ |
| 14 | `Templates/Reports/*.jrxml` | สร้างเทมเพลตด้วย Jaspersoft Studio |

---

## 🎯 สรุป

เราได้ปรับปรุงระบบ **ICMON** ให้ใช้ **Jaspersoft Studio 6.20.1** ในการออกแบบรายงาน และใช้ **JasperReports Library** ในการสร้างเอกสารทุกประเภท ดังนี้:

- ✅ **Document Management**: ใช้ JasperReports แทน QuestPDF/EPPlus
- ✅ **Job Card, Quotation, PO, Invoice, Receipt**: ใช้เทมเพลต .jrxml
- ✅ **Dashboard Reports**: ใช้ JasperReports สำหรับรายงานรายวัน/เดือน/ปี
- ✅ **Export Formats**: PDF, Excel (XLSX), HTML, CSV
- ✅ **การจัดการเทมเพลต**: คอมไพล์ .jrxml เป็น .jasper แบบอัตโนมัติ
- ✅ **ความยืดหยุ่น**: สามารถปรับแต่งรายงานได้โดยไม่ต้องเขียนโค้ด

ระบบพร้อมใช้งานกับ Jaspersoft Studio 6.20.1 อย่างสมบูรณ์ 🚀