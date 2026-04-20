using System.Text.Json;
using Warehouse.Api.Dtos;

namespace Warehouse.Api.Endpoints;

public static class ProductsEndpoints
{
    const string GetProductEndpoint = "GetProduct";
    private static readonly string FilePath = "inventory.json";

    private static List<ProductDto> LoadProducts()
    {
        if (!File.Exists(FilePath))
            return new List<ProductDto>();

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<ProductDto>>(json) ?? new List<ProductDto>();
    }

    private static void SaveProducts(List<ProductDto> products)
    {
        string json = JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, json);
    }

    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");

        group.MapGet("/", () => LoadProducts());

        group.MapGet("/{id}", (int id) =>
        {
            var product = LoadProducts().Find(p => p.Id == id);
            return product is null ? Results.NotFound() : Results.Ok(product);
        })
        .WithName(GetProductEndpoint);

        group.MapPost("/", (CreateProductDto newProduct) =>
        {
            var products = LoadProducts();
            int newId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            var product = new ProductDto(newId, newProduct.Name, newProduct.Price, newProduct.StockQuantity);
            products.Add(product);
            SaveProducts(products);
            return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
        });

        group.MapPut("/{id}", (int id, UpdatedProductDto updatedProduct) =>
        {
            var products = LoadProducts();
            int index = products.FindIndex(p => p.Id == id);
            if (index == -1) return Results.NotFound();
            products[index] = new ProductDto(id, updatedProduct.Name, updatedProduct.Price, updatedProduct.StockQuantity);
            SaveProducts(products);
            return Results.NoContent();
        });
    }
}
