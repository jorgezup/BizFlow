using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleItemsController(IProductService saleItemService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await saleItemService.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await saleItemService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddAsync(Product saleItem)
    {
        await saleItemService.AddAsync(saleItem);
    }

    [HttpPut]
    public async Task UpdateAsync(Product saleItem)
    {
        await saleItemService.UpdateAsync(saleItem);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await saleItemService.DeleteAsync(id);
    }
}