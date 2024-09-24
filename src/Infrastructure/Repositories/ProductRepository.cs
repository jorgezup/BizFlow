using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await appDbContext.Products
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Products
            .Where(p => p.IsActive)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await appDbContext.Products.AddAsync(product);
    }

    public Task UpdateAsync(Product product)
    {
        appDbContext.Products.Update(product);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product is not null)
        {
            product.IsActive = false;
            appDbContext.Products.Update(product);
        }
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        var products = await GetAllAsync();
        return products.FirstOrDefault(p => p.Name == name);
    }
}