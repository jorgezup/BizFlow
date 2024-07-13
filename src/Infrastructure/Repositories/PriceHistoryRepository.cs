using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PriceHistoryRepository(AppDbContext appDbContext) : IPriceHistoryRepository
{
    public async Task<PriceHistory?> GetByIdAsync(Guid id)
    {
        return await appDbContext.PriceHistories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<PriceHistory>> GetAllAsync()
    {
        return await appDbContext.PriceHistories.ToListAsync();
    }

    public async Task AddAsync(PriceHistory priceHistory)
    {
        await appDbContext.PriceHistories.AddAsync(priceHistory);
    }

    public Task UpdateAsync(PriceHistory priceHistory)
    {
        appDbContext.PriceHistories.Update(priceHistory);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var priceHistory = await GetByIdAsync(id);
        if (priceHistory != null)
        {
            appDbContext.PriceHistories.Remove(priceHistory);
        }
    }

    public async Task<IEnumerable<PriceHistory>> GetByProductIdAsync(Guid id)
    {
        return await appDbContext.PriceHistories
            .Where(x => x.ProductId == id)
            .ToListAsync();
    }
}