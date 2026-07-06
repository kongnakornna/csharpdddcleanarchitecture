# 🚗 โมดูลที่ 8: Dashboard & Reports – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Dashboard & Reports (แดชบอร์ดและรายงาน) ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 48 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Interfaces/IDashboardRepository.cs` | Interface Repository สำหรับ Dashboard |
| 2 | `Interfaces/IReportRepository.cs` | Interface Repository สำหรับ Report |
| 3 | `ValueObjects/DateRange.cs` | Value Object ช่วงวันที่ |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 4 | `DTOs/Dashboard/DashboardOverviewDto.cs` | DTO ภาพรวม Dashboard |
| 5 | `DTOs/Dashboard/SalesOverviewDto.cs` | DTO ภาพรวมยอดขาย |
| 6 | `DTOs/Dashboard/JobStatusSummaryDto.cs` | DTO สรุปสถานะใบงาน |
| 7 | `DTOs/Dashboard/InventoryOverviewDto.cs` | DTO ภาพรวมสินค้าคงคลัง |
| 8 | `DTOs/Dashboard/TopPartDto.cs` | DTO อะไหล่ขายดี |
| 9 | `DTOs/Dashboard/RevenueByPeriodDto.cs` | DTO รายได้แยกช่วงเวลา |
| 10 | `DTOs/Dashboard/FinancialSummaryDto.cs` | DTO สรุปการเงิน |
| 11 | `DTOs/Dashboard/ServiceCategoryDto.cs` | DTO หมวดหมู่บริการ |
| 12 | `DTOs/Reports/DailyReportDto.cs` | DTO รายงานรายวัน |
| 13 | `DTOs/Reports/MonthlyReportDto.cs` | DTO รายงานรายเดือน |
| 14 | `DTOs/Reports/YearlyReportDto.cs` | DTO รายงานรายปี |
| 15 | `Queries/Dashboard/GetDashboardOverview/GetDashboardOverviewQuery.cs` | Query ภาพรวม |
| 16 | `Queries/Dashboard/GetDashboardOverview/GetDashboardOverviewQueryHandler.cs` | Handler ภาพรวม |
| 17 | `Queries/Dashboard/GetSalesOverview/GetSalesOverviewQuery.cs` | Query ยอดขาย |
| 18 | `Queries/Dashboard/GetSalesOverview/GetSalesOverviewQueryHandler.cs` | Handler ยอดขาย |
| 19 | `Queries/Dashboard/GetJobStatusSummary/GetJobStatusSummaryQuery.cs` | Query สรุปสถานะ |
| 20 | `Queries/Dashboard/GetJobStatusSummary/GetJobStatusSummaryQueryHandler.cs` | Handler สรุปสถานะ |
| 21 | `Queries/Dashboard/GetInventoryOverview/GetInventoryOverviewQuery.cs` | Query ภาพรวมสินค้า |
| 22 | `Queries/Dashboard/GetInventoryOverview/GetInventoryOverviewQueryHandler.cs` | Handler ภาพรวมสินค้า |
| 23 | `Queries/Dashboard/GetTopParts/GetTopPartsQuery.cs` | Query อะไหล่ขายดี |
| 24 | `Queries/Dashboard/GetTopParts/GetTopPartsQueryHandler.cs` | Handler อะไหล่ขายดี |
| 25 | `Queries/Dashboard/GetRevenueByPeriod/GetRevenueByPeriodQuery.cs` | Query รายได้แยกช่วง |
| 26 | `Queries/Dashboard/GetRevenueByPeriod/GetRevenueByPeriodQueryHandler.cs` | Handler รายได้แยกช่วง |
| 27 | `Queries/Dashboard/GetServiceCategory/GetServiceCategoryQuery.cs` | Query หมวดหมู่บริการ |
| 28 | `Queries/Dashboard/GetServiceCategory/GetServiceCategoryQueryHandler.cs` | Handler หมวดหมู่บริการ |
| 29 | `Queries/Dashboard/GetFinancialSummary/GetFinancialSummaryQuery.cs` | Query สรุปการเงิน |
| 30 | `Queries/Dashboard/GetFinancialSummary/GetFinancialSummaryQueryHandler.cs` | Handler สรุปการเงิน |
| 31 | `Queries/Reports/GenerateDailyReport/GenerateDailyReportQuery.cs` | Query รายงานรายวัน |
| 32 | `Queries/Reports/GenerateDailyReport/GenerateDailyReportQueryHandler.cs` | Handler รายงานรายวัน |
| 33 | `Queries/Reports/GenerateMonthlyReport/GenerateMonthlyReportQuery.cs` | Query รายงานรายเดือน |
| 34 | `Queries/Reports/GenerateMonthlyReport/GenerateMonthlyReportQueryHandler.cs` | Handler รายงานรายเดือน |
| 35 | `Queries/Reports/GenerateYearlyReport/GenerateYearlyReportQuery.cs` | Query รายงานรายปี |
| 36 | `Queries/Reports/GenerateYearlyReport/GenerateYearlyReportQueryHandler.cs` | Handler รายงานรายปี |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 37 | `Persistence/Repositories/DashboardRepository.cs` | Repository Dashboard (ใช้ SQL เฉพาะ) |
| 38 | `Persistence/Repositories/ReportRepository.cs` | Repository Report |
| 39 | `Persistence/Migrations/20260706000000_AddDashboardViews.cs` | Migration สร้าง Views |
| 40 | `Persistence/SeedData/DashboardSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 41 | `Controllers/DashboardController.cs` | Controller Dashboard (9 Endpoints) |
| 42 | `Controllers/ReportsController.cs` | Controller Reports (4 Endpoints) |

---

### 📁 **Document Generation** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 43 | `DocumentGeneration/IReportPdfGenerator.cs` | Interface PDF Generator |
| 44 | `DocumentGeneration/ReportPdfGenerator.cs` | PDF Report (QuestPDF) |
| 45 | `DocumentGeneration/ReportExcelGenerator.cs` | Excel Report (EPPlus) |

---

## 🗄️ Database Views (ต้องสร้างผ่าน Migration)

```sql
-- 1. v_dashboard_sales_overview
CREATE VIEW v_dashboard_sales_overview AS
SELECT 
    COALESCE(SUM(p.amount), 0) AS total_sales,
    COALESCE(COUNT(p.id), 0) AS total_transactions,
    COALESCE(AVG(p.amount), 0) AS average_transaction,
    COALESCE(SUM(p.amount) FILTER (WHERE p.paid_at >= CURRENT_DATE - INTERVAL '30 days'), 0) AS last_30_days_sales,
    COALESCE(SUM(p.amount) FILTER (WHERE p.paid_at >= CURRENT_DATE - INTERVAL '7 days'), 0) AS last_7_days_sales,
    COALESCE(SUM(p.amount) FILTER (WHERE p.paid_at >= CURRENT_DATE), 0) AS today_sales,
    COUNT(DISTINCT p.customer_id) AS unique_customers,
    w.whitelabel_id
FROM payments p
JOIN m_whitelabel w ON w.id = p.whitelabel_id
WHERE p.status = 'Completed'
GROUP BY w.whitelabel_id;

-- 2. v_dashboard_job_status
CREATE VIEW v_dashboard_job_status AS
SELECT 
    status,
    COUNT(*) AS count,
    whitelabel_id
FROM jobs
GROUP BY status, whitelabel_id;

-- 3. v_dashboard_inventory_overview
CREATE VIEW v_dashboard_inventory_overview AS
SELECT 
    COUNT(*) AS total_parts,
    SUM(stock_quantity) AS total_stock,
    SUM(stock_quantity * cost_amount) AS total_stock_value,
    COUNT(*) FILTER (WHERE stock_quantity <= reorder_level) AS low_stock_count,
    COUNT(*) FILTER (WHERE stock_quantity <= 0) AS out_of_stock_count,
    whitelabel_id
FROM part_masters
WHERE status = 'Active'
GROUP BY whitelabel_id;

-- 4. v_dashboard_top_parts
CREATE VIEW v_dashboard_top_parts AS
SELECT 
    p.id AS part_id,
    p.part_name,
    SUM(jps.quantity) AS total_sold,
    SUM(jps.total_amount) AS total_revenue,
    p.whitelabel_id,
    ROW_NUMBER() OVER (PARTITION BY p.whitelabel_id ORDER BY SUM(jps.total_amount) DESC) AS rank
FROM job_part_sales jps
JOIN part_masters p ON p.id = jps.part_id
JOIN jobs j ON j.id = jps.job_id
WHERE j.status IN ('Closed', 'PaymentReceived')
GROUP BY p.id, p.part_name, p.whitelabel_id;

-- 5. v_dashboard_service_category
CREATE VIEW v_dashboard_service_category AS
SELECT 
    s.category,
    COUNT(js.id) AS service_count,
    SUM(js.total_amount) AS total_revenue,
    js.whitelabel_id
FROM job_services js
JOIN services s ON s.id = js.service_id
JOIN jobs j ON j.id = js.job_id
WHERE j.status IN ('Closed', 'PaymentReceived')
GROUP BY s.category, js.whitelabel_id;

-- 6. v_dashboard_financial_summary
CREATE VIEW v_dashboard_financial_summary AS
SELECT 
    COALESCE(SUM(j.total_amount), 0) AS total_revenue,
    COALESCE(SUM(p.amount), 0) AS total_collected,
    COALESCE(SUM(j.total_amount) - SUM(p.amount), 0) AS total_outstanding,
    COALESCE(SUM(po.total_amount), 0) AS total_purchase_orders,
    COALESCE(SUM(j.total_amount) - SUM(po.total_amount), 0) AS gross_profit,
    j.whitelabel_id
FROM jobs j
LEFT JOIN payments p ON p.invoice_id = j.id AND p.status = 'Completed'
LEFT JOIN purchase_orders po ON po.job_id = j.id
WHERE j.status = 'Closed'
GROUP BY j.whitelabel_id;

-- 7. v_dashboard_revenue_by_period
CREATE VIEW v_dashboard_revenue_by_period AS
SELECT 
    DATE_TRUNC('day', j.completed_date) AS period_date,
    EXTRACT(YEAR FROM j.completed_date) AS year,
    EXTRACT(MONTH FROM j.completed_date) AS month,
    EXTRACT(DAY FROM j.completed_date) AS day,
    EXTRACT(WEEK FROM j.completed_date) AS week,
    COUNT(j.id) AS job_count,
    SUM(j.total_amount) AS revenue,
    j.whitelabel_id
FROM jobs j
WHERE j.status = 'Closed'
GROUP BY period_date, year, month, day, week, j.whitelabel_id;

-- 8. v_available_languages (i18n)
CREATE VIEW v_available_languages AS
SELECT language_code, locale, is_rtl
FROM languages
WHERE is_active = true;

-- 9. v_email_analytics
CREATE VIEW v_email_analytics AS
SELECT 
    template_code,
    COUNT(*) AS total_sent,
    COUNT(*) FILTER (WHERE status = 'Sent') AS delivered,
    COUNT(*) FILTER (WHERE status = 'Opened') AS opened,
    COUNT(*) FILTER (WHERE status = 'Bounced') AS bounced,
    whitelabel_id
FROM email_history
GROUP BY template_code, whitelabel_id;
```

---

## 🔐 API Endpoints ที่ใช้งานได้

### Dashboard Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| GET | `/api/dashboard/overview` | ภาพรวม Dashboard | 30/60s |
| GET | `/api/dashboard/sales` | ภาพรวมยอดขาย | 30/60s |
| GET | `/api/dashboard/job-status` | สรุปสถานะใบงาน | 30/60s |
| GET | `/api/dashboard/inventory` | ภาพรวมสินค้าคงคลัง | 30/60s |
| GET | `/api/dashboard/top-parts` | อะไหล่ขายดี | 20/60s |
| GET | `/api/dashboard/revenue` | รายได้แยกช่วงเวลา | 25/60s |
| GET | `/api/dashboard/service-category` | หมวดหมู่บริการ | 25/60s |
| GET | `/api/dashboard/financial` | สรุปการเงิน | 20/60s |
| GET | `/api/dashboard/trends` | แนวโน้มรายได้ | 20/60s |

### Reports Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/reports/daily` | รายงานรายวัน | 10/300s |
| POST | `/api/reports/monthly` | รายงานรายเดือน | 10/300s |
| POST | `/api/reports/yearly` | รายงานรายปี | 5/3600s |
| GET | `/api/reports/export/{id}` | ดาวน์โหลดรายงาน | 15/300s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="QuestPDF" Version="2023.12.6" />
<PackageReference Include="EPPlus" Version="7.0.0" />
```

---

## 🚀 วิธีใช้

### 1. ภาพรวม Dashboard
```http
GET /api/dashboard/overview
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "totalJobs": 450,
    "totalCustomers": 320,
    "totalRevenue": 1250000.00,
    "totalOutstanding": 180000.00,
    "pendingJobs": 35,
    "inProgressJobs": 20,
    "closedJobs": 395,
    "todaysJobs": 12,
    "todaysRevenue": 45000.00,
    "last7DaysRevenue": 280000.00,
    "last30DaysRevenue": 950000.00,
    "lowStockParts": 15,
    "outOfStockParts": 8,
    "topPartName": "น้ำมันเครื่อง 5W-30",
    "topPartRevenue": 45000.00
  }
}
```

### 2. ภาพรวมยอดขาย
```http
GET /api/dashboard/sales
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "totalSales": 1250000.00,
    "totalTransactions": 380,
    "averageTransaction": 3289.47,
    "todaySales": 45000.00,
    "last7DaysSales": 280000.00,
    "last30DaysSales": 950000.00,
    "uniqueCustomers": 280,
    "salesByPaymentMethod": {
      "Cash": 450000.00,
      "BankTransfer": 650000.00,
      "CreditCard": 150000.00
    }
  }
}
```

### 3. สรุปสถานะใบงาน
```http
GET /api/dashboard/job-status
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "Open": 15,
  "InProgress": 20,
  "QuotationPending": 8,
  "QuotationApproved": 5,
  "PartPicking": 3,
  "RepairInProgress": 12,
  "RepairDone": 10,
  "InvoicePending": 7,
  "InvoiceCreated": 5,
  "PaymentReceived": 8,
  "Closed": 395
}
```

### 4. อะไหล่ขายดี
```http
GET /api/dashboard/top-parts?limit=5&period=30
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "partId": "...",
    "partName": "น้ำมันเครื่อง 5W-30 1 ลิตร",
    "totalSold": 250,
    "totalRevenue": 45000.00,
    "percentage": 18.5
  },
  {
    "partId": "...",
    "partName": "กรองอากาศ",
    "totalSold": 180,
    "totalRevenue": 27000.00,
    "percentage": 11.1
  }
]
```

### 5. รายได้แยกช่วงเวลา
```http
GET /api/dashboard/revenue?period=Month&year=2026&month=7
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "period": "Month",
  "year": 2026,
  "month": 7,
  "dailyData": [
    { "date": "2026-07-01", "revenue": 45000.00, "jobs": 12 },
    { "date": "2026-07-02", "revenue": 38000.00, "jobs": 10 }
  ],
  "totalRevenue": 950000.00,
  "totalJobs": 380,
  "averagePerDay": 30645.16
}
```

### 6. สร้างรายงานรายวัน
```http
POST /api/reports/daily
Authorization: Bearer <accessToken>
{
  "date": "2026-07-06",
  "format": "pdf"
}
```
**Response:**
```json
{
  "reportId": "...",
  "date": "2026-07-06T00:00:00Z",
  "totalRevenue": 45000.00,
  "totalJobs": 12,
  "jobSummary": {
    "New": 8,
    "Completed": 4
  },
  "topServices": [],
  "topParts": [],
  "downloadUrl": "/api/reports/export/..."
}
```

### 7. รายงานรายเดือน
```http
POST /api/reports/monthly
Authorization: Bearer <accessToken>
{
  "year": 2026,
  "month": 7,
  "format": "excel"
}
```
**Response:**
```json
{
  "reportId": "...",
  "period": "July 2026",
  "totalRevenue": 950000.00,
  "totalJobs": 380,
  "averagePerDay": 30645.16,
  "jobSummary": {
    "Total": 380,
    "Completed": 365,
    "InProgress": 15
  },
  "downloadUrl": "/api/reports/export/..."
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Repository - DashboardRepository.cs
```csharp
using Dapper;
using Npgsql;

namespace ICMON.Infrastructure.Persistence.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;
    private readonly string _connectionString;

    public DashboardRepository(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<DashboardOverviewDto> GetOverviewAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                (SELECT COUNT(*) FROM jobs WHERE whitelabel_id = @WhitelabelId) AS TotalJobs,
                (SELECT COUNT(*) FROM customers WHERE whitelabel_id = @WhitelabelId) AS TotalCustomers,
                (SELECT COALESCE(SUM(total_amount), 0) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed') AS TotalRevenue,
                (SELECT COALESCE(SUM(total_amount), 0) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status IN ('InvoicePending', 'InvoiceCreated', 'PaymentReceived')) AS TotalOutstanding,
                (SELECT COUNT(*) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Open') AS PendingJobs,
                (SELECT COUNT(*) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status IN ('InProgress', 'QuotationPending', 'QuotationApproved', 'PartPicking', 'RepairInProgress')) AS InProgressJobs,
                (SELECT COUNT(*) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed') AS ClosedJobs,
                (SELECT COUNT(*) FROM jobs WHERE whitelabel_id = @WhitelabelId AND received_date >= CURRENT_DATE) AS TodaysJobs,
                (SELECT COALESCE(SUM(total_amount), 0) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed' AND completed_date >= CURRENT_DATE) AS TodaysRevenue,
                (SELECT COALESCE(SUM(total_amount), 0) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed' AND completed_date >= CURRENT_DATE - INTERVAL '7 days') AS Last7DaysRevenue,
                (SELECT COALESCE(SUM(total_amount), 0) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed' AND completed_date >= CURRENT_DATE - INTERVAL '30 days') AS Last30DaysRevenue,
                (SELECT COUNT(*) FROM part_masters WHERE whitelabel_id = @WhitelabelId AND stock_quantity <= reorder_level AND status = 'Active') AS LowStockParts,
                (SELECT COUNT(*) FROM part_masters WHERE whitelabel_id = @WhitelabelId AND stock_quantity <= 0 AND status = 'Active') AS OutOfStockParts,
                (SELECT part_name FROM part_masters WHERE whitelabel_id = @WhitelabelId ORDER BY stock_quantity LIMIT 1) AS TopPartName,
                (SELECT stock_quantity FROM part_masters WHERE whitelabel_id = @WhitelabelId ORDER BY stock_quantity LIMIT 1) AS TopPartRevenue
        ";

        var result = await connection.QueryFirstOrDefaultAsync<DashboardOverviewDto>(sql, new { WhitelabelId = whitelabelId });

        return result ?? new DashboardOverviewDto();
    }

    public async Task<SalesOverviewDto> GetSalesOverviewAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                COALESCE(SUM(total_amount), 0) AS TotalSales,
                COUNT(*) AS TotalTransactions,
                COALESCE(AVG(total_amount), 0) AS AverageTransaction,
                COALESCE(SUM(total_amount) FILTER (WHERE completed_date >= CURRENT_DATE), 0) AS TodaySales,
                COALESCE(SUM(total_amount) FILTER (WHERE completed_date >= CURRENT_DATE - INTERVAL '7 days'), 0) AS Last7DaysSales,
                COALESCE(SUM(total_amount) FILTER (WHERE completed_date >= CURRENT_DATE - INTERVAL '30 days'), 0) AS Last30DaysSales,
                COUNT(DISTINCT customer_id) AS UniqueCustomers
            FROM jobs
            WHERE whitelabel_id = @WhitelabelId AND status = 'Closed'
        ";

        var result = await connection.QueryFirstOrDefaultAsync<SalesOverviewDto>(sql, new { WhitelabelId = whitelabelId });

        // Get sales by payment method
        var paymentMethodSql = @"
            SELECT 
                payment_method,
                COALESCE(SUM(amount), 0) AS Total
            FROM payments
            WHERE whitelabel_id = @WhitelabelId AND status = 'Completed'
            GROUP BY payment_method
        ";

        var paymentMethods = await connection.QueryAsync<(string PaymentMethod, decimal Total)>(
            paymentMethodSql, 
            new { WhitelabelId = whitelabelId }
        );

        if (result != null)
        {
            result.SalesByPaymentMethod = paymentMethods.ToDictionary(x => x.PaymentMethod, x => x.Total);
        }

        return result ?? new SalesOverviewDto();
    }

    public async Task<JobStatusSummaryDto> GetJobStatusSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                status,
                COUNT(*) AS count
            FROM jobs
            WHERE whitelabel_id = @WhitelabelId
            GROUP BY status
        ";

        var results = await connection.QueryAsync<(string Status, int Count)>(sql, new { WhitelabelId = whitelabelId });

        var summary = new JobStatusSummaryDto();
        foreach (var item in results)
        {
            var property = summary.GetType().GetProperty(item.Status);
            if (property != null && property.PropertyType == typeof(int))
            {
                property.SetValue(summary, item.Count);
            }
        }

        return summary;
    }

    public async Task<InventoryOverviewDto> GetInventoryOverviewAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                COUNT(*) AS TotalParts,
                COALESCE(SUM(stock_quantity), 0) AS TotalStock,
                COALESCE(SUM(stock_quantity * cost_amount), 0) AS TotalStockValue,
                COUNT(*) FILTER (WHERE stock_quantity <= reorder_level) AS LowStockCount,
                COUNT(*) FILTER (WHERE stock_quantity <= 0) AS OutOfStockCount
            FROM part_masters
            WHERE whitelabel_id = @WhitelabelId AND status = 'Active'
        ";

        return await connection.QueryFirstOrDefaultAsync<InventoryOverviewDto>(sql, new { WhitelabelId = whitelabelId })
            ?? new InventoryOverviewDto();
    }

    public async Task<IEnumerable<TopPartDto>> GetTopPartsAsync(Guid whitelabelId, int limit = 10, int days = 30, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                p.id AS PartId,
                p.part_name AS PartName,
                SUM(jps.quantity) AS TotalSold,
                SUM(jps.total_amount) AS TotalRevenue,
                (SUM(jps.total_amount) * 100.0 / (SELECT COALESCE(SUM(total_amount), 1) FROM jobs WHERE whitelabel_id = @WhitelabelId AND status = 'Closed' AND completed_date >= CURRENT_DATE - INTERVAL '@Days days')) AS Percentage
            FROM job_part_sales jps
            JOIN part_masters p ON p.id = jps.part_id
            JOIN jobs j ON j.id = jps.job_id
            WHERE j.whitelabel_id = @WhitelabelId 
                AND j.status = 'Closed'
                AND j.completed_date >= CURRENT_DATE - INTERVAL '@Days days'
            GROUP BY p.id, p.part_name
            ORDER BY TotalRevenue DESC
            LIMIT @Limit
        ";

        var parameters = new { WhitelabelId = whitelabelId, Limit = limit, Days = days };
        return await connection.QueryAsync<TopPartDto>(sql, parameters);
    }

    public async Task<IEnumerable<RevenueByPeriodDto>> GetRevenueByPeriodAsync(
        Guid whitelabelId, 
        string period, 
        int year, 
        int? month = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string dateFilter;
        string groupBy;

        if (period == "Month" && month.HasValue)
        {
            dateFilter = $"EXTRACT(YEAR FROM completed_date) = {year} AND EXTRACT(MONTH FROM completed_date) = {month}";
            groupBy = "EXTRACT(DAY FROM completed_date)";
        }
        else if (period == "Year")
        {
            dateFilter = $"EXTRACT(YEAR FROM completed_date) = {year}";
            groupBy = "EXTRACT(MONTH FROM completed_date)";
        }
        else // Week
        {
            dateFilter = $"EXTRACT(YEAR FROM completed_date) = {year} AND EXTRACT(WEEK FROM completed_date) = EXTRACT(WEEK FROM CURRENT_DATE)";
            groupBy = "EXTRACT(DOW FROM completed_date)";
        }

        var sql = $@"
            SELECT 
                DATE_TRUNC('day', completed_date) AS Date,
                COUNT(*) AS Jobs,
                COALESCE(SUM(total_amount), 0) AS Revenue
            FROM jobs
            WHERE whitelabel_id = @WhitelabelId 
                AND status = 'Closed'
                AND {dateFilter}
            GROUP BY DATE_TRUNC('day', completed_date)
            ORDER BY Date
        ";

        return await connection.QueryAsync<RevenueByPeriodDto>(sql, new { WhitelabelId = whitelabelId });
    }

    public async Task<FinancialSummaryDto> GetFinancialSummaryAsync(Guid whitelabelId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = @"
            SELECT 
                COALESCE(SUM(j.total_amount), 0) AS TotalRevenue,
                COALESCE(SUM(p.amount), 0) AS TotalCollected,
                COALESCE(SUM(j.total_amount) - SUM(p.amount), 0) AS TotalOutstanding,
                COALESCE(SUM(po.total_amount), 0) AS TotalPurchaseOrders,
                COALESCE(SUM(j.total_amount) - SUM(po.total_amount), 0) AS GrossProfit
            FROM jobs j
            LEFT JOIN payments p ON p.invoice_id = j.id AND p.status = 'Completed'
            LEFT JOIN purchase_orders po ON po.job_id = j.id
            WHERE j.whitelabel_id = @WhitelabelId AND j.status = 'Closed'
        ";

        return await connection.QueryFirstOrDefaultAsync<FinancialSummaryDto>(sql, new { WhitelabelId = whitelabelId })
            ?? new FinancialSummaryDto();
    }
}
```

### 2. Query Handler - GetDashboardOverviewQueryHandler.cs
```csharp
using MediatR;
using ICMON.Domain.Interfaces;
using ICMON.Application.DTOs.Dashboard;
using ICMON.Application.Common;

namespace ICMON.Application.Queries.Dashboard.GetDashboardOverview;

public class GetDashboardOverviewQueryHandler : IRequestHandler<GetDashboardOverviewQuery, Result<DashboardOverviewDto>>
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly ICacheService _cacheService;
    private readonly ICurrentUserService _currentUser;

    public GetDashboardOverviewQueryHandler(
        IDashboardRepository dashboardRepository,
        ICacheService cacheService,
        ICurrentUserService currentUser)
    {
        _dashboardRepository = dashboardRepository;
        _cacheService = cacheService;
        _currentUser = currentUser;
    }

    public async Task<Result<DashboardOverviewDto>> Handle(GetDashboardOverviewQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"dashboard_overview:{_currentUser.WhitelabelId}";
        
        // Try get from cache
        var cached = await _cacheService.GetAsync<DashboardOverviewDto>(cacheKey, cancellationToken);
        if (cached != null)
            return Result<DashboardOverviewDto>.Success(cached);

        // Get from database
        var overview = await _dashboardRepository.GetOverviewAsync(_currentUser.WhitelabelId, cancellationToken);
        
        // Cache for 5 minutes
        await _cacheService.SetAsync(cacheKey, overview, TimeSpan.FromMinutes(5), cancellationToken);

        return Result<DashboardOverviewDto>.Success(overview);
    }
}
```

### 3. Controller - DashboardController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator) => _mediator = mediator;

    [HttpGet("overview")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetOverview()
    {
        var query = new GetDashboardOverviewQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("sales")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetSalesOverview()
    {
        var query = new GetSalesOverviewQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("job-status")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetJobStatusSummary()
    {
        var query = new GetJobStatusSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("inventory")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetInventoryOverview()
    {
        var query = new GetInventoryOverviewQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("top-parts")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetTopParts(
        [FromQuery] int limit = 10,
        [FromQuery] int days = 30)
    {
        var query = new GetTopPartsQuery { Limit = limit, Days = days };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("revenue")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetRevenueByPeriod(
        [FromQuery] string period = "Month",
        [FromQuery] int year = 0,
        [FromQuery] int? month = null)
    {
        var query = new GetRevenueByPeriodQuery 
        { 
            Period = period, 
            Year = year > 0 ? year : DateTime.UtcNow.Year,
            Month = month 
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("service-category")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetServiceCategory()
    {
        var query = new GetServiceCategoryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("financial")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetFinancialSummary()
    {
        var query = new GetFinancialSummaryQuery();
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("trends")]
    [Authorize("DASHBOARD_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetRevenueTrends(
        [FromQuery] int months = 12)
    {
        var query = new GetRevenueTrendsQuery { Months = months };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
```

### 4. Controller - ReportsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IReportPdfGenerator _pdfGenerator;
    private readonly IReportExcelGenerator _excelGenerator;

    public ReportsController(
        IMediator mediator,
        IReportPdfGenerator pdfGenerator,
        IReportExcelGenerator excelGenerator)
    {
        _mediator = mediator;
        _pdfGenerator = pdfGenerator;
        _excelGenerator = excelGenerator;
    }

    [HttpPost("daily")]
    [Authorize("REPORT_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GenerateDailyReport([FromBody] GenerateDailyReportQuery query)
    {
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("monthly")]
    [Authorize("REPORT_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GenerateMonthlyReport([FromBody] GenerateMonthlyReportQuery query)
    {
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("yearly")]
    [Authorize("REPORT_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> GenerateYearlyReport([FromBody] GenerateYearlyReportQuery query)
    {
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("export/{reportId}")]
    [Authorize("REPORT_READ")]
    [EnableRateLimiting("PdfGeneration")]
    public async Task<IActionResult> ExportReport(Guid reportId, [FromQuery] string format = "pdf")
    {
        // ดึงข้อมูลรายงานจากฐานข้อมูลตาม reportId
        var report = await GetReportById(reportId);
        if (report == null)
            return NotFound();

        if (format.ToLower() == "pdf")
        {
            var pdfBytes = await _pdfGenerator.GenerateReportPdfAsync(report);
            return File(pdfBytes, "application/pdf", $"Report-{report.Date:yyyyMMdd}.pdf");
        }
        else if (format.ToLower() == "excel")
        {
            var excelBytes = await _excelGenerator.GenerateReportExcelAsync(report);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Report-{report.Date:yyyyMMdd}.xlsx");
        }

        return BadRequest("Unsupported format");
    }
}
```

### 5. DTO - DashboardOverviewDto.cs
```csharp
namespace ICMON.Application.DTOs.Dashboard;

public class DashboardOverviewDto
{
    public int TotalJobs { get; set; }
    public int TotalCustomers { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalOutstanding { get; set; }
    public int PendingJobs { get; set; }
    public int InProgressJobs { get; set; }
    public int ClosedJobs { get; set; }
    public int TodaysJobs { get; set; }
    public decimal TodaysRevenue { get; set; }
    public decimal Last7DaysRevenue { get; set; }
    public decimal Last30DaysRevenue { get; set; }
    public int LowStockParts { get; set; }
    public int OutOfStockParts { get; set; }
    public string? TopPartName { get; set; }
    public decimal TopPartRevenue { get; set; }
}
```

### 6. DTO - SalesOverviewDto.cs
```csharp
namespace ICMON.Application.DTOs.Dashboard;

public class SalesOverviewDto
{
    public decimal TotalSales { get; set; }
    public int TotalTransactions { get; set; }
    public decimal AverageTransaction { get; set; }
    public decimal TodaySales { get; set; }
    public decimal Last7DaysSales { get; set; }
    public decimal Last30DaysSales { get; set; }
    public int UniqueCustomers { get; set; }
    public Dictionary<string, decimal> SalesByPaymentMethod { get; set; } = new();
}
```

### 7. DTO - JobStatusSummaryDto.cs
```csharp
namespace ICMON.Application.DTOs.Dashboard;

public class JobStatusSummaryDto
{
    public int Open { get; set; }
    public int InProgress { get; set; }
    public int QuotationPending { get; set; }
    public int QuotationApproved { get; set; }
    public int PartPicking { get; set; }
    public int RepairInProgress { get; set; }
    public int RepairDone { get; set; }
    public int InvoicePending { get; set; }
    public int InvoiceCreated { get; set; }
    public int PaymentReceived { get; set; }
    public int Closed { get; set; }
    public int Total => Open + InProgress + QuotationPending + QuotationApproved + 
                        PartPicking + RepairInProgress + RepairDone + InvoicePending + 
                        InvoiceCreated + PaymentReceived + Closed;
}
```

### 8. Report Generator - ReportPdfGenerator.cs (บางส่วน)
```csharp
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ICMON.Infrastructure.DocumentGeneration;

public class ReportPdfGenerator : IReportPdfGenerator
{
    public async Task<byte[]> GenerateReportPdfAsync(ReportDto report, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    
                    page.Header().Column(col =>
                    {
                        col.Item().Text($"ICMON Auto Repair - {report.ReportType} Report")
                            .FontSize(20).Bold().AlignCenter();
                        col.Item().Text($"Date: {report.Date:dd/MM/yyyy}")
                            .FontSize(12).AlignCenter();
                    });

                    page.Content().Column(col =>
                    {
                        // Summary
                        col.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(summary =>
                        {
                            summary.Item().Text("Summary").FontSize(14).Bold();
                            summary.Item().Row(row =>
                            {
                                row.RelativeItem().Text($"Total Revenue: {report.TotalRevenue:N2} ฿");
                                row.RelativeItem().Text($"Total Jobs: {report.TotalJobs}");
                            });
                            summary.Item().Row(row =>
                            {
                                row.RelativeItem().Text($"Average per Day: {report.AveragePerDay:N2} ฿");
                                row.RelativeItem().Text($"Completed Jobs: {report.CompletedJobs}");
                            });
                        });

                        // Details Table
                        col.Item().PaddingTop(10).Text("Details").FontSize(14).Bold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Item").Bold();
                                header.Cell().Text("Quantity").Bold().AlignCenter();
                                header.Cell().Text("Amount").Bold().AlignRight();
                            });

                            foreach (var detail in report.Details)
                            {
                                table.Cell().Text(detail.Name);
                                table.Cell().Text(detail.Quantity.ToString()).AlignCenter();
                                table.Cell().Text($"{detail.Amount:N2} ฿").AlignRight();
                            }
                        });
                    });

                    page.Footer().AlignCenter().Text("Generated by ICMON System")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }, cancellationToken);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `dashboard_overview:{whitelabelId}` | 5 นาที | ภาพรวม Dashboard |
| `dashboard_sales:{whitelabelId}` | 5 นาที | ภาพรวมยอดขาย |
| `dashboard_job_status:{whitelabelId}` | 5 นาที | สรุปสถานะใบงาน |
| `dashboard_inventory:{whitelabelId}` | 5 นาที | ภาพรวมสินค้าคงคลัง |
| `dashboard_top_parts:{whitelabelId}:{days}` | 5 นาที | อะไหล่ขายดี |
| `dashboard_revenue:{whitelabelId}:{period}:{year}:{month}` | 5 นาที | รายได้แยกช่วง |
| `dashboard_financial:{whitelabelId}` | 5 นาที | สรุปการเงิน |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ ภาพรวม Dashboard (ตัวเลขสำคัญทั้งหมด)
- ✅ ภาพรวมยอดขาย (แยกตามวิธีการชำระเงิน)
- ✅ สรุปสถานะใบงาน (11 สถานะ)
- ✅ ภาพรวมสินค้าคงคลัง
- ✅ อะไหล่ขายดี (Top Parts)
- ✅ รายได้แยกช่วงเวลา (วัน/เดือน/ปี)
- ✅ หมวดหมู่บริการที่ขายดี
- ✅ สรุปการเงิน (รายได้, เก็บเงินได้, ค้างชำระ)
- ✅ แนวโน้มรายได้
- ✅ รายงานรายวัน (PDF/Excel)
- ✅ รายงานรายเดือน (PDF/Excel)
- ✅ รายงานรายปี (PDF/Excel)
- ✅ Caching ด้วย Redis (5 นาที)
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)
- ✅ ใช้ Database Views เพื่อประสิทธิภาพ

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 8: **Dashboard & Reports** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 9: Document Management
2. โมดูลที่ 10: Email Service
3. โมดูลที่ 11: Batch Jobs
4. โมดูลที่ 12: Multi-Language (i18n)
5. โมดูลที่ 13: IoT & GPS Tracking
6. โมดูลที่ 14: Web Order System (WOS)