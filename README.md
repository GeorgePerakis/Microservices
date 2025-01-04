# This repository contains two microservices: [`CatalogService`](CatalogService) and [`InventoryService`](InventoryService). Each service is built using .NET 8.0 and follows a microservices architecture.

## About
This project is a microservices-based architecture that leverages several modern technologies to ensure scalability, maintainability, and resilience. Below is an overview of the key technologies used and how they are integrated into the project:

### Technologies Used
- [ASP.NET Core: The primary framework for building the web APIs.]
- [MassTransit: A distributed application framework for .NET, used for asynchronous communication between microservices.]
- [RabbitMQ: A message broker that facilitates the asynchronous communication between microservices.]
- [MongoDB: A NoSQL database used for data storage.]
- [Polly: A .NET resilience and transient-fault-handling library used to implement the circuit breaker pattern.]
- [Swashbuckle: Used to generate Swagger documentation for the APIs.]

### Asynchronous Communication
It uses MassTransit with RabbitMQ to enable asynchronous communication between microservices. This is achieved by configuring MassTransit to use RabbitMQ as the transport mechanism. Each microservice can publish and consume messages, allowing for decoupled and scalable communication.

### Circuit Breaker Pattern
Also, it implements the circuit breaker pattern using Polly to handle transient faults and improve the resilience of the microservices. The circuit breaker pattern is particularly useful for preventing cascading failures and ensuring that the system can recover gracefully from temporary issues.

This configuration ensures that the CatalogClient will retry failed requests with an exponential backoff and implement a circuit breaker to stop making requests if a certain number of consecutive failures occur. This helps to prevent overwhelming the service and allows it to recover before more requests are made.

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Setup
### 1. Clone the Repository
```sh
git clone https://github.com/your-repo/microservices.git
cd microservices
```

### 2. Build the Common Library
```sh
cd CommonLib
dotnet build
cd ..
```

### 3. Start MongoDB and RabbitMQ
```sh
docker-compose up -d
```

### 4. Run Services
```sh
cd CatalogService
dotnet run --project src/Services.Catalog/Services.Catalog.csproj
```
```sh
cd InventoryService
dotnet run --project src/Services.Inventory/Services.Inventory.csproj
```

## Configuration
Both configuration settings are located in their respective appsettings.json and appsettings.Development.json.

## Endpoints
There is a json file attached for each services endpoints, you can import it directly to post man so you can try the endpoints yourself
