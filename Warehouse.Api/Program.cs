using Warehouse.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "default");

List<ProductDtos> products = [
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
        2,
        "kiwi",
        3.11M,
        3
    ),
];

// GET -> /products 
app.MapGet("/products", () =>
{
    return products;
});

app.Run();
