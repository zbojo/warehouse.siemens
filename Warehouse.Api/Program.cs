using Warehouse.Api.Endpoints;
using Warehouse.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.Services.AddSingleton<IProductService, ProductService>();
var app = builder.Build();

app.MapProductsEndpoints();

app.Run();
