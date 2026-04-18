namespace Warehouse.Api.Dtos;

public record ProductDtos (
    int Id,
    string Name,
    decimal Price,
    int StockQuantity
);
