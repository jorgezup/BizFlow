using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class SaleItemService(ISaleItemRepository saleItemRepository) : ISaleItemService
{
    private readonly ISaleItemRepository _saleItemRepository = saleItemRepository;

    public async Task<IEnumerable<SaleItem>> GetAllAsync()
    {
        return await _saleItemRepository.GetAllAsync();
    }

    public async Task<SaleItem?> GetByIdAsync(Guid id)
    {
        return await _saleItemRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(SaleItem saleItem)
    {
        await _saleItemRepository.AddAsync(saleItem);
    }

    public async Task UpdateAsync(SaleItem saleItem)
    {
        await _saleItemRepository.UpdateAsync(saleItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _saleItemRepository.DeleteAsync(id);
    }
}