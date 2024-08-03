using Application.DTOs.Sale;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SaleRepository(AppDbContext appDbContext) : ISaleRepository
{
    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Sales.FindAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await appDbContext.Sales.ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetSalesByCustomerIdAsync(Guid customerId)
    {
        return await appDbContext.Sales
            .Where(s => s.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await appDbContext.Sales.AddAsync(sale);
    }

    public Task UpdateAsync(Sale sale)
    {
        appDbContext.Sales.Update(sale);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var sale = await appDbContext.Sales.FindAsync(id);
        if (sale is not null)
        {
            appDbContext.Sales.Remove(sale);
        }
    }
}