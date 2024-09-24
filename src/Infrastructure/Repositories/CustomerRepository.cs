using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await appDbContext.Customers
            .Where(c => c.IsActive)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Customers
            .Where(c => c.IsActive)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Customer customer)
    {
        await appDbContext.Customers.AddAsync(customer);
    }

    public Task UpdateAsync(Customer customer)
    {
        appDbContext.Customers.Update(customer);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await GetByIdAsync(id);
        if (customer is not null)
        {
            customer.IsActive = false;
            appDbContext.Customers.Update(customer);
        }
    }

    public async Task<Customer?> GetByEmailAsync(string? customerEmail)
    {
        var customer = await appDbContext.Customers
            .Where(c => c.IsActive)
            .FirstOrDefaultAsync(x => x.Email == customerEmail);
        return customer;
    }
}