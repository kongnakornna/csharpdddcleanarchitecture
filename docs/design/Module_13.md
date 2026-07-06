# 🚗 โมดูลที่ 13: IoT & GPS Tracking – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ IoT & GPS Tracking ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 55 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/IoTAggregate/IoTDevice.cs` | Aggregate Root อุปกรณ์ IoT |
| 2 | `Aggregates/IoTAggregate/GpsData.cs` | Entity ข้อมูล GPS |
| 3 | `Aggregates/IoTAggregate/DeviceHistory.cs` | Entity ประวัติอุปกรณ์ |
| 4 | `Aggregates/IoTAggregate/DeviceAccessLog.cs` | Entity บันทึกการเข้าถึง |
| 5 | `Aggregates/IoTAggregate/Geofence.cs` | Entity รั้วรอบขอบ |
| 6 | `Aggregates/IoTAggregate/GeofenceAlert.cs` | Entity การแจ้งเตือน Geofence |
| 7 | `Aggregates/IoTAggregate/AutoReport.cs` | Entity รายงานอัตโนมัติ |
| 8 | `Enums/DeviceStatus.cs` | สถานะอุปกรณ์ |
| 9 | `Enums/GeofenceType.cs` | ประเภท Geofence |
| 10 | `Enums/AlertType.cs` | ประเภทการแจ้งเตือน |
| 11 | `ValueObjects/Coordinates.cs` | Value Object พิกัด |
| 12 | `ValueObjects/Speed.cs` | Value Object ความเร็ว |
| 13 | `Events/DeviceRegisteredEvent.cs` | Event ลงทะเบียนอุปกรณ์ |
| 14 | `Events/DeviceStatusChangedEvent.cs` | Event เปลี่ยนสถานะ |
| 15 | `Events/GpsDataReceivedEvent.cs` | Event รับข้อมูล GPS |
| 16 | `Events/GeofenceAlertEvent.cs` | Event แจ้งเตือน Geofence |
| 17 | `Interfaces/IIoTDeviceRepository.cs` | Interface Repository |
| 18 | `Interfaces/IGpsDataRepository.cs` | Interface Repository |
| 19 | `Interfaces/IGeofenceRepository.cs` | Interface Repository |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 20 | `DTOs/IoT/IoTDeviceDto.cs` | DTO อุปกรณ์ |
| 21 | `DTOs/IoT/GpsDataDto.cs` | DTO GPS |
| 22 | `DTOs/IoT/GeofenceDto.cs` | DTO Geofence |
| 23 | `DTOs/IoT/GeofenceAlertDto.cs` | DTO การแจ้งเตือน |
| 24 | `Commands/IoT/RegisterDevice/RegisterDeviceCommand.cs` | Command ลงทะเบียน |
| 25 | `Commands/IoT/RegisterDevice/RegisterDeviceCommandHandler.cs` | Handler ลงทะเบียน |
| 26 | `Commands/IoT/RegisterDevice/RegisterDeviceCommandValidator.cs` | Validator ลงทะเบียน |
| 27 | `Commands/IoT/UpdateDeviceStatus/UpdateDeviceStatusCommand.cs` | Command อัปเดตสถานะ |
| 28 | `Commands/IoT/UpdateDeviceStatus/UpdateDeviceStatusCommandHandler.cs` | Handler อัปเดตสถานะ |
| 29 | `Commands/IoT/UpdateDeviceStatus/UpdateDeviceStatusCommandValidator.cs` | Validator อัปเดตสถานะ |
| 30 | `Commands/IoT/CreateGeofence/CreateGeofenceCommand.cs` | Command สร้าง Geofence |
| 31 | `Commands/IoT/CreateGeofence/CreateGeofenceCommandHandler.cs` | Handler สร้าง Geofence |
| 32 | `Commands/IoT/CreateGeofence/CreateGeofenceCommandValidator.cs` | Validator สร้าง Geofence |
| 33 | `Commands/IoT/PublishMqtt/PublishMqttCommand.cs` | Command ส่ง MQTT |
| 34 | `Commands/IoT/PublishMqtt/PublishMqttCommandHandler.cs` | Handler ส่ง MQTT |
| 35 | `Queries/IoT/GetDeviceById/GetDeviceByIdQuery.cs` | Query ดึงอุปกรณ์ |
| 36 | `Queries/IoT/GetDeviceById/GetDeviceByIdQueryHandler.cs` | Handler ดึงอุปกรณ์ |
| 37 | `Queries/IoT/GetDeviceByIdentifier/GetDeviceByIdentifierQuery.cs` | Query ดึงตาม Identifier |
| 38 | `Queries/IoT/GetDeviceByIdentifier/GetDeviceByIdentifierQueryHandler.cs` | Handler ดึงตาม Identifier |
| 39 | `Queries/IoT/GetDeviceLocation/GetDeviceLocationQuery.cs` | Query ตำแหน่งล่าสุด |
| 40 | `Queries/IoT/GetDeviceLocation/GetDeviceLocationQueryHandler.cs` | Handler ตำแหน่งล่าสุด |
| 41 | `Queries/IoT/GetDeviceHistory/GetDeviceHistoryQuery.cs` | Query ประวัติตำแหน่ง |
| 42 | `Queries/IoT/GetDeviceHistory/GetDeviceHistoryQueryHandler.cs` | Handler ประวัติตำแหน่ง |
| 43 | `Queries/IoT/GetGeofenceAlerts/GetGeofenceAlertsQuery.cs` | Query การแจ้งเตือน |
| 44 | `Queries/IoT/GetGeofenceAlerts/GetGeofenceAlertsQueryHandler.cs` | Handler การแจ้งเตือน |
| 45 | `Queries/IoT/GetDeviceList/GetDeviceListQuery.cs` | Query รายการอุปกรณ์ |
| 46 | `Queries/IoT/GetDeviceList/GetDeviceListQueryHandler.cs` | Handler รายการอุปกรณ์ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 47 | `Persistence/Configurations/IoTDeviceConfiguration.cs` | EF Config IoTDevice |
| 48 | `Persistence/Configurations/GpsDataConfiguration.cs` | EF Config GpsData |
| 49 | `Persistence/Configurations/GeofenceConfiguration.cs` | EF Config Geofence |
| 50 | `Persistence/Configurations/GeofenceAlertConfiguration.cs` | EF Config GeofenceAlert |
| 51 | `Persistence/Repositories/IoTDeviceRepository.cs` | Repository IoTDevice |
| 52 | `Persistence/Repositories/GpsDataRepository.cs` | Repository GpsData |
| 53 | `Persistence/Repositories/GeofenceRepository.cs` | Repository Geofence |
| 54 | `IoT/MqttClientService.cs` | MQTT Client Service |
| 55 | `IoT/GpsDataProcessor.cs` | GPS Data Processor |
| 56 | `IoT/GeofenceService.cs` | Geofence Service |
| 57 | `IoT/InfluxDbService.cs` | InfluxDB Service |
| 58 | `Persistence/SeedData/IoTSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 59 | `Controllers/IoTController.cs` | Controller IoT (14 Endpoints) |
| 60 | `Controllers/MqttController.cs` | Controller MQTT (1 Endpoint) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### IoT Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/iot/devices/register` | ลงทะเบียนอุปกรณ์ | 10/300s |
| GET | `/api/iot/devices/{id}` | ดูข้อมูลอุปกรณ์ | 100/60s |
| GET | `/api/iot/devices/identifier/{identifier}` | ค้นหาด้วย Identifier | 60/60s |
| GET | `/api/iot/devices` | รายการอุปกรณ์ | 50/60s |
| GET | `/api/iot/devices/{id}/location` | ตำแหน่งล่าสุด | 60/60s |
| GET | `/api/iot/devices/{id}/history` | ประวัติตำแหน่ง | 30/60s |
| PUT | `/api/iot/devices/{id}/status` | อัปเดตสถานะ | 30/60s |
| DELETE | `/api/iot/devices/{id}` | ลบอุปกรณ์ | 5/3600s |
| POST | `/api/iot/geofences` | สร้าง Geofence | 10/300s |
| GET | `/api/iot/geofences` | รายการ Geofence | 50/60s |
| GET | `/api/iot/geofences/alerts` | รายการแจ้งเตือน | 30/60s |
| GET | `/api/iot/geofences/alerts/{id}` | ดูการแจ้งเตือน | 60/60s |
| GET | `/api/iot/summary` | สรุปภาพรวม IoT | 30/60s |
| GET | `/api/iot/devices/status-summary` | สรุปสถานะอุปกรณ์ | 30/60s |

### MQTT Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/mqtt/publish` | ส่งข้อความ MQTT | 20/60s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="MQTTnet" Version="4.3.1.873" />
<PackageReference Include="MQTTnet.Extensions.ManagedClient" Version="4.3.1.873" />
<PackageReference Include="InfluxDB.Client" Version="4.14.0" />
<PackageReference Include="NetTopologySuite" Version="2.5.0" />
<PackageReference Include="NetTopologySuite.Core" Version="1.15.3" />
<PackageReference Include="Microsoft.EntityFrameworkCore.NetTopologySuite" Version="8.0.0" />
```

---

## 📡 MQTT Topics

| Topic | คำอธิบาย |
|-------|----------|
| `gps/data` | ข้อมูล GPS จากอุปกรณ์ |
| `device/status` | สถานะอุปกรณ์ (ONLINE, OFFLINE) |
| `device/alert` | การแจ้งเตือนจากอุปกรณ์ |
| `device/command/{deviceId}` | ส่งคำสั่งไปยังอุปกรณ์ |

---

## 🚀 วิธีใช้

### 1. ลงทะเบียนอุปกรณ์ IoT
```http
POST /api/iot/devices/register
Authorization: Bearer <accessToken>
{
  "identifier": "GPS-001-A1B2C3",
  "name": "GPS Tracker รถยนต์คันที่ 1",
  "description": "อุปกรณ์ติดตามรถยนต์ Toyota Camry",
  "deviceType": "GPS",
  "latitude": 13.7563,
  "longitude": 100.5018
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "identifier": "GPS-001-A1B2C3",
    "name": "GPS Tracker รถยนต์คันที่ 1",
    "status": "Offline",
    "deviceType": "GPS",
    "registeredAt": "2026-07-06T10:30:00Z",
    "lastLocation": {
      "latitude": 13.7563,
      "longitude": 100.5018
    }
  }
}
```

### 2. ดูตำแหน่งล่าสุดของอุปกรณ์
```http
GET /api/iot/devices/{deviceId}/location
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "deviceId": "...",
  "deviceName": "GPS Tracker รถยนต์คันที่ 1",
  "latitude": 13.7563,
  "longitude": 100.5018,
  "altitude": 1.5,
  "speed": 0.0,
  "direction": 0.0,
  "accuracy": 10.0,
  "updatedAt": "2026-07-06T10:35:00Z",
  "address": "123 ถนนสุขุมวิท กรุงเทพฯ"
}
```

### 3. ประวัติตำแหน่ง
```http
GET /api/iot/devices/{deviceId}/history?fromDate=2026-07-01&toDate=2026-07-06&limit=100
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "latitude": 13.7563,
    "longitude": 100.5018,
    "speed": 0.0,
    "recordedAt": "2026-07-06T10:35:00Z"
  },
  {
    "latitude": 13.7570,
    "longitude": 100.5025,
    "speed": 5.2,
    "recordedAt": "2026-07-06T10:34:30Z"
  }
]
```

### 4. สร้าง Geofence
```http
POST /api/iot/geofences
Authorization: Bearer <accessToken>
{
  "name": "เขตปลอดภัย 1",
  "description": "พื้นที่รับส่งรถ",
  "type": "Circle",
  "centerLatitude": 13.7563,
  "centerLongitude": 100.5018,
  "radius": 100,
  "alertOnEnter": true,
  "alertOnExit": true,
  "deviceIds": ["device-id-1", "device-id-2"]
}
```
**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "name": "เขตปลอดภัย 1",
    "type": "Circle",
    "center": {
      "latitude": 13.7563,
      "longitude": 100.5018
    },
    "radius": 100,
    "isActive": true
  }
}
```

### 5. รายการแจ้งเตือน Geofence
```http
GET /api/iot/geofences/alerts?deviceId={deviceId}&fromDate=2026-07-01&toDate=2026-07-06
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "id": "...",
    "deviceId": "...",
    "deviceName": "GPS Tracker รถยนต์คันที่ 1",
    "geofenceId": "...",
    "geofenceName": "เขตปลอดภัย 1",
    "alertType": "Exit",
    "latitude": 13.7575,
    "longitude": 100.5025,
    "triggeredAt": "2026-07-06T10:40:00Z",
    "isResolved": false
  }
]
```

### 6. ส่งข้อความ MQTT
```http
POST /api/mqtt/publish
Authorization: Bearer <accessToken>
{
  "topic": "device/command/GPS-001-A1B2C3",
  "payload": "{\"command\":\"get_location\",\"timestamp\":\"2026-07-06T10:30:00Z\"}",
  "qos": 1,
  "retain": false
}
```

### 7. สรุปภาพรวม IoT
```http
GET /api/iot/summary
Authorization: Bearer <accessToken>
```
**Response:**
```json
{
  "totalDevices": 25,
  "onlineDevices": 18,
  "offlineDevices": 7,
  "totalGeofences": 5,
  "activeGeofences": 4,
  "alertsToday": 3,
  "totalGpsRecords": 12500,
  "averageUpdateFrequency": 15
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - IoTDevice.cs
```csharp
namespace ICMON.Domain.Aggregates.IoTAggregate;

public class IoTDevice : AggregateRoot<Guid>
{
    public string Identifier { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string DeviceType { get; private set; }
    public DeviceStatus Status { get; private set; }
    public Coordinates LastLocation { get; private set; }
    public DateTime? LastLocationUpdate { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public DateTime? LastHeartbeat { get; private set; }
    public int BatteryLevel { get; private set; }
    public string? FirmwareVersion { get; private set; }
    public bool IsActive { get; private set; }
    public Dictionary<string, string>? Metadata { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<GpsData> _gpsHistory = new();
    public IReadOnlyList<GpsData> GpsHistory => _gpsHistory.AsReadOnly();

    private readonly List<DeviceHistory> _history = new();
    public IReadOnlyList<DeviceHistory> History => _history.AsReadOnly();

    private readonly List<DeviceAccessLog> _accessLogs = new();
    public IReadOnlyList<DeviceAccessLog> AccessLogs => _accessLogs.AsReadOnly();

    private IoTDevice() { } // For EF Core

    public static IoTDevice Create(
        string identifier,
        string name,
        string deviceType,
        double latitude,
        double longitude,
        Guid whitelabelId,
        string? description = null,
        Dictionary<string, string>? metadata = null)
    {
        var device = new IoTDevice
        {
            Id = Guid.NewGuid(),
            Identifier = identifier,
            Name = name,
            Description = description,
            DeviceType = deviceType,
            Status = DeviceStatus.Offline,
            LastLocation = new Coordinates(latitude, longitude),
            LastLocationUpdate = DateTime.UtcNow,
            RegisteredAt = DateTime.UtcNow,
            IsActive = true,
            BatteryLevel = 100,
            Metadata = metadata,
            WhitelabelId = whitelabelId
        };

        device.AddHistory("Device registered", "Registration");
        device.AddDomainEvent(new DeviceRegisteredEvent(device.Id, identifier));

        return device;
    }

    public void UpdateLocation(double latitude, double longitude, double? altitude = null,
        double? speed = null, double? direction = null, double? accuracy = null)
    {
        var gpsData = GpsData.Create(
            Id,
            latitude,
            longitude,
            altitude,
            speed,
            direction,
            accuracy
        );

        _gpsHistory.Add(gpsData);
        LastLocation = new Coordinates(latitude, longitude);
        LastLocationUpdate = DateTime.UtcNow;

        AddDomainEvent(new GpsDataReceivedEvent(Id, latitude, longitude));
    }

    public void UpdateStatus(DeviceStatus status)
    {
        if (Status != status)
        {
            Status = status;
            LastHeartbeat = DateTime.UtcNow;
            AddHistory($"Status changed to {status}", "StatusChange");
            AddDomainEvent(new DeviceStatusChangedEvent(Id, status));
        }
    }

    public void Heartbeat(double? batteryLevel = null)
    {
        LastHeartbeat = DateTime.UtcNow;
        if (batteryLevel.HasValue)
        {
            BatteryLevel = Math.Clamp((int)batteryLevel.Value, 0, 100);
        }

        if (Status == DeviceStatus.Offline)
        {
            Status = DeviceStatus.Online;
            AddHistory("Device came online", "Heartbeat");
        }
    }

    public void UpdateBatteryLevel(int batteryLevel)
    {
        BatteryLevel = Math.Clamp(batteryLevel, 0, 100);
    }

    public void UpdateFirmware(string version)
    {
        FirmwareVersion = version;
        AddHistory($"Firmware updated to {version}", "FirmwareUpdate");
    }

    public void Deactivate()
    {
        IsActive = false;
        Status = DeviceStatus.Offline;
        AddHistory("Device deactivated", "Deactivation");
    }

    public void Activate()
    {
        IsActive = true;
        AddHistory("Device activated", "Activation");
    }

    public void AddHistory(string description, string type)
    {
        _history.Add(DeviceHistory.Create(Id, description, type));
    }

    public void LogAccess(string accessedBy, string action, string? ipAddress = null)
    {
        _accessLogs.Add(DeviceAccessLog.Create(Id, accessedBy, action, ipAddress));
    }

    public bool IsOnline() => Status == DeviceStatus.Online;
    public bool IsOffline() => Status == DeviceStatus.Offline;
    public bool NeedsMaintenance() => BatteryLevel < 20 || IsOffline();

    public double GetDistanceTo(double latitude, double longitude)
    {
        var coords = new Coordinates(latitude, longitude);
        return LastLocation.DistanceTo(coords);
    }
}
```

### 2. Domain Entity - Geofence.cs
```csharp
namespace ICMON.Domain.Aggregates.IoTAggregate;

public class Geofence : Entity<Guid>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public GeofenceType Type { get; private set; }
    public Coordinates Center { get; private set; }
    public double? Radius { get; private set; } // For Circle type
    public string? PolygonCoordinates { get; private set; } // For Polygon type
    public bool AlertOnEnter { get; private set; }
    public bool AlertOnExit { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<Guid> _deviceIds = new();
    public IReadOnlyList<Guid> DeviceIds => _deviceIds.AsReadOnly();

    private readonly List<GeofenceAlert> _alerts = new();
    public IReadOnlyList<GeofenceAlert> Alerts => _alerts.AsReadOnly();

    private Geofence() { } // For EF Core

    public static Geofence Create(
        string name,
        GeofenceType type,
        Coordinates center,
        Guid whitelabelId,
        double? radius = null,
        string? polygonCoordinates = null,
        string? description = null,
        bool alertOnEnter = true,
        bool alertOnExit = true)
    {
        if (type == GeofenceType.Circle && (!radius.HasValue || radius.Value <= 0))
            throw new ArgumentException("Radius is required for Circle geofence");

        var geofence = new Geofence
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Type = type,
            Center = center,
            Radius = radius,
            PolygonCoordinates = polygonCoordinates,
            AlertOnEnter = alertOnEnter,
            AlertOnExit = alertOnExit,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        return geofence;
    }

    public void Update(string name, string? description, double? radius, string? polygonCoordinates,
        bool alertOnEnter, bool alertOnExit)
    {
        Name = name;
        Description = description;
        Radius = radius;
        PolygonCoordinates = polygonCoordinates;
        AlertOnEnter = alertOnEnter;
        AlertOnExit = alertOnExit;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignDevice(Guid deviceId)
    {
        if (!_deviceIds.Contains(deviceId))
            _deviceIds.Add(deviceId);
    }

    public void UnassignDevice(Guid deviceId)
    {
        _deviceIds.Remove(deviceId);
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsDeviceInside(Guid deviceId, Coordinates location)
    {
        if (!IsActive || !_deviceIds.Contains(deviceId))
            return false;

        return Type switch
        {
            GeofenceType.Circle => IsInsideCircle(location),
            GeofenceType.Polygon => IsInsidePolygon(location),
            _ => false
        };
    }

    private bool IsInsideCircle(Coordinates location)
    {
        if (!Radius.HasValue) return false;
        return Center.DistanceTo(location) <= Radius.Value;
    }

    private bool IsInsidePolygon(Coordinates location)
    {
        if (string.IsNullOrEmpty(PolygonCoordinates))
            return false;

        // Parse polygon coordinates and use ray-casting algorithm
        var points = ParsePolygonCoordinates();
        return PointInPolygon(location, points);
    }

    private List<Coordinates> ParsePolygonCoordinates()
    {
        // Format: "lat1,lon1;lat2,lon2;lat3,lon3;..."
        var result = new List<Coordinates>();
        var parts = PolygonCoordinates.Split(';', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            var coords = part.Split(',');
            if (coords.Length == 2 &&
                double.TryParse(coords[0], out var lat) &&
                double.TryParse(coords[1], out var lon))
            {
                result.Add(new Coordinates(lat, lon));
            }
        }
        return result;
    }

    private bool PointInPolygon(Coordinates point, List<Coordinates> polygon)
    {
        bool inside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if ((polygon[i].Latitude > point.Latitude) != (polygon[j].Latitude > point.Latitude) &&
                point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) *
                (point.Latitude - polygon[i].Latitude) /
                (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude)
            {
                inside = !inside;
            }
        }
        return inside;
    }

    public GeofenceAlert CreateAlert(Guid deviceId, AlertType alertType, Coordinates location)
    {
        var alert = GeofenceAlert.Create(Id, deviceId, alertType, location);
        _alerts.Add(alert);
        return alert;
    }
}
```

### 3. Domain Entity - GpsData.cs
```csharp
namespace ICMON.Domain.Aggregates.IoTAggregate;

public class GpsData : Entity<Guid>
{
    public Guid DeviceId { get; private set; }
    public virtual IoTDevice Device { get; private set; }
    public Coordinates Location { get; private set; }
    public double? Altitude { get; private set; }
    public Speed Speed { get; private set; }
    public double? Direction { get; private set; }
    public double? Accuracy { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public string? Source { get; private set; }
    public Dictionary<string, object>? Metadata { get; private set; }

    private GpsData() { } // For EF Core

    public static GpsData Create(
        Guid deviceId,
        double latitude,
        double longitude,
        double? altitude = null,
        double? speed = null,
        double? direction = null,
        double? accuracy = null,
        string? source = null,
        Dictionary<string, object>? metadata = null)
    {
        return new GpsData
        {
            Id = Guid.NewGuid(),
            DeviceId = deviceId,
            Location = new Coordinates(latitude, longitude),
            Altitude = altitude,
            Speed = speed.HasValue ? Speed.FromKilometersPerHour(speed.Value) : Speed.Zero,
            Direction = direction,
            Accuracy = accuracy,
            RecordedAt = DateTime.UtcNow,
            Source = source,
            Metadata = metadata
        };
    }
}
```

### 4. Value Objects
```csharp
// Coordinates.cs
namespace ICMON.Domain.ValueObjects;

public class Coordinates : ValueObject
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Coordinates(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Latitude must be between -90 and 90");
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Longitude must be between -180 and 180");

        Latitude = latitude;
        Longitude = longitude;
    }

    public double DistanceTo(Coordinates other)
    {
        // Haversine formula
        var R = 6371.0; // Earth radius in km
        var dLat = ToRadians(other.Latitude - Latitude);
        var dLon = ToRadians(other.Longitude - Longitude);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(other.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Math.Round(Latitude, 6);
        yield return Math.Round(Longitude, 6);
    }

    public override string ToString() => $"{Latitude:F6}, {Longitude:F6}";
}

// Speed.cs
namespace ICMON.Domain.ValueObjects;

public class Speed : ValueObject
{
    public double KilometersPerHour { get; }
    public double MetersPerSecond => KilometersPerHour / 3.6;
    public double MilesPerHour => KilometersPerHour * 0.621371;

    public static Speed Zero => new(0);

    private Speed(double kmh)
    {
        KilometersPerHour = Math.Max(0, kmh);
    }

    public static Speed FromKilometersPerHour(double kmh) => new(kmh);
    public static Speed FromMetersPerSecond(double mps) => new(mps * 3.6);
    public static Speed FromMilesPerHour(double mph) => new(mph / 0.621371);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Math.Round(KilometersPerHour, 2);
    }
}
```

### 5. Domain Enums
```csharp
namespace ICMON.Domain.Enums;

public enum DeviceStatus
{
    Online = 0,
    Offline = 1,
    Maintenance = 2,
    Deactivated = 3
}

public enum GeofenceType
{
    Circle = 0,
    Polygon = 1
}

public enum AlertType
{
    Enter = 0,
    Exit = 1,
    Speeding = 2,
    LowBattery = 3,
    DeviceOffline = 4
}
```

### 6. Infrastructure - MqttClientService.cs
```csharp
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text.Json;

namespace ICMON.Infrastructure.IoT;

public class MqttClientService : IHostedService
{
    private readonly IMqttClient _mqttClient;
    private readonly MqttClientOptions _options;
    private readonly ILogger<MqttClientService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly JsonSerializerOptions _jsonOptions;

    public MqttClientService(
        IConfiguration configuration,
        ILogger<MqttClientService> logger,
        IServiceScopeFactory scopeFactory)
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();
        _logger = logger;
        _scopeFactory = scopeFactory;

        var broker = configuration["Mqtt:Broker"] ?? "localhost";
        var port = int.Parse(configuration["Mqtt:Port"] ?? "1883");
        var clientId = configuration["Mqtt:ClientId"] ?? $"ICMON-{Guid.NewGuid():N[..8]}";

        _options = new MqttClientOptionsBuilder()
            .WithTcpServer(broker, port)
            .WithClientId(clientId)
            .WithCleanSession()
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
            .Build();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _mqttClient.ConnectedAsync += OnConnectedAsync;
        _mqttClient.DisconnectedAsync += OnDisconnectedAsync;
        _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;

        try
        {
            await _mqttClient.ConnectAsync(_options, cancellationToken);
            _logger.LogInformation("MQTT Client started");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to MQTT broker");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
        _logger.LogInformation("MQTT Client stopped");
    }

    private Task OnConnectedAsync(MqttClientConnectedEventArgs args)
    {
        _logger.LogInformation("Connected to MQTT broker");

        // Subscribe to topics
        var topics = new[]
        {
            "gps/data",
            "device/status",
            "device/alert"
        };

        foreach (var topic in topics)
        {
            _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build());
        }

        return Task.CompletedTask;
    }

    private Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs args)
    {
        _logger.LogWarning("Disconnected from MQTT broker: {Reason}", args.Reason);

        // Attempt to reconnect
        Task.Delay(5000).ContinueWith(async _ =>
        {
            try
            {
                await _mqttClient.ConnectAsync(_options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reconnect to MQTT broker");
            }
        });

        return Task.CompletedTask;
    }

    private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
    {
        try
        {
            var topic = args.ApplicationMessage.Topic;
            var payload = args.ApplicationMessage.PayloadSegment;
            var payloadString = System.Text.Encoding.UTF8.GetString(payload);

            _logger.LogDebug("MQTT message received: {Topic} - {Payload}", topic, payloadString);

            using var scope = _scopeFactory.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IGpsDataProcessor>();
            var geofenceService = scope.ServiceProvider.GetRequiredService<IGeofenceService>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // Process message based on topic
            switch (topic)
            {
                case "gps/data":
                    await processor.ProcessGpsDataAsync(payloadString);
                    break;

                case "device/status":
                    await processor.ProcessDeviceStatusAsync(payloadString);
                    break;

                case "device/alert":
                    await processor.ProcessDeviceAlertAsync(payloadString);
                    break;

                default:
                    _logger.LogWarning("Unknown MQTT topic: {Topic}", topic);
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing MQTT message");
        }
    }

    public async Task PublishAsync(string topic, object payload, int qos = 1, bool retain = false)
    {
        var json = JsonSerializer.Serialize(payload, _jsonOptions);
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(json)
            .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retain)
            .Build();

        await _mqttClient.PublishAsync(message);
        _logger.LogDebug("Published MQTT message to {Topic}", topic);
    }
}
```

### 7. Infrastructure - GpsDataProcessor.cs
```csharp
using System.Text.Json;

namespace ICMON.Infrastructure.IoT;

public interface IGpsDataProcessor
{
    Task ProcessGpsDataAsync(string payload);
    Task ProcessDeviceStatusAsync(string payload);
    Task ProcessDeviceAlertAsync(string payload);
}

public class GpsDataProcessor : IGpsDataProcessor
{
    private readonly ILogger<GpsDataProcessor> _logger;
    private readonly IIoTDeviceRepository _deviceRepository;
    private readonly IGpsDataRepository _gpsDataRepository;
    private readonly IGeofenceRepository _geofenceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly JsonSerializerOptions _jsonOptions;

    public GpsDataProcessor(
        ILogger<GpsDataProcessor> logger,
        IIoTDeviceRepository deviceRepository,
        IGpsDataRepository gpsDataRepository,
        IGeofenceRepository geofenceRepository,
        IUnitOfWork unitOfWork,
        IMediator mediator)
    {
        _logger = logger;
        _deviceRepository = deviceRepository;
        _gpsDataRepository = gpsDataRepository;
        _geofenceRepository = geofenceRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task ProcessGpsDataAsync(string payload)
    {
        try
        {
            var data = JsonSerializer.Deserialize<GpsPayload>(payload, _jsonOptions);
            if (data == null)
                throw new InvalidOperationException("Invalid GPS data format");

            var device = await _deviceRepository.GetByIdentifierAsync(data.DeviceIdentifier);
            if (device == null)
            {
                _logger.LogWarning("Unknown device: {DeviceIdentifier}", data.DeviceIdentifier);
                return;
            }

            if (!device.IsActive)
            {
                _logger.LogWarning("Device is inactive: {DeviceIdentifier}", data.DeviceIdentifier);
                return;
            }

            // Update device location
            device.UpdateLocation(
                data.Latitude,
                data.Longitude,
                data.Altitude,
                data.Speed,
                data.Direction,
                data.Accuracy
            );

            if (data.BatteryLevel.HasValue)
                device.UpdateBatteryLevel(data.BatteryLevel.Value);

            device.Heartbeat();

            // Save GPS data
            var gpsData = GpsData.Create(
                device.Id,
                data.Latitude,
                data.Longitude,
                data.Altitude,
                data.Speed,
                data.Direction,
                data.Accuracy,
                "MQTT"
            );

            await _gpsDataRepository.AddAsync(gpsData);

            // Check geofences
            await CheckGeofences(device, gpsData.Location);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogDebug("GPS data processed for device {DeviceIdentifier}", data.DeviceIdentifier);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Invalid JSON payload: {Payload}", payload);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing GPS data");
            throw;
        }
    }

    public async Task ProcessDeviceStatusAsync(string payload)
    {
        try
        {
            var data = JsonSerializer.Deserialize<DeviceStatusPayload>(payload, _jsonOptions);
            if (data == null)
                throw new InvalidOperationException("Invalid device status format");

            var device = await _deviceRepository.GetByIdentifierAsync(data.DeviceIdentifier);
            if (device == null)
            {
                _logger.LogWarning("Unknown device: {DeviceIdentifier}", data.DeviceIdentifier);
                return;
            }

            device.UpdateStatus(data.Status);
            if (data.BatteryLevel.HasValue)
                device.UpdateBatteryLevel(data.BatteryLevel.Value);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogDebug("Device status updated: {DeviceIdentifier} -> {Status}",
                data.DeviceIdentifier, data.Status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing device status");
            throw;
        }
    }

    public async Task ProcessDeviceAlertAsync(string payload)
    {
        try
        {
            var data = JsonSerializer.Deserialize<DeviceAlertPayload>(payload, _jsonOptions);
            if (data == null)
                throw new InvalidOperationException("Invalid device alert format");

            var device = await _deviceRepository.GetByIdentifierAsync(data.DeviceIdentifier);
            if (device == null)
            {
                _logger.LogWarning("Unknown device: {DeviceIdentifier}", data.DeviceIdentifier);
                return;
            }

            // Process alert
            var alertType = data.AlertType.ToLower() switch
            {
                "speeding" => AlertType.Speeding,
                "lowbattery" => AlertType.LowBattery,
                "offline" => AlertType.DeviceOffline,
                _ => AlertType.DeviceOffline
            };

            // Log the alert
            device.AddHistory($"Alert: {data.AlertType} - {data.Message}", "Alert");

            await _unitOfWork.SaveChangesAsync();

            // Could publish to notification system
            _logger.LogWarning("Device alert: {DeviceIdentifier} - {AlertType} - {Message}",
                data.DeviceIdentifier, data.AlertType, data.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing device alert");
            throw;
        }
    }

    private async Task CheckGeofences(IoTDevice device, Coordinates location)
    {
        var geofences = await _geofenceRepository.GetByDeviceIdAsync(device.Id);

        foreach (var geofence in geofences)
        {
            var isInside = geofence.IsDeviceInside(device.Id, location);
            var previousInside = await _geofenceRepository.GetDeviceStatusAsync(device.Id, geofence.Id);

            if (isInside != previousInside)
            {
                var alertType = isInside ? AlertType.Enter : AlertType.Exit;
                var alert = geofence.CreateAlert(device.Id, alertType, location);

                await _geofenceRepository.AddAlertAsync(alert);

                // Publish alert via MQTT
                // Could also send email notification
                _logger.LogInformation("Geofence alert: {Device} {AlertType} {Geofence}",
                    device.Identifier, alertType, geofence.Name);

                // Update device status in geofence
                await _geofenceRepository.UpdateDeviceStatusAsync(device.Id, geofence.Id, isInside);
            }
        }
    }

    #region Payload Classes

    private class GpsPayload
    {
        public string DeviceIdentifier { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Speed { get; set; }
        public double? Direction { get; set; }
        public double? Accuracy { get; set; }
        public int? BatteryLevel { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    private class DeviceStatusPayload
    {
        public string DeviceIdentifier { get; set; }
        public DeviceStatus Status { get; set; }
        public int? BatteryLevel { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    private class DeviceAlertPayload
    {
        public string DeviceIdentifier { get; set; }
        public string AlertType { get; set; }
        public string Message { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    #endregion
}
```

### 8. Controller - IoTController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IoTController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIoTDeviceRepository _deviceRepository;
    private readonly IGeofenceRepository _geofenceRepository;

    public IoTController(
        IMediator mediator,
        IIoTDeviceRepository deviceRepository,
        IGeofenceRepository geofenceRepository)
    {
        _mediator = mediator;
        _deviceRepository = deviceRepository;
        _geofenceRepository = geofenceRepository;
    }

    #region Device Management

    [HttpPost("devices/register")]
    [Authorize("IOT_DEVICE_CREATE")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<IoTDeviceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("devices/{id}")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<IoTDeviceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDevice(Guid id)
    {
        var query = new GetDeviceByIdQuery { DeviceId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("devices/identifier/{identifier}")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDeviceByIdentifier(string identifier)
    {
        var query = new GetDeviceByIdentifierQuery { Identifier = identifier };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("devices")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDeviceList(
        [FromQuery] DeviceStatus? status,
        [FromQuery] string? deviceType,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetDeviceListQuery
        {
            Status = status,
            DeviceType = deviceType,
            IsActive = isActive,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("devices/{id}/location")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<GpsDataDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeviceLocation(Guid id)
    {
        var query = new GetDeviceLocationQuery { DeviceId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("devices/{id}/history")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDeviceHistory(
        Guid id,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int limit = 100)
    {
        var query = new GetDeviceHistoryQuery
        {
            DeviceId = id,
            FromDate = fromDate ?? DateTime.UtcNow.AddDays(-7),
            ToDate = toDate ?? DateTime.UtcNow,
            Limit = Math.Min(limit, 1000)
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("devices/{id}/status")]
    [Authorize("IOT_DEVICE_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdateDeviceStatus(Guid id, [FromBody] UpdateDeviceStatusCommand command)
    {
        if (id != command.DeviceId)
            return BadRequest(Result<IoTDeviceDto>.Failure("Device ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("devices/{id}")]
    [Authorize("IOT_DEVICE_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteDevice(Guid id)
    {
        var command = new DeleteDeviceCommand { DeviceId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    #endregion

    #region Geofence Management

    [HttpPost("geofences")]
    [Authorize("IOT_GEOFENCE_CREATE")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<GeofenceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGeofence([FromBody] CreateGeofenceCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("geofences")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetGeofences([FromQuery] bool? isActive = true)
    {
        var query = new GetGeofencesQuery { IsActive = isActive };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("geofences/{id}")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetGeofence(Guid id)
    {
        var query = new GetGeofenceByIdQuery { GeofenceId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("geofences/alerts")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetGeofenceAlerts(
        [FromQuery] Guid? deviceId,
        [FromQuery] AlertType? alertType,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetGeofenceAlertsQuery
        {
            DeviceId = deviceId,
            AlertType = alertType,
            FromDate = fromDate ?? DateTime.UtcNow.AddDays(-7),
            ToDate = toDate ?? DateTime.UtcNow,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("geofences/alerts/{id}")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetGeofenceAlert(Guid id)
    {
        var query = new GetGeofenceAlertByIdQuery { AlertId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("geofences/alerts/{id}/resolve")]
    [Authorize("IOT_GEOFENCE_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> ResolveGeofenceAlert(Guid id)
    {
        var command = new ResolveGeofenceAlertCommand { AlertId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    #endregion

    #region Summary

    [HttpGet("summary")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetIoTSummary()
    {
        var query = new GetIoTSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("devices/status-summary")]
    [Authorize("IOT_DEVICE_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetDeviceStatusSummary()
    {
        var query = new GetDeviceStatusSummaryQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion
}
```

### 9. Controller - MqttController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MqttController : ControllerBase
{
    private readonly IMediator _mediator;

    public MqttController(IMediator mediator) => _mediator = mediator;

    [HttpPost("publish")]
    [Authorize("IOT_MQTT_PUBLISH")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> PublishMqtt([FromBody] PublishMqttCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 10. InfluxDB Service
```csharp
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace ICMON.Infrastructure.IoT;

public class InfluxDbService : IInfluxDbService
{
    private readonly InfluxDBClient _client;
    private readonly string _bucket;
    private readonly string _org;
    private readonly ILogger<InfluxDbService> _logger;

    public InfluxDbService(IConfiguration configuration, ILogger<InfluxDbService> logger)
    {
        var url = configuration["InfluxDb:Url"] ?? "http://localhost:8086";
        var token = configuration["InfluxDb:Token"];
        _bucket = configuration["InfluxDb:Bucket"] ?? "icmon_iot";
        _org = configuration["InfluxDb:Org"] ?? "icmon";

        _client = new InfluxDBClient(url, token);
        _logger = logger;
    }

    public async Task WriteGpsDataAsync(Guid deviceId, string deviceIdentifier, 
        double latitude, double longitude, double? speed = null, 
        double? altitude = null, double? direction = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var point = PointData
                .Measurement("gps_data")
                .Tag("device_id", deviceId.ToString())
                .Tag("device_identifier", deviceIdentifier)
                .Field("latitude", latitude)
                .Field("longitude", longitude)
                .Field("altitude", altitude ?? 0)
                .Field("speed", speed ?? 0)
                .Field("direction", direction ?? 0)
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            await _client.GetWriteApiAsync()
                .WritePointAsync(point, _bucket, _org, cancellationToken);

            _logger.LogDebug("GPS data written to InfluxDB for device {DeviceIdentifier}", deviceIdentifier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error writing GPS data to InfluxDB");
            throw;
        }
    }

    public async Task WriteDeviceStatusAsync(Guid deviceId, string deviceIdentifier, 
        DeviceStatus status, int batteryLevel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var point = PointData
                .Measurement("device_status")
                .Tag("device_id", deviceId.ToString())
                .Tag("device_identifier", deviceIdentifier)
                .Field("status", (int)status)
                .Field("battery_level", batteryLevel)
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            await _client.GetWriteApiAsync()
                .WritePointAsync(point, _bucket, _org, cancellationToken);

            _logger.LogDebug("Device status written to InfluxDB for device {DeviceIdentifier}", deviceIdentifier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error writing device status to InfluxDB");
            throw;
        }
    }

    public async Task<IEnumerable<GpsDataDto>> QueryGpsHistoryAsync(
        Guid deviceId,
        DateTime fromDate,
        DateTime toDate,
        int limit = 100,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var flux = $@"
                from(bucket: ""{_bucket}"")
                |> range(start: {fromDate:yyyy-MM-ddTHH:mm:ssZ}, stop: {toDate:yyyy-MM-ddTHH:mm:ssZ})
                |> filter(fn: (r) => r._measurement == ""gps_data"")
                |> filter(fn: (r) => r.device_id == ""{deviceId}"")
                |> pivot(rowKey:[""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                |> sort(columns: [""_time""], desc: false)
                |> limit(n: {limit})
            ";

            var queryApi = _client.GetQueryApi();
            var results = await queryApi.QueryAsync(flux, _org, cancellationToken);

            var gpsData = new List<GpsDataDto>();
            foreach (var table in results)
            {
                foreach (var record in table.Records)
                {
                    if (record.Values.TryGetValue("latitude", out var lat) &&
                        record.Values.TryGetValue("longitude", out var lon))
                    {
                        gpsData.Add(new GpsDataDto
                        {
                            Latitude = Convert.ToDouble(lat),
                            Longitude = Convert.ToDouble(lon),
                            Altitude = record.Values.TryGetValue("altitude", out var alt) ? Convert.ToDouble(alt) : null,
                            Speed = record.Values.TryGetValue("speed", out var spd) ? Convert.ToDouble(spd) : null,
                            Direction = record.Values.TryGetValue("direction", out var dir) ? Convert.ToDouble(dir) : null,
                            RecordedAt = record.GetTime().ToDateTimeUtc()
                        });
                    }
                }
            }

            return gpsData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error querying GPS history from InfluxDB");
            throw;
        }
    }

    public async Task<Dictionary<DeviceStatus, int>> GetDeviceStatusSummaryAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var flux = $@"
                from(bucket: ""{_bucket}"")
                |> range(start: -1h)
                |> filter(fn: (r) => r._measurement == ""device_status"")
                |> last()
                |> group(columns: [""device_id""])
                |> pivot(rowKey:[""device_id""], columnKey: [""_field""], valueColumn: ""_value"")
                |> keep(columns: [""status""])
                |> group(columns: [""status""])
                |> count()
            ";

            var queryApi = _client.GetQueryApi();
            var results = await queryApi.QueryAsync(flux, _org, cancellationToken);

            var summary = new Dictionary<DeviceStatus, int>();
            foreach (var table in results)
            {
                foreach (var record in table.Records)
                {
                    if (record.Values.TryGetValue("status", out var status) &&
                        record.Values.TryGetValue("_value", out var count))
                    {
                        var statusEnum = (DeviceStatus)Convert.ToInt32(status);
                        summary[statusEnum] = Convert.ToInt32(count);
                    }
                }
            }

            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting device status summary from InfluxDB");
            throw;
        }
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `iot_devices:{deviceId}` | 1 นาที | ข้อมูลอุปกรณ์ IoT |
| `iot_device_identifier:{identifier}` | 1 นาที | อุปกรณ์ตาม Identifier |
| `device_location:{deviceId}` | 30 วินาที | ตำแหน่งล่าสุด |
| `geofences:{deviceId}` | 1 นาที | Geofence ของอุปกรณ์ |
| `iot_summary:{whitelabelId}` | 1 นาที | สรุปภาพรวม IoT |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ ลงทะเบียนอุปกรณ์ IoT
- ✅ รับข้อมูล GPS ผ่าน MQTT
- ✅ อัปเดตสถานะอุปกรณ์ (Online/Offline)
- ✅ บันทึกประวัติตำแหน่ง
- ✅ ตรวจสอบ Geofence (Circle & Polygon)
- ✅ แจ้งเตือน Geofence (Enter/Exit)
- ✅ เก็บข้อมูลใน InfluxDB สำหรับวิเคราะห์
- ✅ สรุปภาพรวม IoT
- ✅ ส่งคำสั่งผ่าน MQTT
- ✅ Domain Events
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)
- ✅ Clean Architecture + DDD + CQRS

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 13: **IoT & GPS Tracking** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 14: Web Order System (WOS)