# Module 14: Web Order System (WOS)

## Overview
ระบบสั่งซื้อออนไลน์ แคตตาล็อกสินค้า ตะกร้าสินค้า สั่งซื้อ โปรโมชัน

## Database Tables

| Table | Description |
|-------|-------------|
| `m_catalogue_category` | หมวดหมู่สินค้าในแคตตาล็อก |
| `m_catalogue_item` | สินค้าในแคตตาล็อก |
| `m_sales_price` | ราคาขาย (รองรับหลาย Tier) |
| `m_promotion` | โปรโมชัน/ส่วนลด |
| `t_cart` | ตะกร้าสินค้า (Session-based) |
| `t_cart_item` | รายการในตะกร้า |
| `t_web_order` | ออเดอร์ออนไลน์ |
| `t_web_order_item` | รายการออเดอร์ |
| `t_web_order_status_history` | ประวัติสถานะออเดอร์ |

## Order Status Flow

```
PENDING → CONFIRMED → PROCESSING → SHIPPED → DELIVERED
```

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| GET | `/api/wos/catalogue` | 100/60s |
| GET | `/api/wos/catalogue/{id}` | 200/60s |
| GET | `/api/wos/catalogue/search` | 80/60s |
| GET | `/api/wos/catalogue/category/{categoryId}` | 80/60s |
| POST | `/api/wos/cart/add` | 50/60s |
| PUT | `/api/wos/cart/update` | 50/60s |
| DELETE | `/api/wos/cart/remove/{itemId}` | 30/60s |
| GET | `/api/wos/cart` | 100/60s |
| POST | `/api/wos/cart/promotion` | 20/60s |
| POST | `/api/wos/orders` | 20/60s |
| GET | `/api/wos/orders/{id}` | 100/60s |
| GET | `/api/wos/orders/list` | 50/60s |
| PUT | `/api/wos/orders/{id}/status` | 20/60s |
| POST | `/api/wos/orders/{id}/cancel` | 10/3600s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `catalogue:{itemId}` | 1 hr |
| `catalogue_code:{itemCode}` | 1 hr |
| `catalogue_category:{categoryId}` | 1 hr |
| `cart:{cartId}` | 30 min |
| `orders:{orderId}` | 30 min |

## Key Features

- ✅ Product catalogue with categories
- ✅ Multiple price tiers
- ✅ Shopping cart management
- ✅ Promotion and discount codes
- ✅ Order placement and tracking
- ✅ Order history for customers
- ✅ Inventory integration
- ✅ Email notification on order status change
