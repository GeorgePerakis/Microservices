# This repository contains two microservices: [`CatalogService`](CatalogService) and [`InventoryService`](InventoryService). Each service is built using .NET 8.0 and follows a microservices architecture.

## Prerequisites

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
