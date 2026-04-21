# Warehouse API

ASP.NET Core Web API for managing warehouse inventory. Data is stored in `inventory.json`.

## Structure

- `Warehouse.Api/` — Web API project
  - `Controllers/` — API endpoints
  - `Services/` — warehouse logic and JSON persistence
  - `Dtos/` — data transfer objects
- `Warehouse.Api.Tests/` — unit tests

## Run

```
dotnet run --project Warehouse.Api
```

## Tests

```
dotnet test
```

## Endpoints

- GET `/api/products`
- GET `/api/products/{id}`
- POST `/api/products`
- PUT `/api/products/{id}`
