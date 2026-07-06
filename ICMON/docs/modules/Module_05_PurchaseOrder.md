# Module 5: Purchase Order

## Overview
ระบบใบสั่งซื้ออะไหล่ สร้าง PO จาก Quotation หรือ Manual ส่งให้ Supplier รับสินค้าเข้าคลัง

## Database Tables

| Table | Description |
|-------|-------------|
| `t_purchase_order_header` | ใบสั่งซื้อ (po_no, supplier_id, status, total) |
| `t_purchase_order_detail` | รายการใบสั่งซื้อ |
| `t_purchase_order_status_history` | ประวัติสถานะ PO |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/purchase-orders/create` | 20/60s |
| POST | `/api/purchase-orders/from-quotation/{quotationId}` | 15/60s |
| GET | `/api/purchase-orders/{id}` | 80/60s |
| GET | `/api/purchase-orders/list` | 50/60s |
| POST | `/api/purchase-orders/{id}/send` | 10/60s |
| POST | `/api/purchase-orders/{id}/receive` | 15/60s |
| PUT | `/api/purchase-orders/{id}/status` | 20/60s |
| GET | `/api/purchase-orders/{id}/pdf` | 15/300s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `purchase_orders:{poId}` | 30 min |
| `po_quotation:{quotationId}` | 30 min |
| `po_suggestion:{jobId}` | 15 min |

## Key Features

- ✅ Create PO manually or from approved Quotation
- ✅ Multi-supplier support
- ✅ Status tracking (DRAFT → SENT → RECEIVED → CANCELLED)
- ✅ Partial receiving support
- ✅ PDF generation for supplier communication
- ✅ Auto-suggest parts based on job history
