using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SaleRepository(AppDbContext appDbContext) : ISaleRepository
{
    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await appDbContext.Sales.ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Sales.FirstOrDefaultAsync(x => x.SaleId == id);
    }

    public async Task<Sale> AddAsync(Sale sale)
    {
        await appDbContext.Sales.AddAsync(sale);
        await appDbContext.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale> UpdateAsync(Sale sale)
    {
        appDbContext.Sales.Update(sale);
        await appDbContext.SaveChangesAsync();
        return sale;
    }

    public async Task DeleteAsync(Guid id)
    {
        var sale = await appDbContext.Sales.FindAsync(id);
        if (sale != null)
        {
            appDbContext.Sales.Remove(sale);
            await appDbContext.SaveChangesAsync();
        }
    }
}