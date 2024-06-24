using Core.Entities;

namespace Core.Interfaces;

public interface IPriceHistoryRepository
{
    Task<PriceHistory?> GetByIdAsync(Guid id);
    Task<IEnumerable<PriceHistory>> GetAllAsync();
    Task AddAsync(PriceHistory priceHistory);
    Task UpdateAsync(PriceHistory priceHistory);
    Task DeleteAsync(Guid id);
}