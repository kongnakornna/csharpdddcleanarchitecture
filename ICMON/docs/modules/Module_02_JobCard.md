# Module 2: Job Card Management

## Overview
ระบบจัดการใบงานซ่อม (Job Card) ตั้งแต่รับรถเข้าซ่อมจนถึงปิดงาน ครอบคลุมการบันทึกอาการรถ รายการบริการ อะไหล่ที่ใช้ และประวัติการเปลี่ยนสถานะ

## Database Tables

| Table | Description |
|-------|-------------|
| `t_job` | ใบงานหลัก (job_no, customer_id, car_id, mechanic_id, status) |
| `t_job_service` | รายการบริการในใบงาน |
| `t_job_part_sales` | อะไหล่ที่ขายในใบงาน |
| `t_job_symptom` | อาการรถ |
| `t_job_diag_code` | รหัสข้อผิดพลาด (OBD2) |
| `t_job_status_history` | ประวัติการเปลี่ยนสถานะ |

## Job Status Flow

```
OPEN → IN_PROGRESS → QUOTATION_PENDING → QUOTATION_APPROVED
→ PART_PICKING → REPAIR_IN_PROGRESS → REPAIR_DONE
→ INVOICE_PENDING → INVOICE_CREATED → PAYMENT_RECEIVED → CLOSED
```

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/jobs/create` | 30/60s |
| GET | `/api/jobs/list` | 50/60s |
| GET | `/api/jobs/{id}` | 100/60s |
| PUT | `/api/jobs/{id}/status` | 60/60s |
| GET | `/api/jobs/report/{id}` | 20/300s |
| POST | `/api/jobs/{id}/services` | 30/60s |
| POST | `/api/jobs/{id}/parts` | 30/60s |
| GET | `/api/jobs/status-summary` | 40/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `jobs:{jobId}` | 30 min |
| `job_status_summary:{whitelabelId}` | 5 min |

## Key Features

- ✅ Job creation with customer, car, mechanic assignment
- ✅ Service and part tracking
- ✅ Status workflow with transition validation
- ✅ OBD2 diagnostic trouble code recording
- ✅ Status history audit trail
- ✅ PDF report generation
- ✅ Domain events (JobCreated, JobStatusChanged)
