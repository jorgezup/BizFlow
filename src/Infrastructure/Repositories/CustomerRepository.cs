using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
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
        var customer = await appDbContext.Customers.FindAsync(id);
        if (customer is not null)
        {
            appDbContext.Customers.Remove(customer);
        }
    }

    public async Task<Customer?> GetByEmailAsync(string? customerEmail)
    {
        var customer = await appDbContext.Customers.FirstOrDefaultAsync(x => x.Email == customerEmail);
        return customer;
    }
}