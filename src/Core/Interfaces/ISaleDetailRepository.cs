using Core.Entities;

namespace Core.Interfaces;

public interface ISaleDetailRepository
{
    Task<SaleDetail?> GetByIdAsync(Guid id);
    Task<IEnumerable<SaleDetail>> GetAllAsync();
    Task<IEnumerable<SaleDetail>> GetBySaleIdAsync(Guid id);
    Task AddAsync(SaleDetail saleDetail);
    Task UpdateAsync(SaleDetail saleDetail);
    Task DeleteAsync(Guid id);
}