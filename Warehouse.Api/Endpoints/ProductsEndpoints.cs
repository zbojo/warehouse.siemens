using System;
using Warehouse.Api.Dtos;

namespace Warehouse.Api.Endpoints;

public static class ProductsEndpoints
{
    const string GetProductEndpoint = "GetProduct";
    private static readonly List<ProductDto> products = [
        new (
            1,
            "apple",
            2.35M,
            4
        ),
        new (
            2,
            "banana",
            3.49M,
            10
        ),
        new (
            3,
            "kiwi",
            3.11M,
            3
        ),
    ];

    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");

        // GET /products 
        group.MapGet("/", () => products );

        // GET /products/{id} 
        group.MapGet("/{id}", (int id) => {
            var product = products.Find(product => product.Id == id);

            return product is null ? Results.NotFound() : Results.Ok(product);
        })
        .WithName(GetProductEndpoint);

        // POST /products 
        group.MapPost("/", (CreateProductDto newProduct) =>
        {
            ProductDto product = new (
                products.Count + 1,
                newProduct.Name,
                newProduct.Price,
                newProduct.StockQuantity
            );
            products.Add(product);

            return Results.CreatedAtRoute(GetProductEndpoint, new {id = product.Id}, product);

        });

        // PUT /products/{id} 
        group.MapPut("/{id}", (int id, UpdatedProductDto updatedProduct) =>
        {
            int index = products.FindIndex(product => product.Id == id);

            if (index == -1) return Results.NotFound();

            products[index] = new ProductDto (
                id,
                updatedProduct.Name,
                updatedProduct.Price,
                updatedProduct.StockQuantity
            );

            return Results.NoContent();

        });
    }
}
