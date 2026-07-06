# Module 3: Customer Management

## Overview
ระบบจัดการข้อมูลลูกค้าและรถยนต์ ค้นหาลูกค้าด้วยเบอร์โทร ค้นหารถด้วยทะเบียน ดูประวัติการซ่อมบำรุง

## Database Tables

| Table | Description |
|-------|-------------|
| `m_customer` | ข้อมูลลูกค้า (ชื่อ, เบอร์โทร, อีเมล, ที่อยู่, TIN) |
| `m_car` | ข้อมูลรถยนต์ (ทะเบียน, ยี่ห้อ, รุ่น, ปี, VIN) |
| `m_car_service_history` | ประวัติการซ่อมบำรุงของรถ |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/customers/create` | 20/60s |
| GET | `/api/customers/{id}` | 100/60s |
| GET | `/api/customers/phone/{phone}` | 60/60s |
| POST | `/api/customers/search` | 50/60s |
| PUT | `/api/customers/{id}` | 20/60s |
| POST | `/api/cars/create` | 30/60s |
| GET | `/api/cars/plate/{licensePlate}` | 60/60s |
| GET | `/api/customers/{customerId}/cars` | 60/60s |
| GET | `/api/cars/{carId}/history` | 40/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `customers:{customerId}` | 1 hr |
| `customer_phone:{phone}` | 1 hr |
| `cars:{carId}` | 1 hr |
| `car_plate:{licensePlate}` | 1 hr |

## Key Features

- ✅ Customer CRUD with address and contact info
- ✅ Car registration with VIN validation
- ✅ Search by phone, license plate, name
- ✅ Service history tracking per car
- ✅ Duplicate detection
- ✅ Multi-language support
