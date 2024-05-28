using Core.Entities;

namespace Core.Interfaces;

public interface ISaleItemRepository
{
    Task<IEnumerable<SaleItem>> GetAllAsync();
    Task<SaleItem?> GetByIdAsync(Guid id);
    Task<SaleItem> AddAsync(SaleItem saleItem);
    Task<SaleItem> UpdateAsync(SaleItem saleItem);
    Task DeleteAsync(Guid id);
}