using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SaleDetailRepository(AppDbContext appDbContext) : ISaleDetailRepository
{
    public async Task<SaleDetail?> GetByIdAsync(Guid id)
    {
        return await appDbContext.SaleDetails.FindAsync(id);
    }

    public async Task<IEnumerable<SaleDetail>> GetAllAsync()
    {
        return await appDbContext.SaleDetails.ToListAsync();
    }

    public async Task<IEnumerable<SaleDetail>> GetBySaleIdAsync(Guid id)
    {
        return await appDbContext.SaleDetails
            .Where(x => x.SaleId == id)
            .ToListAsync();
    }

    public async Task AddAsync(SaleDetail saleDetail)
    {
        await appDbContext.SaleDetails.AddAsync(saleDetail);
    }

    public Task UpdateAsync(SaleDetail saleDetail)
    {
        appDbContext.SaleDetails.Update(saleDetail);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var saleDetail = await appDbContext.SaleDetails.FindAsync(id);
        if (saleDetail is not null)
        {
            appDbContext.SaleDetails.Remove(saleDetail);
        }
    }
}