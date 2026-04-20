using System.Text.Json;
using Warehouse.Api.Dtos;

namespace Warehouse.Api.Services;

public class ProductService : IProductService
{
    private const string FilePath = "inventory.json";

    private List<ProductDto> LoadProducts()
    {
        if (!File.Exists(FilePath))
            return new List<ProductDto>();

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<ProductDto>>(json) ?? new List<ProductDto>();
    }

    private void SaveProducts(List<ProductDto> products)
    {
        string json = JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, json);
    }

    public List<ProductDto> GetAll() => LoadProducts();

    public ProductDto? GetById(int id) => LoadProducts().Find(p => p.Id == id);

    public ProductDto Create(CreateProductDto newProduct)
    {
        var products = LoadProducts();
        int newId = products.Count + 1;
        var product = new ProductDto(newId, newProduct.Name, newProduct.Price, newProduct.StockQuantity);
        products.Add(product);
        SaveProducts(products);
        return product;
    }

    public bool Update(int id, UpdatedProductDto updatedProduct)
    {
        var products = LoadProducts();
        int index = products.FindIndex(p => p.Id == id);
        if (index == -1) return false;
        products[index] = new ProductDto(id, updatedProduct.Name, updatedProduct.Price, updatedProduct.StockQuantity);
        SaveProducts(products);
        return true;
    }
}
