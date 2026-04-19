using Warehouse.Api.Dtos;
using Warehouse.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapProductsEndpoints();

app.Run();
