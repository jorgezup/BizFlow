using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await productService.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await productService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddAsync(Product product)
    {
        await productService.AddAsync(product);
    }

    [HttpPut]
    public async Task UpdateAsync(Product product)
    {
        await productService.UpdateAsync(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await productService.DeleteAsync(id);
    }
}