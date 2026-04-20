using Warehouse.Api.Dtos;
using Warehouse.Api.Services;

namespace Warehouse.Api.Endpoints;

public static class ProductsEndpoints
{
    const string GetProductEndpoint = "GetProduct";

    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/", (IProductService service) => service.GetAll());

        group.MapGet("/{id}", (int id, IProductService service) =>
        {
            var product = service.GetById(id);
            return product is null ? Results.NotFound() : Results.Ok(product);
        })
        .WithName(GetProductEndpoint);

        group.MapPost("/", (CreateProductDto newProduct, IProductService service) =>
        {
            var product = service.Create(newProduct);
            return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
        });

        group.MapPut("/{id}", (int id, UpdatedProductDto updatedProduct, IProductService service) =>
        {
            return service.Update(id, updatedProduct) ? Results.NoContent() : Results.NotFound();
        });
    }
}
