<h1> Clean Architecture and Domain Driven Design Template </h1>

This is a template for creating your own application using **Clean Architecture**, utilizing **building blocks from Domain-Driven Design**. Read and write operations have been separated according to the **CQRS** pattern. 
System observability is ensured by the implementation of OpenTelemetry and the Aspire Dashboard.
Additionally, you'll find implementations of design patterns such as mediator, factory, strategy, and several others.

The implementation of business domain logic is kept to a minimum. Selected use cases were implemented to demonstrate communication between layers and the use of domain and integration events.

This project is undergoing rapid development, with new features being added frequently. 
Stay updated and click Watch button, click ⭐ if you find it useful.

## Table of contents

- [Table of contents](#table-of-contents)
- [1. Installation](#1-installation)
  - [1.1 Solution Template Installation](#11-solution-template-installation)
  - [1.2 Database](#12-database)
  - [1.3 Docker](#13-docker)
  - [1.4 Database migrations](#14-database-migrations)
- [2. Introduction](#2-introduction)
  - [2.1 Motivation](#21-motivation)
- [3. Domain](#3-domain)
  - [3.1 Introduction](#31-introduction)
  - [3.2 Aggregates](#32-aggregates)
  - [3.3 Policies](#33-policies)
  - [3.4 Domain services](#34-domain-services)
  - [3.5 Domain Events](#35-domain-events)
- [4. Architecture](#4-architecture)
  - [4.1 Clean Architecure](#41-clean-architecure)
    - [4.1.1 Presentation Layer](#411-presentation-layer)
    - [4.1.2 Infrastructure Layer](#412-infrastructure-layer)
    - [4.1.3 Application Layer](#413-application-layer)
    - [4.1.4 Domain Layer](#414-domain-layer)
  - [4.2 Eventual consistency](#42-eventual-consistency)
  - [4.2.1 Domain Events](#421-domain-events)
  - [4.2.2 Integration Events](#422-integration-events)
  - [4.3 Command Query Responsibility Segregation (CQRS)](#43-command-query-responsibility-segregation-cqrs)
  - [4.4 Cross Cutting Concerns](#44-cross-cutting-concerns)
- [4.5 Caching](#45-caching)
  - [4.5.1 Cache Aside pattern](#451-cache-aside-pattern)
  - [4.5.2 Cache invalidation](#452-cache-invalidation)
- [5. Authentication](#5-authentication)
  - [5.1 Keycloak](#51-keycloak)
  - [5.2 Communication with API](#52-communication-with-api)
  - [5.3 Exporting KeyCloak realm](#53-exporting-keycloak-realm)
- [6. Observability](#6-observability)
  - [6.1 Open Telemetry](#61-open-telemetry)
- [7 Design patterns implemented in this project](#7-design-patterns-implemented-in-this-project)
  - [7.1 Mediator](#71-mediator)
  - [7.2 Factory method](#72-factory-method)
  - [7.3 Strategy](#73-strategy)
- [8. Tests](#8-tests)
  - [8.1 Domain tests](#81-domain-tests)
  - [8.2 Application tests](#82-application-tests)
- [9. Build with](#9-build-with)
- [1. บทนำและภาพรวมระบบ](#1-บทนำและภาพรวมระบบ)
  - [1.1 วัตถุประสงค์ (เหมือนเดิม)](#11-วัตถุประสงค์-เหมือนเดิม)
  - [1.2 เทคโนโลยีหลัก (Tech Stack – .NET Edition)](#12-เทคโนโลยีหลัก-tech-stack--net-edition)
- [2. สถาปัตยกรรมระบบ (Clean Architecture + DDD)](#2-สถาปัตยกรรมระบบ-clean-architecture--ddd)
  - [2.1 แผนภาพสถาปัตยกรรม (ปรับเป็น .NET)](#21-แผนภาพสถาปัตยกรรม-ปรับเป็น-net)
  - [2.2 หลักการออกแบบ (เหมือนเดิม)](#22-หลักการออกแบบ-เหมือนเดิม)
- [3. ตัวอย่างโครงสร้างโปรเจกต์ (Solution Structure)](#3-ตัวอย่างโครงสร้างโปรเจกต์-solution-structure)


## 1. Installation
I do recommend using CLI instead Visual Studnio Wizard when creating your application based on this template because Vistual Studio doesn't keep folders structure correctly.

### 1.1 Solution Template Installation
First and foremost, install this template on your system to be able to create applications based on it.
```
dotnet new install CA.And.DDD.Template
```
Once installed type. (Replace the phrase 'MyDreamProject' with the name of your project.)
```
dotnet new ca-and-ddd -o MyDreamProject
```

### 1.2 Database

The template uses PostgreSQL as a database provider. Migrations will be applied automatically during project startup, so you don't have to do anything.

### 1.3 Docker

As a result of running command from step 1.1 all files and folders will be created. Among them you will find docker-compose.yaml.
Simply run the command 'docker-compose up' to create required containers.
docker-compose.yaml provides instances of: PostgreSQL, Redis, RabbitMQ, and Aspire Dashboard. 

### 1.4 Database migrations

In case you need to add a new migration, just navigate to the src/ directory and run. 
(Please remember to replace "Clean.Architecture.And.DDD.Template" with you project name).

```
dotnet ef migrations add "Migration name" --context "AppDbContext" --project .\CA.And.DDD.Template.Infrastructure\ --startup-project .\CA.And.DDD.Template.WebApi\
```

## 2. Introduction

### 2.1 Motivation

I couldn't find any repository that met the following criteria:

1. Implemented Mediator pattern **using MassTransit library instead of MediatR** with added message interception support (similar to IPipelineBehaviour in MediatR).
2. Implemented system observability using Open Telemetry.
3. Implemented Domain Events as part of Eventual Consistency, so Domain Events are not published in the same transaction as saving/updating Aggregate.

Even when encountering projects that fulfilled one of these points, they often conflicted with others or omitted them entirely. Therefore, I decided to create a project that meets the above criteria. I am sharing it in case someone else is looking for something similar.

## 3. Domain

### 3.1 Introduction
The e-commerce domain was deliberately chosen because it is widely known and understood. This template consists of two aggregates: Customer and Order.
I decided to extend the domain just enough to utilize all the building blocks, but nothing more. The goal of this repository is to create a template that provides an example implementation.
Keeping things simple in the Domain layer allows you to focus on understanding complex topics, such as the building blocks of the Domain-Driven Design approach.

### 3.2 Aggregates

There are two aggregates in the Domain Layer. These are:

**Customer** – This is the most important aggregate in our layer because it is responsible for placing orders. In other words, this is the entity that starts the process of placing orders. Besides placing orders, the customer can also change their e-mail address and verify it.

**Order** – This represents a particular order. It has a list of order items, along with their price, quantity, etc.

### 3.3 Policies

The Policy pattern allows encapsulating domain logic into a separate class. This is very useful for testing and modifying it.
In our case, there is a policy called AmountBasedDiscountPolicy, which is responsible for applying a discount to an order.
As you can see below, the discount amount depends on how much the user spent last month, if not on the value of their current order.

<details>
  <summary><b>Code</b></summary>
  <p>

```csharp
    public class AmountBasedDiscountPolicy
    {
        public decimal CalculateDiscount(IReadOnlyCollection<OrderItem> orderItems)
        {
            var currentTotalAmount = orderItems.Sum(x => x.Quantity * x.Price.Amount);
            if (currentTotalAmount > 800)
            {
                return 0.05m;
            }
            else if (currentTotalAmount > 600)
            {
                return 0.025m;
            }
            else
            {
                return 0.00m;
            }
        }
    }
```
</p>
</details>


### 3.4 Domain services

Domain services are building blocks that help encapsulate business logic that does not belong to any specific entity or aggregate.

In our example, we use it to calculate a discount for the order. We retrieve the total amount of money spent in the last month and pass it to the domain service named OrderDomainService. There, we calculate the discount using the TotalSpentMoneyInLast31DaysDiscountPolicy class.

Whenever possible, you should avoid creating domain services. Relying on them shifts the business logic from aggregates to these services, making the aggregates more anemic.

### 3.5 Domain Events

Domain events allow informing other parts of the application about changes that have occurred within our domain. They are an excellent way to implement business processes in a loosely coupled manner. Each domain event represents a specific action adhering to the [(Single Responsibility Principle, SRP)](https://en.wikipedia.org/wiki/Single-responsibility_principle) that has happened in the system; therefore, event names are represented in the past tense, e.g., OrderCreatedDomainEvent. When a domain event is published, all interested parties can react to it. As the number of "interested parties" for a given domain event grows, all that needs to be done is to create an additional handler to perform extra logic. This approach also aligns with the [Open/Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle).

In our solution, domain events are stored in the database within the same transaction as the aggregate's save operation. This means they are not dispatched immediately but only after the aggregate has been successfully saved. Once this happens, the DomainEventsProcessor retrieves the events from the database and publishes them.

For example: When a Customer places an order, an event called OrderCreatedDomainEvent is stored in the database and then retrieved and published by the DomainEventsProcessor. An event handler, OrderCreatedDomainEventHandler, listens for this event and handles sending an email to the customer who placed the order. Implementing the business process in this way allows for leveraging the eventual consistency approach.

The diagram above shows all implemented Domain Events and their respective handlers.
![](docs/images/Domain-Events-Flow.jpg)


Dispatching domain events within the same transaction or outside of it is a sticking point in the community. You can find various implementations on the web.

Outside transaction: [ardalis/CleanArchitecture](https://github.com/ardalis/CleanArchitecture/blob/main/src/Clean.Architecture.Infrastructure/Data/AppDbContext.cs)

During transaction: [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/Infrastructure/Data/Interceptors/DispatchDomainEventsInterceptor.cs)


## 4. Architecture

### 4.1 Clean Architecure

By applying the Clean Architecture approach, we achieved a separation of the application into layers that are independent of each other, making them easy to test. The project consists of four layers, as shown in the image below.

![](docs/images/Clean-Architecture-View.png)

#### 4.1.1 Presentation Layer

The presentation layer (in our case, simply a [RESTful API](https://en.wikipedia.org/wiki/REST)) is responsible for interacting with users by receiving and processing HTTP requests. It performs validation of commands and queries, and then communicates directly with the application layer to handle the request.

#### 4.1.2 Infrastructure Layer

The Infrastructure layer is responsible for implementing technical aspects such as database access and broad integrations with external systems. This layer is also responsible for implementing interfaces defined in the domain and application layers.
The Infrastructure layer references the Application layer. 

#### 4.1.3 Application Layer

This layer orchestrates processes in the application. The Application Layer only references the Domain Layer. Most of the application logic resides in command handlers, where you can implement specific scenarios (use cases).

#### 4.1.4 Domain Layer

This is the most important layer, as it contains business logic and implements business processes. It consists of aggregates, value objects, domain services, and other domain-related elements. This layer is the heart of the whole system. The domain layer is completely independent; it does not reference other layers and does not depend on any external libraries, making it very easy to test and maintain.

### 4.2 Eventual consistency

### 4.2.1 Domain Events
Saving and publishing domain events only after the aggregate has been saved means that we must use the approach known as eventual consistency.

In our case:
When a Customer places an order, an event called **OrderCreatedDomainEvent** is stored in the database and then retrieved and published by the **DomainEventsProcessor**. An event handler, **OrderCreatedDomainEventHandler**, listens for this event and handles sending an email to the customer who placed the order.

If we decided to publish the **OrderCreatedDomainEvent** just before saving the aggregate to the database, there could be a situation where we send an email to the customer, but the save operation fails, for example, due to network issues.

### 4.2.2 Integration Events

Integration events are used to notify other modules or systems about important events that occurred in our system. Through these events, we can achieve consistency at the level of components or microservices by synchronizing data. In our solution, domain events are mapped to integration events and are only published after the aggregate has been successfully saved. The IntegrationEventsProcessor is responsible for fetching and publishing integration events from the database.

In our solution, we have only one handler, which was included purely for demonstration purposes. The CustomerCreatedIntegrationEvent is sent to a RabbitMq queue. http://localhost:15672/ login: guest, password: guest.

```csharp
  public class CustomerCreatedIntegrationEventHandler : IConsumer<CustomerCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedIntegrationEvent> context)
        {
            //This handler is being triggered as a result of mapping CustomerCreatedDomainEvent to CustomerCreatedIntegrationEvent.
            //Integration events are the way to notify other modules / microservices about changes in our domain.
            //This particular integration event is sent to RabbitMQ by IntegrationEventsProcessor and received here.
            //This handler should never have been placed here; it should be placed in another module or microservice.
            //However, I decided to leave that implementation here just to demonstrate how to register this handler
            //in the IoC container and generally how to use it if needed in the future.

            return Task.CompletedTask;
        }
    }
````

This handler should be placed in a separate module or another microservice.

### 4.3 Command Query Responsibility Segregation (CQRS)

Implementation of CQRS involves separating write and read requests. Commands are responsible for changing the state of aggregates (saving and updating them), and such operations are available only through specific repositories, e.g., ICustomerRepository.

```csharp
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> GetAsync(string email, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    }
```

Queries are responsible for retrieving entities from the database. Queries are placed in the infrastructure layer to directly utilize the DbContext and avoid creating unnecessary abstractions.

Please take a look at the example: ***GetCustomerQueryHandler***

```csharp
        public GetCustomerQueryHandler(ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _cacheService = cacheService;
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// This handler demonstrates the usage of the Cache Aside Pattern.
        /// First, we check if the data is available in the cache (Redis). If not,
        /// we retrieve the data from the database and store it in the cache.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetCustomerQuery> query)
        {
            var cachedCustomerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(query.Message.Email));
            if (cachedCustomerDto is { })
            {
                await query.RespondAsync(cachedCustomerDto);
                return;
            }

            var email = query.Message.Email;
            var customer = (await _customerRepository.GetAsync(email))!.ToDto();

            await _cacheService.SetAsync(CacheKeyBuilder.GetCustomerKey(query.Message.Email), customer);
            await query.RespondAsync(customer);
        }
```

### 4.4 Cross Cutting Concerns

Cross-cutting concerns are implemented using MassTransit filters. This is a very convenient and elegant approach to achieving these kinds of tasks.
MassTransit filters follow the same concept as IPipelineBehavior<TRequest, TResponse> in the MediatR library.
There are several filters implemented in our code:

```csharp
                cfg.ConfigureMediator((context, cfg) =>
                {
                    cfg.UseConsumeFilter(typeof(ValidationFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(LoggingFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(RedisFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(EventsFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));
                    cfg.UseConsumeFilter(typeof(HtmlSanitizerFilter<>), context, x => x.Include(type => !type.HasInterface<IDomainEvent>()));

                });
```
**ValidationFilter** – is responsible for performing validation of commands and queries sent by the user. The validations are done using the FluentValidation library.
If a validation error occurs, processing is interrupted, and a response is sent to the user with details about what went wrong.

<details>
  <summary><b>Code</b></summary>
  <p>

```csharp
    public class ValidationFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public ValidationFilter(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var _context = new ValidationContext<T>(context.Message);

            var validationFailures = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context.Message)));


            if (validationFailures.Any(x => x.Errors.Any()))
            {
                var groupedErrors = validationFailures.SelectMany(x => x.Errors).GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(a => a.ErrorMessage).ToArray());

                throw new CommandValidationException(String.Empty, groupedErrors);
            }

            await next.Send(context);

        }

        public void Probe(ProbeContext context) { }

    }
```
</p>
</details>

**LoggingFilter** - is responsible for logging requests along with their total duration and payload. The current implementation logs all requests; however, you could, for example, detect only long-running requests and log them.

<details>
  <summary><b>Code</b></summary>
  <p>

```csharp
    public class LoggingFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly ILogger<LoggingFilter<T>> _logger;

        public LoggingFilter(ILogger<LoggingFilter<T>> logger)
        {
            _logger = logger;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await next.Send(context);
            }
            finally
            {
                stopwatch.Stop();

            }
            _logger.LogTrace($"Operation duration: {stopwatch.Elapsed.TotalMilliseconds} ms", context);

        }


        public void Probe(ProbeContext context) { }
    }
```
</p>
</details>
**RedisFilter** - is responsible for counting all requests per day. This is just an example implementation that uses Redis. You could implement other logic here, such as caching, checking permissions, etc.

**EventsFilter** - is responsible for saving Domain Events and Integration Events to the database.

**HtmlSanitizerFilter** - is responsible for cleaning HTML that can lead to XSS attacks.

## 4.5 Caching
### 4.5.1 Cache Aside pattern


Caching was provided through the implementation of the Cache Aside Pattern. It is a simple pattern that can be described in three steps:

    1. When the application requests data, it first checks if they are available in the cache.
    2. If they are available, they are returned.
    3. If they are not available, the data is retrieved from the database, stored in the cache, and then returned.

In our case, the implementation looks like this:

<details>
  <summary><b>Code</b></summary>
  <p>

```csharp
        /// <summary>
        /// This handler demonstrates the usage of the Cache Aside Pattern.
        /// First, we check if the data is available in the cache (Redis). If not,
        /// we retrieve the data from the database and store it in the cache.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="OrderNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetOrderQuery> query)
        {
            var cachedOder = await _cacheService.GetAsync<OrderDto>(CacheKeyBuilder.GetOrderKey(query.Message.Id));
            if (cachedOder is { })
            {
                await query.RespondAsync(cachedOder);
            }

            var id = query.Message.Id;
            var order = await _appDbContext
                .Set<Order>()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => ((Guid)x.OrderId) == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderNotFoundApplicationException(id);
            }

            await _cacheService.SetAsync(CacheKeyBuilder.GetOrderKey(query.Message.Id), order);
            await query.RespondAsync(
                new OrderDto(
                    order.OrderId.Value,
                    order.OrderItems.MapToOrderItemDto()
                    )
                );
        }
```
</details>

### 4.5.2 Cache invalidation

Cache invalidation can be implemented in various ways. I decided to invalidate the cache as a result of processing commands, because they produce domain events.
When a customer changes their email, an event called  **CustomerEmailChangedDomainEvent** is emitted, and we can react to it by invalidating the cache.

<details>
  <summary><b>Code</b></summary>
  <p>
     
```csharp
    public class CustomerEmailChangedDomainEventHandler : IConsumer<CustomerEmailChangedDomainEvent>
    {
        private readonly ICacheService _cacheService;

        public CustomerEmailChangedDomainEventHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<CustomerEmailChangedDomainEvent> context)
        {
            //Here, you could send an emails to old and new e-email addresses
            //informing about the correct change of the email address.

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

            var customerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(context.Message.OldEmailAddress));
            if(customerDto is { })
            {
                await _cacheService.RemoveAsync(CacheKeyBuilder.GetCustomerKey(context.Message.OldEmailAddress));
            }
        }
    }
```
  </p>
</details>

## 5. Authentication
### 5.1 Keycloak
Authentication and authorization have been implemented using the Resource Owner Password Flow in Keycloak. All necessary components, such as users, roles, groups, clients, and scopes, will be automatically imported
through a volume in docker-compose, so you do not have to do anything extra.

If you are interested in how it is configured, please visit:

Keycloak: http://localhost:8080/

Username: admin

Password: admin

### 5.2 Communication with API
In order to interact with the API, the user must authenticate themselves against the UserController/Login endpoint. This endpoint forwards user credentials to Keycloak and returns an Access Token and a Refresh Token to the user.

There are two accounts created in Keycloak:

1. Username: admin, Password: admin

2. Username: user, Password: user

In simple terms, all you have to do is authenticate against UserController/Login, grab the JWT token, and pass it to other HTTP requests using the Authorization header.

However, if you prefer to authenticate against Keycloak instead, you can send the following request:

```sh
curl --location 'http://localhost:8080/realms/e-commerce-realm/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=password' \
--data-urlencode 'client_id=e-commerce-api-resource-owner-client' \
--data-urlencode 'username=admin' \
--data-urlencode 'password=admin' \
--data-urlencode 'scope=profile email offline_access e-commerce-scope'
```

and to refresh the token send:

```sh
curl --location 'http://localhost:8080/realms/e-commerce-realm/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'grant_type=refresh_token' \
--data-urlencode 'client_id=e-commerce-api-resource-owner-client' \
--data-urlencode 'refresh_token=	PLACE 	HERE	 YOUR 	REFRESH 	TOKEN 	HERE'
```

### 5.3 Exporting KeyCloak realm
If you wish to persist realm-related changes in source control, as configured in this repository, you have to export these settings using the commands below:

```sh
docker exec -it keycloak_web bash
cd /opt/keycloak/bin/
./kc.sh export --dir /tmp/export --users different_files
docker cp keycloak-web:/tmp/export ./keycloak-export
```

## 6. Observability
### 6.1 Open Telemetry
The observability of the system has been ensured through the use of OpenTelemetry. Telemetry data is sent to the Aspire Dashboard collector and visualized there. With this approach, we can check how long an HTTP request took, how much time was spent communicating with the PostgreSQL database, and how much with Redis. Event logs are linked to requests, making it easy to navigate between them.

Aspire Dashboard is avaiable at: http://localhost:18888 once docker-compose has been launched.

Here is an example of creating a customer, which results in adding a new record in the Aspire Dashboard.

![](docs/images/Post-Customer.gif)

And here is the code responsible for setting up Open Telemetry.

<details>
  <summary><b>Code</b></summary>
  <p>

```csharp
public static void InstallTelemetry(this WebApplicationBuilder builder, IConfiguration configuration, ConnectionMultiplexer redisConnection)
{
    var telemetrySettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>().Telemetry;
    var url = $"{telemetrySettings.Host}:{telemetrySettings.Port}";

    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService(telemetrySettings.Name, serviceInstanceId: Environment.MachineName))
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation();

            metrics.AddOtlpExporter(options =>
            {
                if (!string.IsNullOrEmpty(url))
                {
                    options.Endpoint = new Uri(url);
                }
            });

        })
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRedisInstrumentation(redisConnection, opt => opt.FlushInterval = TimeSpan.FromSeconds(1))
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    options.EnrichWithIDbCommand = (activity, command) =>
                    {
                        var stateDisplayName = $"{command.CommandType} {command.CommandText} Database: {command.Connection?.Database}";
                        activity.DisplayName = stateDisplayName;
                        activity.SetTag("db.name", stateDisplayName);
                    };
                });

            tracing.AddOtlpExporter(options =>
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    options.Endpoint = new Uri(url);
                }
            });
        });

    builder.Logging.AddOpenTelemetry(logging =>
    {
        if (!string.IsNullOrEmpty(url))
        {
            logging.AddOtlpExporter(options => options.Endpoint = new Uri(url));
        }
    });

}
```
  </p>
</details>


## 7 Design patterns implemented in this project
### 7.1 Mediator
The Mediator from the MassTransit library was chosen because it doesn't require the implementation of any interfaces, unlike the MediatR library. 
If we were to use the Mediator from MediatR instead of MassTransit, our domain event would look like this:
```csharp
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : INotification
``` 
INotification intefrace comes from MediatR library.
However our domain events look like this:

```csharp
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
```
IDomainEvent is just a marker interface that we keep in our Domain Layer.

This is particularly important because domain events located in the domain layer can remain free of any dependencies. I believe that the domain layer should be free from all libraries, making it easier to test.

https://masstransit.io/documentation/concepts/mediator

### 7.2 Factory method

Domain Events are mapped to Integration Events thanks to ***EventMapperFactory*** class. 

```csharp
    public class EventMapperFactory
    {
        private readonly Dictionary<Type, IEventMapper> _mappers;

        public EventMapperFactory(Dictionary<Type, IEventMapper> mappers)
        {
            _mappers = mappers;
        }

        public IEventMapper GetMapper(IDomainEvent domainEvent)
        {
            if (_mappers.TryGetValue(domainEvent.GetType(), out var mapper))
            {
                return mapper;
            }

            return null;
        }
    }
```

The registration of new mappers is moved to the ***DependencyInjectionInstaller*** file presented below. As you can see, currently, we have only one mapper registered for the CustomerCreatedDomainEvent.
To add another mapper, simply register it here. This approach supports the Open/Closed Principle.

```csharp
            builder.Services.AddSingleton<EventMapperFactory>(provider =>
            {
                var mappers = new Dictionary<Type, IEventMapper>
                {
                    { typeof(CustomerCreatedDomainEvent), provider.GetRequiredService<CustomerCreatedEventMapper>() },
                };

                return new EventMapperFactory(mappers);
            });
```

### 7.3 Strategy

The [Strategy pattern](https://en.wikipedia.org/wiki/Strategy_pattern) is used to obtain the appropriate mapper for mapping Domain Events to Integration Events. Each mapper must implement the IEventMapper interface, which allows you to dynamically apply the correct mapper at runtime. 

## 8. Tests
In the project, tests were implemented for the domain and application layers.

![](docs/images/Tests-Overview.png)

Test Explorer

![](docs/images/Test-Explorer.png)

### 8.1 Domain tests
Thanks to separating domain logic from other layers, we are able to easily test our code. Below is a unit test responsible for creating a customer.
```csharp

    public class CustomerTests
    {
        [Theory]
        [InlineData("incomplete-email@")]
        [InlineData("sample.email")]
        internal void Should_Throw_Invalid_Email_Domain_Exception_For_Invalid_Email(string invalidEmail)
        {
            Assert.Throws<InvalidEmailDomainException>(() =>
            {
                new Email(invalidEmail);
            });
        }

        [Fact]
        internal void Should_Create_Customer_For_Valid_Input_Data()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var fullName = new FullName("Mikolaj Jankowski");
            var age = new Age(DateTime.UtcNow.AddYears(-20));
            var email = new Email("my-email@yahoo.com");
            var address = new Address("Fifth Avenue", "10A", "1", "USA", "10037");

            // Act
            var customer = CA.And.DDD.Template.Domain.Customers.Customer.CreateCustomer(
                customerId,
                fullName,
                age,
                email,
                address);

            // Assert
            var domainEvents = customer.DomainEvents; 
            Assert.NotNull(domainEvents);
            Assert.Single(domainEvents); 

            var domainEvent = domainEvents.FirstOrDefault();
            Assert.NotNull(domainEvent);
            Assert.IsType<CustomerCreatedDomainEvent>(domainEvent); 

        }
    }

```
### 8.2 Application tests

Testing the application layer essentially comes down to testing handlers. Below are selected implemented test cases.

```csharp
    [Fact]
    public async Task Should_Change_Email_When_Customer_Exists()
    {
        // Arrange
        var oldEmail = "old@email.com";
        var newEmail = "new@email.com";

        var customer = CA.And.DDD.Template.Domain.Customers.Customer.CreateCustomer(
            new CustomerId(Guid.NewGuid()),
            new FullName("Mikolaj"),
            new Age(DateTime.Now.AddYears(-30)),
            new Email("email@email.com"),
            new Address("Fifth Avenue", "10A", "1", "PL", "10037"));

        

        _customerRepositoryMock.Setup(repo => repo.GetAsync(oldEmail, default))
                               .ReturnsAsync(customer);

        var command = new ChangeEmailCommand(oldEmail, newEmail);
        var consumeContextMock = Mock.Of<ConsumeContext<ChangeEmailCommand>>(c => c.Message == command);

        // Act
        await _handler.Consume(consumeContextMock);

        // Assert
        Assert.Equal(newEmail, customer.Email.Value);
        _customerRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<string>(), default), Times.Exactly(1));
    }

    [Fact]
    public async Task Should_Throw_CustomerNotFoundApplicationException_When_Customer_Does_Not_Exist()
    {
        // Arrange
        var oldEmail = "nonexistent@example.com";
        var newEmail = "new@example.com";

        _customerRepositoryMock.Setup(repo => repo.GetAsync(oldEmail, default)).ReturnsAsync((Customer?)null);

        var command = new ChangeEmailCommand(oldEmail, newEmail);
        var consumeContextMock = Mock.Of<ConsumeContext<ChangeEmailCommand>>(c => c.Message == command);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomerNotFoundApplicationException>(() => _handler.Consume(consumeContextMock));
        _customerRepositoryMock.Verify(repo => repo.GetAsync(It.IsAny<string>(), default), Times.Exactly(1));

    }
```
## 9. Build with
* [.NET Core 10](https://github.com/dotnet/core)
* [ASP.NET Core 10](https://github.com/dotnet/aspnetcore)
* [RabbitMQ](https://github.com/rabbitmq)
* [Redis](https://github.com/redis/redis)
* [MassTransit](https://github.com/MassTransit)
* [EF Core](https://github.com/dotnet/efcore)
* [Moq](https://github.com/moq)
* [xUnit](https://github.com/xunit/xunit)
* [HtmlSanitizer](https://github.com/mganss/HtmlSanitizer)
* [Open Telemetry](https://github.com/open-telemetry)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation)
* [MailHog](https://github.com/mailhog/MailHog)
* [KeyCloak](https://github.com/keycloak/keycloak)


## 1. บทนำและภาพรวมระบบ

### 1.1 วัตถุประสงค์ (เหมือนเดิม)

### 1.2 เทคโนโลยีหลัก (Tech Stack – .NET Edition)

| หมวดหมู่ | เทคโนโลยี | เวอร์ชัน / ไลบรารี |
|---------|-----------|-------------------|
| **ภาษา** | C# | 12+ (.NET 8 / .NET 9) |
| **Framework** | ASP.NET Core | 8.0+ |
| **ORM** | Entity Framework Core | 8.0+ |
| **ฐานข้อมูลหลัก** | PostgreSQL | 15+ (Npgsql) |
| **Cache** | Redis | 7+ (StackExchange.Redis) |
| **Message Queue** | Apache Kafka | 3.4+ (Confluent.Kafka) |
| **Logging & Monitoring** | Serilog + Elasticsearch + Grafana | - |
| **การจัดการเอกสาร** | QuestPDF / iTextSharp / PDFSharp | - |
| **IoT** | MQTTnet (MQTT), InfluxDB.Client | - |
| **Documentation** | Swashbuckle (Swagger) | - |
| **Build Tool** | .NET CLI / MSBuild | - |
| **Dependency Injection** | Built-in (Microsoft.Extensions.DependencyInjection) | - |
| **Mapping** | AutoMapper | 12.0+ |
| **Validation** | FluentValidation | 11.0+ |
| **Background Jobs** | Hangfire / Quartz.NET | - |
| **Real-time** | SignalR (optional) | - |

---

## 2. สถาปัตยกรรมระบบ (Clean Architecture + DDD)

### 2.1 แผนภาพสถาปัตยกรรม (ปรับเป็น .NET)

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           PRESENTATION LAYER                               │
│                    (ASP.NET Core Web API + Swagger)                         │
│                   Controllers, Filters, Middleware                          │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                           APPLICATION LAYER                                │
│              (CQRS with MediatR / Use Cases / DTOs)                        │
│         Commands, Queries, Handlers, Validators, Mappers                   │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                            DOMAIN LAYER                                    │
│              (Entities, Value Objects, Aggregates, Interfaces)             │
│         Domain Events, Repository Interfaces, Business Rules               │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                         INFRASTRUCTURE LAYER                               │
│    (EF Core Repositories, Unit of Work, Redis Cache, Kafka Producer/Consumer)│
│    MQTT Client, Email Sender, File Storage, PDF Generation, Hangfire Jobs   │
└─────────────────────────────────────────────────────────────────────────────┘
                                      │
                                      ▼
                      ┌───────────────┼───────────────┐
                      ▼               ▼               ▼
               ┌───────────┐  ┌───────────┐  ┌───────────┐
               │PostgreSQL │  │  Redis    │  │  Kafka    │
               │(EF Core)  │  │(Cache)    │  │(Event Bus)│
               └───────────┘  └───────────┘  └─────┬─────┘
                                                    │
                              ┌─────────────────────┼─────────────┐
                              ▼                     ▼             ▼
                       ┌───────────┐         ┌───────────┐ ┌───────────┐
                       │Elasticsearch│         │ InfluxDB  │ │ Grafana   │
                       │ (Logs)     │         │ (IoT)     │ │ (Metrics) │
                       └───────────┘         └───────────┘ └───────────┘
```

### 2.2 หลักการออกแบบ (เหมือนเดิม)
- **Separation of Concerns**
- **Dependency Inversion**: Domain ไม่ขึ้นกับ Infrastructure
- **CQRS + Event Sourcing** (บางส่วน)
- **Repository + Unit of Work**
- **Domain Events** เพื่อแยกเหตุการณ์

---

## 3. ตัวอย่างโครงสร้างโปรเจกต์ (Solution Structure)

```
AutoRepairShop.sln
├── AutoRepairShop.Domain                     (Class Library)
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── Job.cs
│   │   ├── Quotation.cs
│   │   ├── Customer.cs
│   │   └── ...
│   ├── ValueObjects/
│   │   ├── Money.cs
│   │   ├── Address.cs
│   │   └── ...
│   ├── Aggregates/
│   │   ├── JobAggregate.cs
│   │   └── ...
│   ├── Enums/
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   ├── IUnitOfWork.cs
│   │   └── IEventPublisher.cs
│   ├── Events/
│   │   ├── JobCreatedEvent.cs
│   │   ├── QuotationApprovedEvent.cs
│   │   └── ...
│   └── Exceptions/
├── AutoRepairShop.Application               (Class Library)
│   ├── Commands/
│   │   ├── Jobs/
│   │   │   ├── CreateJobCommand.cs
│   │   │   ├── CreateJobCommandHandler.cs
│   │   │   └── CreateJobCommandValidator.cs
│   │   ├── Quotations/
│   │   └── ...
│   ├── Queries/
│   │   ├── Jobs/
│   │   │   ├── GetJobByIdQuery.cs
│   │   │   ├── GetJobByIdQueryHandler.cs
│   │   │   └── GetJobListQuery.cs
│   │   └── ...
│   ├── DTOs/
│   │   ├── JobDto.cs
│   │   ├── QuotationDto.cs
│   │   └── ...
│   ├── Mappings/
│   │   └── AutoMapperProfile.cs
│   ├── Validators/
│   ├── Services/
│   │   ├── IEmailService.cs
│   │   ├── IDocumentGenerator.cs
│   │   └── ...
│   └── Common/
│       ├── BaseCommand.cs
│       ├── BaseQuery.cs
│       └── Result.cs
├── AutoRepairShop.Infrastructure             (Class Library)
│   ├── Persistence/
│   │   ├── AppDbContext.cs
│   │   ├── Configurations/
│   │   │   ├── JobConfiguration.cs
│   │   │   └── ...
│   │   ├── Repositories/
│   │   │   ├── GenericRepository.cs
│   │   │   ├── JobRepository.cs
│   │   │   └── ...
│   │   ├── UnitOfWork.cs
│   │   └── Migrations/
│   ├── Cache/
│   │   ├── RedisCacheService.cs
│   │   └── ICacheService.cs
│   ├── Messaging/
│   │   ├── KafkaProducer.cs
│   │   ├── KafkaConsumer.cs
│   │   └── EventPublisher.cs
│   ├── IoT/
│   │   ├── MqttClientService.cs
│   │   └── InfluxDbService.cs
│   ├── BackgroundJobs/
│   │   ├── HangfireJobs.cs
│   │   └── QuartzJobs.cs
│   ├── DocumentGeneration/
│   │   ├── PdfGenerator.cs (using QuestPDF)
│   │   └── ExcelGenerator.cs (using EPPlus)
│   ├── Email/
│   │   └── SmtpEmailService.cs
│   ├── Authentication/
│   │   ├── JwtTokenService.cs
│   │   └── CustomIdentityService.cs
│   └── Extensions/
│       └── ServiceCollectionExtensions.cs
├── AutoRepairShop.Api                       (ASP.NET Core Web API)
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── JobsController.cs
│   │   ├── QuotationsController.cs
│   │   └── ...
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   ├── RateLimitingMiddleware.cs
│   │   └── RequestLoggingMiddleware.cs
│   ├── Filters/
│   │   └── ApiKeyFilter.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Properties/
│       └── launchSettings.json
├── AutoRepairShop.Shared                    (Class Library)
│   ├── Constants/
│   ├── Helpers/
│   ├── Extensions/
│   └── Resources/                           (สำหรับ i18n)
│       ├── Messages.th.resx
│       ├── Messages.en.resx
│       └── ...
└── AutoRepairShop.Tests                     (xUnit / NUnit)
    ├── UnitTests/
    ├── IntegrationTests/
    └── Mock/
```

# 🏗️ โครงสร้างระบบ ICMON – แยกตามโมดูลทั้ง 14 โมดูล

แผนภาพด้านล่างแสดงสถาปัตยกรรมของระบบ ICMON แบบ **Clean Architecture + DDD + CQRS** โดยแยกชั้น (Layers) และโมดูล (Modules) อย่างชัดเจน พร้อมความสัมพันธ์ระหว่างกัน

---

## 📊 แผนภาพสถาปัตยกรรม (Mermaid)

```mermaid
flowchart TB
    subgraph Presentation["🎯 Presentation Layer (API)"]
        direction LR
        AuthCtrl["Auth Controller"]
        JobCtrl["Job Controller"]
        CustCtrl["Customer Controller"]
        QuotCtrl["Quotation Controller"]
        POCtrl["PO Controller"]
        InvCtrl["Inventory Controller"]
        PayCtrl["Payment Controller"]
        DashCtrl["Dashboard Controller"]
        RepCtrl["Reports Controller"]
        DocCtrl["Document Controller"]
        EmailCtrl["Email Controller"]
        BatchCtrl["Batch Jobs Controller"]
        LangCtrl["Language Controller"]
        IoTCtrl["IoT Controller"]
        WOSCtrl["WOS Controller"]
    end

    subgraph Application["⚙️ Application Layer (CQRS + MediatR)"]
        direction TB
        subgraph AuthApp["🔐 Authentication"]
            AuthCmd["Commands<br/>(Login, Register, Refresh, Logout)"]
            AuthQry["Queries<br/>(GetCurrentUser)"]
        end
        subgraph JobApp["📋 Job Card"]
            JobCmd["Commands<br/>(Create, UpdateStatus, AddService, AddPart)"]
            JobQry["Queries<br/>(GetById, GetList, StatusSummary)"]
        end
        subgraph CustApp["👤 Customer"]
            CustCmd["Commands<br/>(Create, Update, Delete, AddCar, UpdateCar)"]
            CustQry["Queries<br/>(GetById, GetByPhone, Search, GetCarHistory)"]
        end
        subgraph QuotApp["📄 Quotation"]
            QuotCmd["Commands<br/>(Create, Approve, Reject, Update)"]
            QuotQry["Queries<br/>(GetById, GetByJob, GetList, StatusSummary)"]
        end
        subgraph POApp["📦 Purchase Order"]
            POCmd["Commands<br/>(Create, CreateFromQuotation, Send, Receive)"]
            POQry["Queries<br/>(GetById, GetByQuotation, GetList, TotalByPeriod)"]
        end
        subgraph InvApp["📊 Inventory"]
            InvCmd["Commands<br/>(CreatePart, Receive, Issue, Adjust, CreatePicking, ConfirmPicking)"]
            InvQry["Queries<br/>(GetPart, LowStock, Transactions, PickingList)"]
        end
        subgraph PayApp["💰 Payment"]
            PayCmd["Commands<br/>(RecordPayment, Refund, Confirm)"]
            PayQry["Queries<br/>(GetById, GetByInvoice, Outstanding, Summary)"]
        end
        subgraph DashApp["📈 Dashboard & Reports"]
            DashQry["Queries<br/>(Overview, Sales, JobStatus, Inventory, TopParts, Revenue, Financial)"]
            RepQry["Queries<br/>(Daily, Monthly, Yearly)"]
        end
        subgraph DocApp["📁 Document"]
            DocCmd["Commands<br/>(Generate, Upload, OCR, CreateTemplate)"]
            DocQry["Queries<br/>(GetById, GetByReference, GetList, GetTemplate)"]
        end
        subgraph EmailApp["✉️ Email"]
            EmailCmd["Commands<br/>(Send, SendTemplate, Bulk, Resend)"]
            EmailQry["Queries<br/>(Status, History, Analytics)"]
        end
        subgraph BatchApp["🔄 Batch Jobs"]
            BatchCmd["Commands<br/>(Trigger, Enable, Disable)"]
            BatchQry["Queries<br/>(GetList, Status, History)"]
        end
        subgraph LangApp["🌍 Multi-Language"]
            LangCmd["Commands<br/>(AddLanguage, UpdateLanguage, UpdateTranslation, BulkUpdate)"]
            LangQry["Queries<br/>(GetLanguages, GetCurrent, GetTranslations, GetTranslationByKey)"]
        end
        subgraph IoTApp["📡 IoT & GPS"]
            IoTCmd["Commands<br/>(RegisterDevice, UpdateStatus, CreateGeofence, PublishMQTT)"]
            IoTQry["Queries<br/>(GetDevice, GetLocation, GetHistory, GetAlerts, Summary)"]
        end
        subgraph WOSApp["🛒 Web Order System"]
            WOSCmd["Commands<br/>(AddToCart, UpdateCart, RemoveFromCart, ClearCart, ApplyPromotion, CreateOrder, UpdateOrderStatus, CancelOrder)"]
            WOSQry["Queries<br/>(GetCatalogue, GetCart, GetOrder, GetOrderList)"]
        end
    end

    subgraph Domain["🧠 Domain Layer (Entities, Aggregates, Events)"]
        direction LR
        AuthDom["User, Role, Permission, Token"]
        JobDom["Job, JobService, JobPart, StatusHistory"]
        CustDom["Customer, Car, CarHistory"]
        QuotDom["Quotation, QuotationPart, QuotationService"]
        PODom["PurchaseOrder, PurchaseOrderDetail"]
        InvDom["PartMaster, StockLocation, InventoryTransaction, PickingRequest, Stocktake"]
        PayDom["Payment, Receipt, OutstandingBalance"]
        DashDom["Views (v_dashboard_*)"]
        DocDom["Document, DocumentTemplate, OCRResult"]
        EmailDom["Email, EmailTemplate, EmailQueue"]
        BatchDom["BatchJob, BatchJobHistory"]
        LangDom["Language, Translation"]
        IoTDom["IoTDevice, GPSData, Geofence, GeofenceAlert"]
        WOSDom["CatalogueItem, ShoppingCart, WebOrder"]
    end

    subgraph Infrastructure["🛠️ Infrastructure Layer"]
        direction LR
        EF["Entity Framework Core<br/>(PostgreSQL)"]
        Redis["Redis Cache"]
        Kafka["Kafka Event Bus"]
        MQTT["MQTT Broker"]
        Influx["InfluxDB"]
        Jasper["JasperReports"]
        SMTP["SMTP / SendGrid"]
        Hangfire["Hangfire / Quartz"]
        Storage["File Storage (Local/S3/Azure)"]
        OCR["Tesseract OCR"]
    end

    subgraph External["🌐 External Systems"]
        extMQTT["IoT Devices"]
        extKafka["Other Services"]
        extEmail["Email Providers"]
        extPayment["Payment Gateway"]
    end

    %% Presentation -> Application
    AuthCtrl --> AuthApp
    JobCtrl --> JobApp
    CustCtrl --> CustApp
    QuotCtrl --> QuotApp
    POCtrl --> POApp
    InvCtrl --> InvApp
    PayCtrl --> PayApp
    DashCtrl --> DashApp
    RepCtrl --> RepApp
    DocCtrl --> DocApp
    EmailCtrl --> EmailApp
    BatchCtrl --> BatchApp
    LangCtrl --> LangApp
    IoTCtrl --> IoTApp
    WOSCtrl --> WOSApp

    %% Application -> Domain
    AuthApp --> AuthDom
    JobApp --> JobDom
    CustApp --> CustDom
    QuotApp --> QuotDom
    POApp --> PODom
    InvApp --> InvDom
    PayApp --> PayDom
    DashApp --> DashDom
    RepApp --> DashDom
    DocApp --> DocDom
    EmailApp --> EmailDom
    BatchApp --> BatchDom
    LangApp --> LangDom
    IoTApp --> IoTDom
    WOSApp --> WOSDom

    %% Infrastructure implements Domain interfaces
    AuthDom -.-> AuthInfra
    JobDom -.-> JobInfra
    CustDom -.-> CustInfra
    QuotDom -.-> QuotInfra
    PODom -.-> POInfra
    InvDom -.-> InvInfra
    PayDom -.-> PayInfra
    DashDom -.-> DashInfra
    DocDom -.-> DocInfra
    EmailDom -.-> EmailInfra
    BatchDom -.-> BatchInfra
    LangDom -.-> LangInfra
    IoTDom -.-> IoTInfra
    WOSDom -.-> WOSInfra

    %% Infrastructure Detail
    subgraph InfraDetails["Infrastructure Implementations"]
        AuthInfra["AuthRepository, JWT, PermissionService, RedisCache"]
        JobInfra["JobRepository, UnitOfWork, EventPublisher"]
        CustInfra["CustomerRepository, CarRepository"]
        QuotInfra["QuotationRepository"]
        POInfra["PurchaseOrderRepository"]
        InvInfra["PartRepository, TransactionRepository, PickingRepository"]
        PayInfra["PaymentRepository, ReceiptRepository"]
        DashInfra["DashboardRepository (Dapper), ReportRepository"]
        DocInfra["DocumentRepository, FileStorageService, JasperDocumentGenerator"]
        EmailInfra["EmailRepository, SmtpEmailSender, TemplateEngine"]
        BatchInfra["BatchJobRepository, BatchJobOrchestrator, HangfireService"]
        LangInfra["LanguageRepository, TranslationRepository, LocalizationService"]
        IoTInfra["IoTDeviceRepository, MqttClientService, GpsDataProcessor, InfluxDbService"]
        WOSInfra["CatalogueRepository, CartRepository, OrderRepository, PromotionRepository"]
    end

    %% Infrastructure connects to external
    InfraDetails --> EF
    InfraDetails --> Redis
    InfraDetails --> Kafka
    InfraDetails --> MQTT
    InfraDetails --> Influx
    InfraDetails --> Jasper
    InfraDetails --> SMTP
    InfraDetails --> Hangfire
    InfraDetails --> Storage
    InfraDetails --> OCR

    %% External connections
    MQTT <--> extMQTT
    Kafka <--> extKafka
    SMTP <--> extEmail
    PayApp --> extPayment

    %%  Dependency Direction (Clean Architecture)
    Presentation --> Application
    Application --> Domain
    Infrastructure -.-> Domain
    Application -.-> Infrastructure

    %% Styling
    classDef layer fill:#f9f,stroke:#333,stroke-width:2px;
    classDef module fill:#e1f5fe,stroke:#01579b,stroke-width:1px;
    classDef domain fill:#fff3e0,stroke:#e65100,stroke-width:1px;
    classDef infra fill:#e8f5e9,stroke:#1b5e20,stroke-width:1px;
    classDef external fill:#fce4ec,stroke:#c62828,stroke-width:1px;
    class Presentation,Application,Domain,Infrastructure layer;
    class AuthApp,JobApp,CustApp,QuotApp,POApp,InvApp,PayApp,DashApp,DocApp,EmailApp,BatchApp,LangApp,IoTApp,WOSApp module;
    class AuthDom,JobDom,CustDom,QuotDom,PODom,InvDom,PayDom,DashDom,DocDom,EmailDom,BatchDom,LangDom,IoTDom,WOSDom domain;
    class AuthInfra,JobInfra,CustInfra,QuotInfra,POInfra,InvInfra,PayInfra,DashInfra,DocInfra,EmailInfra,BatchInfra,LangInfra,IoTInfra,WOSInfra infra;
    class extMQTT,extKafka,extEmail,extPayment external;
```

---

## 🧱 คำอธิบายแต่ละชั้น (Layers)

| Layer | บทบาท | เทคโนโลยีหลัก |
|-------|-------|--------------|
| **Presentation** | รับ Request จาก Client, ตรวจสอบสิทธิ์, ส่งต่อ Command/Query | ASP.NET Core Web API, Swagger, Rate Limiting, JWT |
| **Application** | จัดการ Business Logic ผ่าน CQRS, ตรวจสอบข้อมูล (Validation), แปลง DTO | MediatR, FluentValidation, AutoMapper |
| **Domain** | กำหนด Entities, Value Objects, Aggregates, Events, Interfaces สำหรับ Repository | C# Classes, Domain Events |
| **Infrastructure** | Implement Repository, ติดต่อ Database, Cache, Message Queue, External Services | EF Core, Redis, Kafka, MQTT, InfluxDB, JasperReports |

---

## 📦 รายละเอียดแต่ละโมดูล (Modules)

### 1. Authentication & Permission
- **Presentation**: `AuthController` (Login, Logout, Refresh, Register, Me)
- **Application**: Commands/Queries สำหรับ Authentication
- **Domain**: `User`, `Role`, `Permission`, `UserToken`
- **Infrastructure**: JWT Service, Permission Service, Repository, Redis Cache

### 2. Job Card Management
- **Presentation**: `JobsController`
- **Application**: Create, UpdateStatus, AddService, AddPart, GetById, GetList, StatusSummary
- **Domain**: `Job` (Aggregate Root), `JobService`, `JobPartSales`, `JobStatusHistory`, `JobSymptom`, `JobDiagnosticCode`
- **Infrastructure**: `JobRepository`, Caching, Event Publishing

### 3. Customer Management
- **Presentation**: `CustomersController`, `CarsController`
- **Application**: Create/Update/Delete Customer, Add/Update/Delete Car, Search
- **Domain**: `Customer`, `Car`, `CustomerCarHistory`
- **Infrastructure**: `CustomerRepository`, `CarRepository`

### 4. Quotation
- **Presentation**: `QuotationsController`
- **Application**: Create, Approve, Reject, Update, GetById, GetByJob, GetList, StatusSummary
- **Domain**: `Quotation`, `QuotationPart`, `QuotationService`, `QuotationStatusHistory`
- **Infrastructure**: `QuotationRepository`, PDF Generation (JasperReports)

### 5. Purchase Order
- **Presentation**: `PurchaseOrdersController`
- **Application**: Create (Manual & From Quotation), Send, Receive, GetById, GetList, TotalByPeriod, Suggestion
- **Domain**: `PurchaseOrder`, `PurchaseOrderDetail`, `PurchaseOrderStatusHistory`
- **Infrastructure**: `PurchaseOrderRepository`, PDF Generation

### 6. Inventory Management
- **Presentation**: `InventoryController`, `PartPickingController`
- **Application**: CreatePart, Receive, Issue, Adjust, CreatePicking, ConfirmPicking, LowStock, Transactions
- **Domain**: `PartMaster`, `StockLocation`, `InventoryTransaction`, `PartPickingRequest`, `Stocktake`
- **Infrastructure**: `PartMasterRepository`, `InventoryTransactionRepository`, `PickingRepository`

### 7. Payment Management
- **Presentation**: `PaymentsController`, `ReceiptsController`
- **Application**: RecordPayment, Refund, Confirm, GetOutstanding, GetByInvoice, Summary
- **Domain**: `Payment`, `Receipt`, `PaymentHistory`, `OutstandingBalance`
- **Infrastructure**: `PaymentRepository`, `ReceiptRepository`, PDF Receipt

### 8. Dashboard & Reports
- **Presentation**: `DashboardController`, `ReportsController`
- **Application**: Queries for Overview, Sales, JobStatus, Inventory, TopParts, Revenue, Financial, Daily/Monthly/Yearly Reports
- **Domain**: Database Views (`v_dashboard_*`)
- **Infrastructure**: `DashboardRepository` (Dapper), `ReportRepository`, JasperReports Generator

### 9. Document Management
- **Presentation**: `DocumentsController`
- **Application**: GenerateDocument, UploadDocument, ProcessOCR, CreateTemplate
- **Domain**: `Document`, `DocumentTemplate`, `DocumentHistory`, `OCRResult`
- **Infrastructure**: `DocumentRepository`, File Storage (Local/S3/Azure), JasperReports Integration, Tesseract OCR

### 10. Email Service
- **Presentation**: `EmailController`
- **Application**: SendEmail, SendTemplate, BulkEmail, Resend, GetStatus, GetHistory, GetAnalytics
- **Domain**: `Email`, `EmailTemplate`, `EmailQueue`, `EmailHistory`
- **Infrastructure**: `EmailRepository`, SMTP/SendGrid, Razor Template Engine

### 11. Batch Jobs
- **Presentation**: `BatchJobsController`
- **Application**: Trigger, Enable, Disable, GetList, GetStatus, GetHistory
- **Domain**: `BatchJob`, `BatchJobHistory`
- **Infrastructure**: Hangfire/Quartz, `BatchJobOrchestrator`, 6 predefined jobs

### 12. Multi-Language (i18n)
- **Presentation**: `LanguagesController`
- **Application**: AddLanguage, UpdateLanguage, UpdateTranslation, BulkUpdate, GetLanguages, GetCurrent, GetTranslations
- **Domain**: `Language`, `Translation`
- **Infrastructure**: `LanguageRepository`, `TranslationRepository`, `LocalizationService`, CustomStringLocalizer, Middleware

### 13. IoT & GPS Tracking
- **Presentation**: `IoTController`, `MqttController`
- **Application**: RegisterDevice, UpdateStatus, CreateGeofence, PublishMQTT, GetLocation, GetHistory, GetAlerts, Summary
- **Domain**: `IoTDevice`, `GPSData`, `Geofence`, `GeofenceAlert`, `DeviceHistory`
- **Infrastructure**: `IoTDeviceRepository`, `MqttClientService`, `GpsDataProcessor`, `GeofenceService`, `InfluxDbService`

### 14. Web Order System (WOS)
- **Presentation**: `WOSCatalogueController`, `WOSCartController`, `WOSOrdersController`
- **Application**: AddToCart, UpdateCart, RemoveFromCart, ClearCart, ApplyPromotion, CreateOrder, UpdateOrderStatus, CancelOrder, GetCatalogue, GetCart, GetOrder, GetOrderList
- **Domain**: `CatalogueItem`, `CatalogueCategory`, `SalesPrice`, `Promotion`, `ShoppingCart`, `ShoppingCartItem`, `WebOrder`, `WebOrderItem`, `WebOrderStatusHistory`
- **Infrastructure**: `CatalogueRepository`, `CartRepository`, `OrderRepository`, `PromotionRepository`, `OrderNumberService`

---

## 🔄 ความสัมพันธ์ระหว่างโมดูล

```mermaid
flowchart LR
    Auth["🔐 Auth"] --> Job["📋 Job"]
    Auth --> Cust["👤 Customer"]
    Auth --> Quot["📄 Quotation"]
    Auth --> PO["📦 PO"]
    Auth --> Inv["📊 Inventory"]
    Auth --> Pay["💰 Payment"]
    Auth --> Doc["📁 Document"]
    Auth --> Email["✉️ Email"]
    Auth --> Batch["🔄 Batch"]
    Auth --> Lang["🌍 i18n"]
    Auth --> IoT["📡 IoT"]
    Auth --> WOS["🛒 WOS"]

    Cust --> Job
    Cust --> Quot
    Job --> Quot
    Quot --> PO
    PO --> Inv
    Inv --> Job
    Job --> Pay
    Pay --> Job
    Job --> Doc
    Quot --> Doc
    PO --> Doc
    Pay --> Doc
    Job --> Email
    Pay --> Email
    Batch --> Email
    Batch --> Dash["📈 Dashboard"]
    Dash --> Reports["📊 Reports"]
    IoT --> Dash
    WOS --> Inv
    WOS --> Pay
    WOS --> Email
```

---

## 🗂️ สรุปโครงสร้างโฟลเดอร์ (Solution Explorer)

```
ICMON.sln
├── ICMON.Domain
│   ├── Aggregates
│   │   ├── Auth
│   │   ├── JobAggregate
│   │   ├── CustomerAggregate
│   │   ├── QuotationAggregate
│   │   ├── PurchaseOrderAggregate
│   │   ├── InventoryAggregate
│   │   ├── PaymentAggregate
│   │   ├── DocumentAggregate
│   │   ├── EmailAggregate
│   │   ├── BatchJobAggregate
│   │   ├── LanguageAggregate
│   │   ├── IoTAggregate
│   │   └── WOSAggregate
│   ├── Enums
│   ├── Events
│   ├── ValueObjects
│   ├── Interfaces (IRepository, IUnitOfWork, IEventPublisher)
│   ├── Exceptions
│   └── Specifications
├── ICMON.Application
│   ├── Commands
│   │   ├── Auth
│   │   ├── Jobs
│   │   ├── Customers
│   │   ├── Quotations
│   │   ├── PurchaseOrders
│   │   ├── Inventory
│   │   ├── Payments
│   │   ├── Documents
│   │   ├── Emails
│   │   ├── BatchJobs
│   │   ├── Languages
│   │   ├── IoT
│   │   └── WOS
│   ├── Queries
│   │   ├── Auth
│   │   ├── Jobs
│   │   ├── Customers
│   │   ├── Quotations
│   │   ├── PurchaseOrders
│   │   ├── Inventory
│   │   ├── Payments
│   │   ├── Dashboards
│   │   ├── Reports
│   │   ├── Documents
│   │   ├── Emails
│   │   ├── BatchJobs
│   │   ├── Languages
│   │   ├── IoT
│   │   └── WOS
│   ├── DTOs
│   ├── Mappings (AutoMapper)
│   ├── Validators (FluentValidation)
│   ├── Behaviors (Pipeline)
│   ├── Services (Interfaces)
│   └── Common (BaseCommand, Result)
├── ICMON.Infrastructure
│   ├── Persistence
│   │   ├── Configurations (EF)
│   │   ├── Repositories
│   │   ├── Migrations
│   │   └── SeedData
│   ├── Cache (Redis)
│   ├── Messaging (Kafka)
│   ├── IoT (MQTT, InfluxDB)
│   ├── BackgroundJobs (Hangfire/Quartz)
│   ├── DocumentGeneration (JasperReports, OCR)
│   ├── Email (SMTP, SendGrid, TemplateEngine)
│   ├── Authentication (JWT, Permission)
│   ├── Localization
│   ├── Storage (File)
│   └── Extensions (DI)
├── ICMON.Api
│   ├── Controllers
│   ├── Middleware
│   ├── Filters
│   ├── Attributes
│   ├── Program.cs
│   └── appsettings.json
├── ICMON.Shared
│   ├── Constants
│   ├── Helpers
│   ├── Extensions
│   └── Resources (i18n JSON)
├── tests
│   ├── ICMON.UnitTests
│   ├── ICMON.IntegrationTests
│   └── ICMON.ArchitectureTests
├── Templates (JasperReports)
│   ├── Reports
│   └── Compiled
├── docker-compose.yml
├── Dockerfile
└── README.md
```

---

## 📌 สรุป

ระบบ ICMON ถูกออกแบบด้วย Clean Architecture + DDD + CQRS แบ่งเป็น 4 ชั้นหลักและ 14 โมดูลการทำงาน ซึ่งครอบคลุมทุกฟังก์ชันของอู่ซ่อมรถ ตั้งแต่การรับรถเข้าซ่อม การเสนอราคา การสั่งซื้ออะไหล่ การจัดการสต็อก การชำระเงิน ไปจนถึงระบบ IoT และระบบสั่งซื้อออนไลน์ โดยแต่ละโมดูลแยกจากกันอย่างชัดเจน ทำให้ง่ายต่อการพัฒนา ทดสอบ และบำรุงรักษา 🚀