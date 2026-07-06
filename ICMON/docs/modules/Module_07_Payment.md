# Module 7: Payment Management

## Overview
ระบบจัดการการชำระเงิน ใบแจ้งหนี้ ใบเสร็จรับเงิน ยอดคงค้างและการคืนเงิน

## Database Tables

| Table | Description |
|-------|-------------|
| `m_payment_method` | วิธีการชำระเงิน (CASH, BANK_TRANSFER, CREDIT_CARD) |
| `t_invoice` | ใบแจ้งหนี้ |
| `t_invoice_adjustment` | การปรับปรุงใบแจ้งหนี้ |
| `t_payment` | การชำระเงิน |
| `t_receipt` | ใบเสร็จรับเงิน |
| `t_payment_history` | ประวัติสถานะการชำระ |
| `t_outstanding_balance` | ยอดคงค้าง |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/payments/record` | 20/60s |
| GET | `/api/payments/invoice/{invoiceId}` | 60/60s |
| GET | `/api/payments/outstanding/{customerId}` | 40/60s |
| POST | `/api/payments/{id}/refund` | 10/3600s |
| GET | `/api/payments/{id}` | 60/60s |
| POST | `/api/invoices/create` | 20/60s |
| GET | `/api/invoices/{id}` | 80/60s |
| GET | `/api/receipts/{id}/pdf` | 15/300s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `payments:{paymentId}` | 1 hr |
| `payment_invoice:{invoiceId}` | 1 hr |
| `receipts:{receiptId}` | 1 hr |

## Key Features

- ✅ Invoice creation from completed Job
- ✅ Multiple payment methods support
- ✅ Partial payment handling
- ✅ Receipt generation (PDF)
- ✅ Refund processing
- ✅ Outstanding balance tracking
- ✅ Payment history audit trail
- ✅ Integration with Job status flow
