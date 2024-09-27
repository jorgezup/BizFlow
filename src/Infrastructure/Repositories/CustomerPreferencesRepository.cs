using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Infrastructure.Repositories;

public class CustomerPreferencesRepository(AppDbContext appDbContext) : ICustomerPreferencesRepository
{
    public async Task<CustomerPreferencesResponse> GetByIdAsync(Guid id)
    {
        return await appDbContext.CustomerPreferences
            .Where(x => x.Id == id)
            .Select(cp => new CustomerPreferencesResponse(
                cp.Id,
                cp.CustomerId,
                cp.Customer.Name,
                cp.ProductId,
                cp.Product.Name,
                cp.Quantity,
                cp.PreferredPurchaseDay,
                cp.CreatedAt,
                cp.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustomerPreferencesResponse>> GetAllAsync()
    {
        return await appDbContext.CustomerPreferences
            .Select(cp => new CustomerPreferencesResponse(
                cp.Id,
                cp.CustomerId,
                cp.Customer.Name,
                cp.ProductId,
                cp.Product.Name,
                cp.Quantity,
                cp.PreferredPurchaseDay,
                cp.CreatedAt,
                cp.UpdatedAt
            ))
            .ToListAsync();
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
        var customerPreferences = await appDbContext.CustomerPreferences.FindAsync(id);
        if (customerPreferences is not null)
        {
            appDbContext.CustomerPreferences.Remove(customerPreferences);
        }
    }

    public async Task<IEnumerable<CustomerPreferencesResponse>> GetByCustomerIdAsync(Guid customerId)
    {
        return await appDbContext.CustomerPreferences
            .Where(cp => cp.CustomerId == customerId)
            .Select(cp => new CustomerPreferencesResponse(
                cp.Id,
                cp.CustomerId,
                cp.Customer.Name,
                cp.ProductId,
                cp.Product.Name,
                cp.Quantity,
                cp.PreferredPurchaseDay,
                cp.CreatedAt,
                cp.UpdatedAt
            ))
            .ToListAsync();
    }
}