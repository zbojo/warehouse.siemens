using Warehouse.Api.Dtos;

const string GetProductEndpoint = "GetProduct";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "default");

List<ProductDto> products = [
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

// GET /products #get all products
app.MapGet("/products", () => products );

// GET /products/{id} #get specific product
app.MapGet("/products/{id}", (int id) => products.Find(product => product.Id == id))
    .WithName(GetProductEndpoint);

// POST /products #create new product
app.MapPost("/products", (CreateProductDto newProduct) =>
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

// PUT /products/{id} #updates product
app.MapPut("/products/{id}", (int id, UpdatedProductDto updatedProduct) =>
{
    int index = products.FindIndex(product => product.Id == id);
    products[index] = new ProductDto (
        id,
        updatedProduct.Name,
        updatedProduct.Price,
        updatedProduct.StockQuantity
    );

    return Results.NoContent();

});

app.Run();
