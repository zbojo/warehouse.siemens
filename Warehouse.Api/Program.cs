using Warehouse.Api.Dtos;
using Warehouse.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
var app = builder.Build();

app.MapProductsEndpoints();

app.Run();
