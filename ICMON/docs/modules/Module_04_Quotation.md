# Module 4: Quotation

## Overview
ระบบใบเสนอราคา สร้าง报价จาก Job Card เพิ่มบริการและอะไหล่ คำนวณภาษีและส่วนลด ส่ง PDF ให้ลูกค้า

## Database Tables

| Table | Description |
|-------|-------------|
| `t_quotation` | ใบเสนอราคา (quotation_no, job_id, status, subtotal, tax, total) |
| `t_quotation_part` | อะไหล่ในใบเสนอราคา |
| `t_quotation_service` | บริการในใบเสนอราคา |
| `t_quotation_status_history` | ประวัติการเปลี่ยนสถานะ |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/quotations/create` | 20/60s |
| GET | `/api/quotations/{id}` | 100/60s |
| GET | `/api/quotations/job/{jobId}` | 80/60s |
| PUT | `/api/quotations/{id}/approve` | 20/60s |
| PUT | `/api/quotations/{id}/reject` | 20/60s |
| PUT | `/api/quotations/{id}` | 20/60s |
| GET | `/api/quotations/{id}/pdf` | 15/300s |
| POST | `/api/quotations/{id}/email` | 10/300s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `quotations:{quotationId}` | 30 min |
| `quotation_job:{jobId}` | 30 min |

## Key Features

- ✅ Create quotation from Job Card
- ✅ Add services and parts with pricing
- ✅ Automatic tax and discount calculation
- ✅ Approval workflow
- ✅ PDF generation with company logo
- ✅ Email quotation to customer
- ✅ Status history tracking
