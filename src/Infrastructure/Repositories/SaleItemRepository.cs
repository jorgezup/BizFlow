using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SaleItemRepository(AppDbContext appDbContext) : ISaleItemRepository
{
    public async Task<IEnumerable<SaleItem>> GetAllAsync()
    {
        return await appDbContext.SaleItems.ToListAsync();
    }

    public async Task<SaleItem?> GetByIdAsync(Guid id)
    {
        return await appDbContext.SaleItems.FirstOrDefaultAsync(x => x.SaleItemId == id);
    }

    public async Task<SaleItem> AddAsync(SaleItem saleItem)
    {
        await appDbContext.SaleItems.AddAsync(saleItem);
        await appDbContext.SaveChangesAsync();
        return saleItem;
    }

    public async Task<SaleItem> UpdateAsync(SaleItem saleItem)
    {
        appDbContext.SaleItems.Update(saleItem);
        await appDbContext.SaveChangesAsync();
        return saleItem;
    }

    public async Task DeleteAsync(Guid id)
    {
        var saleItem = await appDbContext.SaleItems.FindAsync(id);
        if (saleItem != null)
        {
            appDbContext.SaleItems.Remove(saleItem);
            await appDbContext.SaveChangesAsync();
        }
    }
}