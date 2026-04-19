using System.ComponentModel.DataAnnotations;

namespace Warehouse.Api.Dtos;

public record CreateProductDto (
    [Required][StringLength(30)] string Name,
    [Range(0,999999)]decimal Price,
    [Range(0,999999)]int StockQuantity
);
