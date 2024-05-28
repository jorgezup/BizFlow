using Core.Entities;

namespace Application.Interfaces;

public interface ISaleItemService
{
    Task<IEnumerable<SaleItem>> GetAllAsync();
    Task<SaleItem?> GetByIdAsync(Guid id);
    Task AddAsync(SaleItem saleItem);
    Task UpdateAsync(SaleItem saleItem);
    Task DeleteAsync(Guid id);
}