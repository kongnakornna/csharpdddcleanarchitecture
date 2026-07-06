# Module 8: Dashboard & Reports

## Overview
ระบบ Dashboard และรายงาน สรุปยอดขาย สถานะงาน สินค้าคงคลัง อะไหล่ขายดี รายงานรายวัน/เดือน/ปี

## Database Views

| View | Description |
|------|-------------|
| `v_dashboard_sales_overview` | ภาพรวมยอดขาย |
| `v_dashboard_job_status` | สรุปสถานะใบงาน |
| `v_dashboard_inventory_overview` | ภาพรวมสินค้าคงคลัง |
| `v_dashboard_top_parts` | อะไหล่ขายดี |
| `v_dashboard_service_category` | สรุปตามประเภทบริการ |
| `v_dashboard_financial_summary` | สรุปการเงิน |
| `v_dashboard_revenue_by_period` | รายได้แยกช่วงเวลา |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| GET | `/api/dashboard/overview` | 30/60s |
| GET | `/api/dashboard/sales` | 30/60s |
| GET | `/api/dashboard/job-status` | 30/60s |
| GET | `/api/dashboard/inventory` | 30/60s |
| GET | `/api/dashboard/top-parts` | 20/60s |
| GET | `/api/dashboard/revenue` | 25/60s |
| POST | `/api/reports/daily` | 10/300s |
| POST | `/api/reports/monthly` | 10/300s |
| POST | `/api/reports/yearly` | 5/3600s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `dashboard_overview:{whitelabelId}` | 5 min |
| `sales_by_period:{whitelabelId}:{period}` | 5 min |
| `top_parts:{whitelabelId}` | 5 min |

## Key Features

- ✅ Real-time dashboard with key metrics
- ✅ Sales overview with charts
- ✅ Job status distribution
- ✅ Top-selling parts analysis
- ✅ Revenue tracking by period
- ✅ Automated daily/monthly/yearly reports
- ✅ PDF and Excel export
- ✅ Configurable date ranges
