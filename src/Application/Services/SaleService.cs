using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class SaleService(ISaleRepository saleRepository) : ISaleService
{
    private readonly ISaleRepository _saleRepository = saleRepository;

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Sale sale)
    {
        await _saleRepository.AddAsync(sale);
    }

    public async Task UpdateAsync(Sale sale)
    {
        await _saleRepository.UpdateAsync(sale);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _saleRepository.DeleteAsync(id);
    }
}