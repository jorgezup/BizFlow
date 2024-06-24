using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
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
            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PriceHistory priceHistory)
        {
            appDbContext.PriceHistories.Update(priceHistory);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var priceHistory = await GetByIdAsync(id);
            if (priceHistory != null)
            {
                appDbContext.PriceHistories.Remove(priceHistory);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}