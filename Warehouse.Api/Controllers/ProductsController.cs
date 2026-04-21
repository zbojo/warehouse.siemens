using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Dtos;
using Warehouse.Api.Services;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService service;

    public ProductsController(IProductService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = service.GetById(id);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto newProduct)
    {
        var product = service.Create(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdatedProductDto updatedProduct)
    {
        if (!service.Update(id, updatedProduct))
        {
            return NotFound();
        }
        return NoContent();
    }
}
