using Warehouse.Api.Dtos;
using Warehouse.Api.Services;

namespace Warehouse.Api.Tests;

public class ProductServiceTests : IDisposable
{
    private readonly string tempFile;
    private readonly ProductService service;

    public ProductServiceTests()
    {
        tempFile = Path.GetTempFileName();
        File.Delete(tempFile);
        service = new ProductService(tempFile);
    }

    public void Dispose()
    {
        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void GetAll_WhenFileDoesNotExist_ReturnsEmptyList()
    {
        var result = service.GetAll();

        Assert.Empty(result);
    }

    [Fact]
    public void GetById_WhenProductDoesNotExist_ReturnsNull()
    {
        var result = service.GetById(42);

        Assert.Null(result);
    }

    [Fact]
    public void GetById_WhenProductExists_ReturnsProduct()
    {
        var created = service.Create(new CreateProductDto("Apple", 5.5m, 10));

        var result = service.GetById(created.Id);

        Assert.NotNull(result);
        Assert.Equal("Apple", result!.Name);
        Assert.Equal(5.5m, result.Price);
        Assert.Equal(10, result.StockQuantity);
    }

    [Fact]
    public void Create_FirstProduct_GetsIdOne()
    {
        var product = service.Create(new CreateProductDto("Apple", 4.5m, 10));

        Assert.Equal(1, product.Id);
    }

    [Fact]
    public void Create_SubsequentProducts_GetIncrementedIds()
    {
        service.Create(new CreateProductDto("Apple", 1.5m, 10));
        var second = service.Create(new CreateProductDto("Banana", 2m, 5));
        var third = service.Create(new CreateProductDto("Cherry", 3m, 20));

        Assert.Equal(2, second.Id);
        Assert.Equal(3, third.Id);
    }

    [Fact]
    public void Create_PersistsProductToFile()
    {
        service.Create(new CreateProductDto("Apple", 1.5m, 10));

        var freshService = new ProductService(tempFile);
        var all = freshService.GetAll();

        Assert.Single(all);
        Assert.Equal("Apple", all[0].Name);
    }

    [Fact]
    public void Update_WhenProductExists_ReturnsTrueAndChangesData()
    {
        var created = service.Create(new CreateProductDto("Apple", 1.5m, 10));

        var success = service.Update(created.Id, new UpdatedProductDto("Green Apple", 2m, 15));

        Assert.True(success);
        var updated = service.GetById(created.Id);
        Assert.Equal("Green Apple", updated!.Name);
        Assert.Equal(2m, updated.Price);
        Assert.Equal(15, updated.StockQuantity);
    }

    [Fact]
    public void Update_WhenProductDoesNotExist_ReturnsFalse()
    {
        var success = service.Update(999, new UpdatedProductDto("Ghost", 1m, 1));

        Assert.False(success);
    }
}
