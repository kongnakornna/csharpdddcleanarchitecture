# Module 13: IoT & GPS Tracking

## Overview
ระบบ IoT และติดตามตำแหน่ง GPS รับข้อมูลจากอุปกรณ์ MQTT จัดการ Geofence แจ้งเตือน

## Database Tables

| Table | Description |
|-------|-------------|
| `m_iot_device` | อุปกรณ์ IoT (device_id, status, last_location) |
| `t_gps_data` | ข้อมูล GPS (ละติจูด, ลองจิจูด, ความเร็ว) |
| `t_device_history` | ประวัติการทำงานของอุปกรณ์ |
| `t_device_access_log` | บันทึกการเข้าถึงอุปกรณ์ |
| `m_geofence` | รั้วรอบขอบ (CIRCLE, POLYGON) |
| `t_geofence_alert` | การแจ้งเตือน Geofence |
| `t_auto_report` | รายงานอัตโนมัติ |

## MQTT Topics

| Topic | Description |
|-------|-------------|
| `gps/data` | ข้อมูล GPS จากอุปกรณ์ |
| `device/status` | สถานะอุปกรณ์ (ONLINE, OFFLINE) |
| `device/alert` | การแจ้งเตือนจากอุปกรณ์ |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| POST | `/api/iot/devices/register` | 10/300s |
| GET | `/api/iot/devices/{id}` | 100/60s |
| GET | `/api/iot/devices/{id}/location` | 60/60s |
| GET | `/api/iot/devices/{id}/history` | 30/60s |
| PUT | `/api/iot/devices/{id}` | 20/60s |
| POST | `/api/iot/mqtt/publish` | 20/60s |
| POST | `/api/iot/geofences` | 10/300s |
| GET | `/api/iot/geofences/alerts` | 30/60s |
| DELETE | `/api/iot/geofences/{id}` | 10/3600s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `iot_devices:{deviceId}` | 1 min |
| `iot_device_identifier:{identifier}` | 1 min |
| `device_location:{deviceId}` | 30 sec |

## Key Features

- ✅ MQTT client for real-time data ingestion
- ✅ GPS data storage in InfluxDB (time-series)
- ✅ Device management and registration
- ✅ Geofencing with circle and polygon support
- ✅ Real-time alerts and notifications
- ✅ Device history and access logs
- ✅ Auto-report generation
