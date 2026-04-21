using Warehouse.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IProductService>(new ProductService("inventory.json"));
var app = builder.Build();

app.MapControllers();

app.Run();
