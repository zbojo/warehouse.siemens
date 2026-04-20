using Warehouse.Api.Dtos;

namespace Warehouse.Api.Services;

public interface IProductService
{
    List<ProductDto> GetAll();
    ProductDto? GetById(int id);
    ProductDto Create(CreateProductDto newProduct);
    bool Update(int id, UpdatedProductDto updatedProduct);
}
