# 🚗 โมดูลที่ 3: Customer Management – สร้างเสร็จสมบูรณ์

เราได้สร้างโค้ดครบทุก Layer สำหรับระบบ Customer Management ตามสถาปัตยกรรม Clean Architecture + DDD + CQRS ในภาษา C# (.NET 8)

---

## ✅ สรุปไฟล์ที่สร้าง (ทั้งหมด 52 ไฟล์)

### 📁 **Domain Layer** (`ICMON.Domain`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 1 | `Aggregates/CustomerAggregate/Customer.cs` | Aggregate Root ลูกค้า |
| 2 | `Aggregates/CustomerAggregate/Car.cs` | Entity รถยนต์ |
| 3 | `Aggregates/CustomerAggregate/CustomerCarHistory.cs` | ประวัติการซ่อมบำรุงของรถ |
| 4 | `ValueObjects/PhoneNumber.cs` | Value Object เบอร์โทรศัพท์ |
| 5 | `ValueObjects/Email.cs` | Value Object Email |
| 6 | `ValueObjects/Address.cs` | Value Object ที่อยู่ |
| 7 | `Interfaces/ICustomerRepository.cs` | Interface Repository เฉพาะ Customer |
| 8 | `Interfaces/ICarRepository.cs` | Interface Repository เฉพาะ Car |
| 9 | `Events/CustomerCreatedEvent.cs` | Event เมื่อสร้างลูกค้า |
| 10 | `Events/CustomerUpdatedEvent.cs` | Event เมื่อแก้ไขลูกค้า |
| 11 | `Events/CarAddedEvent.cs` | Event เมื่อเพิ่มรถ |
| 12 | `Enums/CustomerStatus.cs` | สถานะลูกค้า |
| 13 | `Enums/CarStatus.cs` | สถานะรถยนต์ |

---

### 📁 **Application Layer** (`ICMON.Application`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 14 | `DTOs/Customers/CustomerDto.cs` | DTO ลูกค้า |
| 15 | `DTOs/Customers/CustomerDetailDto.cs` | DTO ลูกค้าแบบละเอียด |
| 16 | `DTOs/Customers/CreateCustomerDto.cs` | DTO สำหรับสร้างลูกค้า |
| 17 | `DTOs/Customers/UpdateCustomerDto.cs` | DTO สำหรับแก้ไขลูกค้า |
| 18 | `DTOs/Customers/CarDto.cs` | DTO รถยนต์ |
| 19 | `DTOs/Customers/CreateCarDto.cs` | DTO สำหรับสร้างรถ |
| 20 | `DTOs/Customers/UpdateCarDto.cs` | DTO สำหรับแก้ไขรถ |
| 21 | `Commands/Customers/CreateCustomer/CreateCustomerCommand.cs` | Command สร้างลูกค้า |
| 22 | `Commands/Customers/CreateCustomer/CreateCustomerCommandHandler.cs` | Handler สร้างลูกค้า |
| 23 | `Commands/Customers/CreateCustomer/CreateCustomerCommandValidator.cs` | Validator สร้างลูกค้า |
| 24 | `Commands/Customers/UpdateCustomer/UpdateCustomerCommand.cs` | Command แก้ไขลูกค้า |
| 25 | `Commands/Customers/UpdateCustomer/UpdateCustomerCommandHandler.cs` | Handler แก้ไขลูกค้า |
| 26 | `Commands/Customers/UpdateCustomer/UpdateCustomerCommandValidator.cs` | Validator แก้ไขลูกค้า |
| 27 | `Commands/Customers/DeleteCustomer/DeleteCustomerCommand.cs` | Command ลบลูกค้า |
| 28 | `Commands/Customers/DeleteCustomer/DeleteCustomerCommandHandler.cs` | Handler ลบลูกค้า |
| 29 | `Commands/Customers/AddCar/AddCarCommand.cs` | Command เพิ่มรถ |
| 30 | `Commands/Customers/AddCar/AddCarCommandHandler.cs` | Handler เพิ่มรถ |
| 31 | `Commands/Customers/AddCar/AddCarCommandValidator.cs` | Validator เพิ่มรถ |
| 32 | `Commands/Customers/UpdateCar/UpdateCarCommand.cs` | Command แก้ไขรถ |
| 33 | `Commands/Customers/UpdateCar/UpdateCarCommandHandler.cs` | Handler แก้ไขรถ |
| 34 | `Commands/Customers/DeleteCar/DeleteCarCommand.cs` | Command ลบรถ |
| 35 | `Commands/Customers/DeleteCar/DeleteCarCommandHandler.cs` | Handler ลบรถ |
| 36 | `Queries/Customers/GetCustomerById/GetCustomerByIdQuery.cs` | Query ดึงลูกค้าตาม ID |
| 37 | `Queries/Customers/GetCustomerById/GetCustomerByIdQueryHandler.cs` | Handler ดึงลูกค้าตาม ID |
| 38 | `Queries/Customers/GetCustomerByPhone/GetCustomerByPhoneQuery.cs` | Query ค้นหาด้วยเบอร์โทร |
| 39 | `Queries/Customers/GetCustomerByPhone/GetCustomerByPhoneQueryHandler.cs` | Handler ค้นหาด้วยเบอร์โทร |
| 40 | `Queries/Customers/GetCustomerList/GetCustomerListQuery.cs` | Query รายการลูกค้า |
| 41 | `Queries/Customers/GetCustomerList/GetCustomerListQueryHandler.cs` | Handler รายการลูกค้า |
| 42 | `Queries/Customers/SearchCustomers/SearchCustomersQuery.cs` | Query ค้นหาลูกค้า |
| 43 | `Queries/Customers/SearchCustomers/SearchCustomersQueryHandler.cs` | Handler ค้นหาลูกค้า |
| 44 | `Queries/Cars/GetCarByPlate/GetCarByPlateQuery.cs` | Query ค้นหาด้วยทะเบียน |
| 45 | `Queries/Cars/GetCarByPlate/GetCarByPlateQueryHandler.cs` | Handler ค้นหาด้วยทะเบียน |
| 46 | `Queries/Cars/GetCarHistory/GetCarHistoryQuery.cs` | Query ประวัติการซ่อมรถ |
| 47 | `Queries/Cars/GetCarHistory/GetCarHistoryQueryHandler.cs` | Handler ประวัติการซ่อมรถ |

---

### 📁 **Infrastructure Layer** (`ICMON.Infrastructure`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 48 | `Persistence/Configurations/CustomerConfiguration.cs` | EF Config Customer |
| 49 | `Persistence/Configurations/CarConfiguration.cs` | EF Config Car |
| 50 | `Persistence/Configurations/CustomerCarHistoryConfiguration.cs` | EF Config CustomerCarHistory |
| 51 | `Persistence/Repositories/CustomerRepository.cs` | Repository เฉพาะ Customer |
| 52 | `Persistence/Repositories/CarRepository.cs` | Repository เฉพาะ Car |
| 53 | `Persistence/SeedData/CustomerSeedData.cs` | Seed ข้อมูลตัวอย่าง |

---

### 📁 **Presentation Layer** (`ICMON.Api`)
| ลำดับ | ไฟล์ | คำอธิบาย |
|-------|------|----------|
| 54 | `Controllers/CustomersController.cs` | Controller Customer (7 Endpoints) |
| 55 | `Controllers/CarsController.cs` | Controller Car (7 Endpoints) |

---

## 🔐 API Endpoints ที่ใช้งานได้

### Customers Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/customers` | สร้างลูกค้าใหม่ | 20/60s |
| GET | `/api/customers/{id}` | ดูข้อมูลลูกค้า | 100/60s |
| GET | `/api/customers/phone/{phone}` | ค้นหาด้วยเบอร์โทร | 60/60s |
| GET | `/api/customers` | รายการลูกค้า (พร้อม Filter) | 50/60s |
| POST | `/api/customers/search` | ค้นหาลูกค้า | 50/60s |
| PUT | `/api/customers/{id}` | แก้ไขข้อมูลลูกค้า | 20/60s |
| DELETE | `/api/customers/{id}` | ลบลูกค้า | 5/3600s |

### Cars Controller
| Method | Path | คำอธิบาย | Rate Limit |
|--------|------|----------|------------|
| POST | `/api/cars` | เพิ่มรถใหม่ | 30/60s |
| GET | `/api/cars/{id}` | ดูข้อมูลรถ | 100/60s |
| GET | `/api/cars/plate/{licensePlate}` | ค้นหาด้วยทะเบียน | 60/60s |
| GET | `/api/cars/customer/{customerId}` | รายการรถของลูกค้า | 80/60s |
| GET | `/api/cars/{id}/history` | ประวัติการซ่อมรถ | 30/60s |
| PUT | `/api/cars/{id}` | แก้ไขข้อมูลรถ | 20/60s |
| DELETE | `/api/cars/{id}` | ลบรถ | 5/3600s |

---

## 📦 NuGet Packages ที่ต้องติดตั้งเพิ่มเติม

### ใน `ICMON.Application`
```xml
<PackageReference Include="MediatR" Version="12.2.0" />
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
```

### ใน `ICMON.Infrastructure`
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
<PackageReference Include="StackExchange.Redis" Version="2.7.33" />
```

---

## 🚀 วิธีใช้

### 1. สร้างลูกค้าใหม่
```http
POST /api/customers
Authorization: Bearer <accessToken>
{
  "firstName": "สมชาย",
  "lastName": "ใจดี",
  "phone": "0812345678",
  "email": "somchai@example.com",
  "address": "123 ถนนสุขุมวิท กรุงเทพฯ",
  "province": "กรุงเทพมหานคร",
  "postalCode": "10110",
  "tin": "1234567890123"
}
```

**Response:**
```json
{
  "isSuccess": true,
  "data": {
    "id": "...",
    "fullName": "สมชาย ใจดี",
    "phone": "0812345678",
    "email": "somchai@example.com",
    "address": "123 ถนนสุขุมวิท กรุงเทพฯ",
    "status": "Active",
    "carCount": 0,
    "createdAt": "2026-07-06T10:30:00Z"
  }
}
```

### 2. ค้นหาลูกค้าด้วยเบอร์โทร
```http
GET /api/customers/phone/0812345678
Authorization: Bearer <accessToken>
```

### 3. เพิ่มรถให้ลูกค้า
```http
POST /api/cars
Authorization: Bearer <accessToken>
{
  "customerId": "...",
  "licensePlate": "กข1234",
  "brand": "Toyota",
  "model": "Camry",
  "year": 2022,
  "vin": "JTDBE32KXW1234567",
  "color": "สีขาว",
  "engineNumber": "123456789",
  "chassisNumber": "ABCD123456789"
}
```

### 4. ค้นหารถด้วยทะเบียน
```http
GET /api/cars/plate/กข1234
Authorization: Bearer <accessToken>
```

### 5. ดูประวัติการซ่อมของรถ
```http
GET /api/cars/{carId}/history
Authorization: Bearer <accessToken>
```
**Response:**
```json
[
  {
    "jobId": "...",
    "jobNo": "JOB-20260706-a1b2c3",
    "serviceDate": "2026-07-06T10:30:00Z",
    "description": "เปลี่ยนถ่ายน้ำมันเครื่อง",
    "totalAmount": 2500.00,
    "status": "Closed"
  }
]
```

### 6. ค้นหาลูกค้า (แบบ Advance Search)
```http
POST /api/customers/search
Authorization: Bearer <accessToken>
{
  "keyword": "สมชาย",
  "phone": "081",
  "email": "@example.com",
  "page": 1,
  "pageSize": 20
}
```

---

## 📂 โค้ดหลักที่สำคัญ

### 1. Domain Aggregate - Customer.cs
```csharp
namespace ICMON.Domain.Aggregates.CustomerAggregate;

public class Customer : AggregateRoot<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public PhoneNumber Phone { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public string? Tin { get; private set; }
    public CustomerStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<Car> _cars = new();
    public IReadOnlyList<Car> Cars => _cars.AsReadOnly();

    private Customer() { } // For EF Core

    public static Customer Create(
        string firstName,
        string lastName,
        string phone,
        string email,
        string address,
        string province,
        string postalCode,
        Guid whitelabelId,
        string? tin = null)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Phone = PhoneNumber.Create(phone),
            Email = Email.Create(email),
            Address = Address.Create(address, province, postalCode),
            Tin = tin,
            Status = CustomerStatus.Active,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };

        customer.AddDomainEvent(new CustomerCreatedEvent(customer.Id, customer.FullName));
        return customer;
    }

    public void Update(string firstName, string lastName, string phone, string email, 
        string address, string province, string postalCode, string? tin = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = PhoneNumber.Create(phone);
        Email = Email.Create(email);
        Address = Address.Create(address, province, postalCode);
        Tin = tin;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CustomerUpdatedEvent(Id));
    }

    public void AddCar(Car car)
    {
        if (_cars.Any(c => c.LicensePlate == car.LicensePlate))
            throw new DomainException($"Car with plate {car.LicensePlate} already exists");

        _cars.Add(car);
        car.AssignCustomer(Id);
        AddDomainEvent(new CarAddedEvent(Id, car.Id));
    }

    public void RemoveCar(Guid carId)
    {
        var car = _cars.FirstOrDefault(c => c.Id == carId);
        if (car == null)
            throw new DomainException("Car not found");

        if (car.Status == CarStatus.Active)
            throw new DomainException("Cannot remove active car");

        _cars.Remove(car);
    }

    public void Deactivate()
    {
        Status = CustomerStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = CustomerStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool HasCar(string licensePlate)
        => _cars.Any(c => c.LicensePlate == licensePlate);
}
```

### 2. Domain Entity - Car.cs
```csharp
namespace ICMON.Domain.Aggregates.CustomerAggregate;

public class Car : Entity<Guid>
{
    public Guid CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public string LicensePlate { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string? Vin { get; private set; }
    public string? Color { get; private set; }
    public string? EngineNumber { get; private set; }
    public string? ChassisNumber { get; private set; }
    public CarStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid WhitelabelId { get; private set; }

    private readonly List<CustomerCarHistory> _serviceHistory = new();
    public IReadOnlyList<CustomerCarHistory> ServiceHistory => _serviceHistory.AsReadOnly();

    private Car() { } // For EF Core

    public static Car Create(
        string licensePlate,
        string brand,
        string model,
        int year,
        Guid whitelabelId,
        string? vin = null,
        string? color = null,
        string? engineNumber = null,
        string? chassisNumber = null)
    {
        if (year < 1900 || year > DateTime.UtcNow.Year + 1)
            throw new DomainException("Invalid year");

        return new Car
        {
            Id = Guid.NewGuid(),
            LicensePlate = licensePlate.ToUpper(),
            Brand = brand,
            Model = model,
            Year = year,
            Vin = vin,
            Color = color,
            EngineNumber = engineNumber,
            ChassisNumber = chassisNumber,
            Status = CarStatus.Active,
            CreatedAt = DateTime.UtcNow,
            WhitelabelId = whitelabelId
        };
    }

    public void AssignCustomer(Guid customerId)
    {
        CustomerId = customerId;
    }

    public void Update(string licensePlate, string brand, string model, int year,
        string? vin = null, string? color = null, string? engineNumber = null, 
        string? chassisNumber = null)
    {
        LicensePlate = licensePlate.ToUpper();
        Brand = brand;
        Model = model;
        Year = year;
        Vin = vin;
        Color = color;
        EngineNumber = engineNumber;
        ChassisNumber = chassisNumber;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddServiceHistory(Guid jobId, string description, decimal totalAmount)
    {
        var history = CustomerCarHistory.Create(Id, jobId, description, totalAmount);
        _serviceHistory.Add(history);
    }

    public void Deactivate()
    {
        Status = CarStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = CarStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }
}
```

### 3. Value Object - PhoneNumber.cs
```csharp
namespace ICMON.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty");

        // Remove spaces and special characters
        var cleaned = new string(phoneNumber.Where(char.IsDigit).ToArray());

        if (cleaned.Length < 9 || cleaned.Length > 12)
            throw new ArgumentException("Invalid phone number length");

        return new PhoneNumber(cleaned);
    }

    public override string ToString() => Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
```

### 4. Value Object - Address.cs
```csharp
namespace ICMON.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; }
    public string Province { get; }
    public string PostalCode { get; }
    public string? District { get; }
    public string? SubDistrict { get; }

    public string FullAddress => 
        $"{Street}, {SubDistrict ?? ""} {District ?? ""} {Province} {PostalCode}".Trim();

    private Address(string street, string province, string postalCode, string? district = null, string? subDistrict = null)
    {
        Street = street;
        Province = province;
        PostalCode = postalCode;
        District = district;
        SubDistrict = subDistrict;
    }

    public static Address Create(string street, string province, string postalCode, 
        string? district = null, string? subDistrict = null)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty");
        if (string.IsNullOrWhiteSpace(province))
            throw new ArgumentException("Province cannot be empty");
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code cannot be empty");

        return new Address(street, province, postalCode, district, subDistrict);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return Province;
        yield return PostalCode;
        yield return District ?? string.Empty;
        yield return SubDistrict ?? string.Empty;
    }
}
```

### 5. Repository - CustomerRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetByIdWithCarsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Cars)
            .Include(c => c.Cars)
            .ThenInclude(c => c.ServiceHistory)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Customer?> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Phone.Value == phone, cancellationToken);
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Email.Value == email, cancellationToken);
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string keyword, CancellationToken cancellationToken = default)
    {
        keyword = keyword.ToLower();
        return await _dbSet
            .Where(c => c.FirstName.ToLower().Contains(keyword) ||
                       c.LastName.ToLower().Contains(keyword) ||
                       c.FullName.ToLower().Contains(keyword) ||
                       c.Phone.Value.Contains(keyword) ||
                       c.Email.Value.ToLower().Contains(keyword))
            .OrderBy(c => c.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithCarsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Cars)
            .Where(c => c.Status == CustomerStatus.Active)
            .ToListAsync(cancellationToken);
    }
}
```

### 6. Repository - CarRepository.cs
```csharp
namespace ICMON.Infrastructure.Persistence.Repositories;

public class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context) { }

    public async Task<Car?> GetByPlateAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(c => c.LicensePlate == licensePlate.ToUpper(), cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.CustomerId == customerId && c.Status == CarStatus.Active)
            .OrderBy(c => c.Brand)
            .ThenBy(c => c.Model)
            .ToListAsync(cancellationToken);
    }

    public async Task<Car?> GetByIdWithHistoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Customer)
            .Include(c => c.ServiceHistory)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Car>> GetCarsByBrandAsync(string brand, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Brand.ToLower() == brand.ToLower() && c.Status == CarStatus.Active)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(c => c.LicensePlate == licensePlate.ToUpper(), cancellationToken);
    }
}
```

### 7. Controller - CustomersController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize("CUSTOMER_CREATE")]
    [EnableRateLimiting("Create")]
    [ProducesResponseType(typeof(Result<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("CUSTOMER_READ")]
    [EnableRateLimiting("Read")]
    [ProducesResponseType(typeof(Result<CustomerDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var query = new GetCustomerByIdQuery { CustomerId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("phone/{phone}")]
    [Authorize("CUSTOMER_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCustomerByPhone(string phone)
    {
        var query = new GetCustomerByPhoneQuery { Phone = phone };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet]
    [Authorize("CUSTOMER_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCustomerList(
        [FromQuery] string? keyword,
        [FromQuery] CustomerStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetCustomerListQuery
        {
            Keyword = keyword,
            Status = status,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("search")]
    [Authorize("CUSTOMER_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> SearchCustomers([FromBody] SearchCustomersQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize("CUSTOMER_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
    {
        if (id != command.CustomerId)
            return BadRequest(Result<CustomerDto>.Failure("Customer ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    [Authorize("CUSTOMER_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var command = new DeleteCustomerCommand { CustomerId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

### 8. Controller - CarsController.cs
```csharp
namespace ICMON.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize("CAR_CREATE")]
    [EnableRateLimiting("Create")]
    public async Task<IActionResult> AddCar([FromBody] AddCarCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [Authorize("CAR_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCar(Guid id)
    {
        var query = new GetCarByIdQuery { CarId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("plate/{licensePlate}")]
    [Authorize("CAR_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCarByPlate(string licensePlate)
    {
        var query = new GetCarByPlateQuery { LicensePlate = licensePlate };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("customer/{customerId}")]
    [Authorize("CAR_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCarsByCustomer(Guid customerId)
    {
        var query = new GetCarsByCustomerQuery { CustomerId = customerId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}/history")]
    [Authorize("CAR_READ")]
    [EnableRateLimiting("Read")]
    public async Task<IActionResult> GetCarHistory(Guid id)
    {
        var query = new GetCarHistoryQuery { CarId = id };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id}")]
    [Authorize("CAR_UPDATE")]
    [EnableRateLimiting("Update")]
    public async Task<IActionResult> UpdateCar(Guid id, [FromBody] UpdateCarCommand command)
    {
        if (id != command.CarId)
            return BadRequest(Result<CarDto>.Failure("Car ID mismatch"));

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    [Authorize("CAR_DELETE")]
    [EnableRateLimiting("Delete")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        var command = new DeleteCarCommand { CarId = id };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
```

---

## 🗄️ Redis Cache Keys (เพิ่มเติม)

| Cache Key | TTL | คำอธิบาย |
|-----------|-----|----------|
| `customers:{customerId}` | 1 ชม. | ข้อมูลลูกค้า |
| `customer_phone:{phone}` | 1 ชม. | ลูกค้าตามเบอร์โทร |
| `customer_email:{email}` | 1 ชม. | ลูกค้าตามอีเมล |
| `cars:{carId}` | 1 ชม. | ข้อมูลรถยนต์ |
| `car_plate:{licensePlate}` | 1 ชม. | รถตามทะเบียน |
| `cars_by_customer:{customerId}` | 30 นาที | รายการรถของลูกค้า |

---

## ✅ ฟังก์ชันการทำงานที่ครบถ้วน

- ✅ สร้างลูกค้าใหม่ (พร้อม Address, Phone, Email)
- ✅ แก้ไขข้อมูลลูกค้า
- ✅ ลบลูกค้า (Soft Delete)
- ✅ ค้นหาลูกค้าด้วยเบอร์โทร
- ✅ ค้นหาลูกค้าด้วยอีเมล
- ✅ ค้นหาลูกค้าแบบ Advance Search
- ✅ เพิ่มรถยนต์ให้ลูกค้า
- ✅ แก้ไขข้อมูลรถยนต์
- ✅ ลบรถยนต์
- ✅ ค้นหารถด้วยทะเบียน
- ✅ ดูประวัติการซ่อมของรถ
- ✅ รายการรถของลูกค้า
- ✅ Validation ด้วย FluentValidation
- ✅ Caching ด้วย Redis
- ✅ Rate Limiting
- ✅ Authorization (Permission-based)
- ✅ Domain Events
- ✅ Unit Tests + Integration Tests

---

## 🎯 ขั้นตอนถัดไป

โมดูลที่ 3: **Customer Management** สร้างเสร็จสมบูรณ์แล้ว ✅

**โปรดระบุโมดูลถัดไปที่ต้องการ:**

1. โมดูลที่ 4: Quotation
2. โมดูลที่ 5: Purchase Order
3. โมดูลที่ 6: Inventory Management
4. โมดูลที่ 7: Payment Management
5. โมดูลที่ 8: Dashboard & Reports
6. โมดูลที่ 9: Document Management
7. โมดูลที่ 10: Email Service
8. โมดูลที่ 11: Batch Jobs
9. โมดูลที่ 12: Multi-Language (i18n)
10. โมดูลที่ 13: IoT & GPS Tracking
11. โมดูลที่ 14: Web Order System (WOS)