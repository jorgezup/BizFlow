using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerPreferencesRepository(AppDbContext appDbContext) : ICustomerPreferencesRepository
{
    public async Task<CustomerPreferences?> GetByIdAsync(Guid id)
    {
        return await appDbContext.CustomerPreferences.FindAsync(id);
    }

    public async Task<IEnumerable<CustomerPreferences>> GetAllAsync()
    {
        return await appDbContext.CustomerPreferences.ToListAsync();
    }

    public async Task AddAsync(CustomerPreferences customerPreferences)
    {
        await appDbContext.CustomerPreferences.AddAsync(customerPreferences);
    }

    public Task UpdateAsync(CustomerPreferences customerPreferences)
    {
        appDbContext.CustomerPreferences.Update(customerPreferences);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customerPreferences = await GetByIdAsync(id);
        if (customerPreferences is not null)
        {
            appDbContext.CustomerPreferences.Remove(customerPreferences);
        }
    }

    public async Task<IEnumerable<CustomerPreferences>> GetByCustomerIdAsync(Guid customerId)
    {
        return await appDbContext.CustomerPreferences
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }
}