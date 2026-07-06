# Module 10: Email Service

## Overview
ระบบส่งอีเมล เทมเพลตอีเมล ส่งจำนวนมาก ประวัติการส่ง คิวอีเมล

## Database Tables

| Table | Description |
|-------|-------------|
| `m_email_template` | เทมเพลตอีเมล (HTML, Text) |
| `t_email_history` | ประวัติการส่งอีเมล |
| `t_email_queue` | คิวอีเมล (สำหรับ Async) |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/email/send` | 20/60s |
| POST | `/api/email/send-template` | 25/60s |
| POST | `/api/email/bulk` | 5/300s |
| GET | `/api/email/status/{emailId}` | 50/60s |
| POST | `/api/email/resend/{emailId}` | 10/3600s |
| GET | `/api/email/history` | 30/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `email_templates:{templateCode}:{lang}` | 2 hr |

## Key Features

- ✅ SMTP email sending with attachments
- ✅ HTML template engine with variables
- ✅ Bulk email with queue processing
- ✅ Email history and tracking
- ✅ Retry mechanism for failed emails
- ✅ Multi-language email templates
- ✅ Async processing via Hangfire
- ✅ Integration with modules (Quotation, Invoice, Promotion)
