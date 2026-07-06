# Module 6: Inventory Management

## Overview
ระบบจัดการสินค้าคงคลัง รับเข้า เบิกจ่าย ปรับปรุงสต็อก ตรวจนับ เตือนสินค้าใกล้หมด

## Database Tables

| Table | Description |
|-------|-------------|
| `m_part_master` | อะไหล่หลัก (part_code, part_name, stock_quantity, reorder_level) |
| `m_stock_location` | ตำแหน่งจัดเก็บ |
| `t_inventory` | การเคลื่อนไหวสินค้า (RECEIVE, ISSUE, ADJUSTMENT) |
| `t_inventory_adjustment_header/detail` | การปรับปรุงสต็อก |
| `t_stocktake_header/detail` | การตรวจนับสต็อก |
| `t_part_picking_request/detail` | คำขอเบิกอะไหล่ |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/inventory/receive` | 20/60s |
| POST | `/api/inventory/issue` | 30/60s |
| GET | `/api/inventory/part/{partId}` | 80/60s |
| POST | `/api/inventory/adjust` | 15/60s |
| POST | `/api/part-picking/create` | 30/60s |
| PUT | `/api/part-picking/{id}/confirm` | 20/60s |
| GET | `/api/parts/low-stock` | 30/60s |
| POST | `/api/stocktake/create` | 10/300s |
| PUT | `/api/stocktake/{id}/approve` | 10/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `parts:{partId}` | 1 hr |
| `part_code:{partCode}` | 1 hr |
| `stock_summary:{partId}` | 15 min |

## Key Features

- ✅ Part master data management
- ✅ Stock movement tracking (receive, issue, adjust)
- ✅ Reorder level alerts
- ✅ Stocktake (physical count) with adjustment
- ✅ Part picking request for job orders
- ✅ Multi-location stock management
- ✅ Barcode/QR code support
