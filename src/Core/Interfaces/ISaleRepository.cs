using Core.Entities;

namespace Core.Interfaces;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(Guid id);
    Task<Sale> AddAsync(Sale sale);
    Task<Sale> UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
}