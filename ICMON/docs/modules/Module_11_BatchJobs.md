# Module 11: Batch Jobs

## Overview
ระบบงาน Batch อัตโนมัติ แจ้งเตือนรายวัน สร้างรายงาน อัปเดตสถานะ ซิงค์ข้อมูล

## Database Tables

| Table | Description |
|-------|-------------|
| `m_batch_job` | งาน Batch (job_code, cron_expression, enabled) |
| `t_batch_job_history` | ประวัติการรันงาน |

## Scheduled Jobs

| Job | Cron | Time | Description |
|-----|------|------|-------------|
| batch001 | `0 30 6 ? * *` | 06:30 | ส่งอีเมลแจ้งเตือนรายวัน |
| batch002 | `0 45 6 ? * *` | 06:45 | สร้างรายงานประจำวัน |
| batch003 | `0 30 6 ? * *` | 06:30 | อัปเดตสถานะงานค้าง |
| batch004 | `0 0 3 ? * *` | 03:00 | ล้างข้อมูล/ซิงค์ฐานข้อมูล |
| batch005 | `0 0/30 * * * ?` | Every 30 min | ซิงค์ข้อมูล Realtime |
| batch006 | `0 30 6 ? * *` | 06:30 | ส่งสรุปยอดขาย |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| GET | `/api/batch-jobs/list` | 20/60s |
| GET | `/api/batch-jobs/{jobCode}/status` | 30/60s |
| POST | `/api/batch-jobs/{jobCode}/trigger` | 5/3600s |
| GET | `/api/batch-jobs/{jobCode}/history` | 20/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `batch_jobs:{jobCode}` | 5 min |
| `batch_lock:{jobCode}` | 5 min |

## Key Features

- ✅ Job scheduling with cron expressions
- ✅ Manual trigger support
- ✅ Execution history tracking
- ✅ Success/failure logging
- ✅ Distributed lock via Redis
- ✅ Retry on failure
- ✅ Email notifications on failure
