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
        return await appDbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await appDbContext.Products.AddAsync(product);
        await appDbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        appDbContext.Products.Update(product);
        await appDbContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await appDbContext.Products.FindAsync(id);
        if (product != null)
        {
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();
        }
    }
}