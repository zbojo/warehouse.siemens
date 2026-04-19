namespace Warehouse.Api.Dtos;

public record UpdatedProductDto (
    string Name,
    decimal Price,
    int StockQuantity
);