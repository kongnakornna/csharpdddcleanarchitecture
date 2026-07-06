# Module 9: Document Management

## Overview
ระบบจัดการเอกสาร เทมเพลตเอกสาร สร้าง PDF/Excel อัปโหลด ดาวน์โหลด OCR

## Database Tables

| Table | Description |
|-------|-------------|
| `m_document_template` | เทมเพลตเอกสาร (JasperReports .jrxml) |
| `t_document` | เอกสารที่สร้างแล้ว (PDF, Excel) |
| `t_document_history` | ประวัติเอกสาร |
| `t_ocr_result` | ผลลัพธ์ OCR |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/documents/generate` | 15/300s |
| POST | `/api/documents/upload` | 20/300s |
| GET | `/api/documents/{id}/download` | 30/60s |
| GET | `/api/documents/{id}` | 50/60s |
| DELETE | `/api/documents/{id}` | 10/3600s |
| POST | `/api/documents/ocr` | 10/60s |
| POST | `/api/templates/upload` | 10/300s |
| GET | `/api/templates` | 30/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `documents:{documentId}` | 1 hr |
| `document_ref:{refType}:{refId}` | 1 hr |
| `templates:{templateCode}` | 2 hr |

## Key Features

- ✅ Document generation from templates (PDF, Excel)
- ✅ File upload with validation (size, type)
- ✅ Secure download with access control
- ✅ OCR processing for scanned documents
- ✅ Template management
- ✅ Document history tracking
- ✅ Integration with modules (Job, Quotation, PO, Receipt)
