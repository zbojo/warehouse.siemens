namespace Warehouse.Api.Dtos;

public record CreateProductDto (
    string Name,
    decimal Price,
    int StockQuantity
);
