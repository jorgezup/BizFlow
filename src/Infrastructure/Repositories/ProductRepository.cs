using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await appDbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Products.FindAsync(id);
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
        var product = await appDbContext.Products.FindAsync(id);
        if (product is not null)
        {
            appDbContext.Products.Remove(product);
        }
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        return product;
    }
}