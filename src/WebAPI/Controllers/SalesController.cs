using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(IProductService saleService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await saleService.GetAllAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await saleService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task AddAsync(Product sale)
    {
        await saleService.AddAsync(sale);
    }

    [HttpPut]
    public async Task UpdateAsync(Product sale)
    {
        await saleService.UpdateAsync(sale);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await saleService.DeleteAsync(id);
    }
}