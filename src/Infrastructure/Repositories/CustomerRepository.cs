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
        return await appDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await appDbContext.Customers.AddAsync(customer);
        await appDbContext.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        appDbContext.Customers.Update(customer);
        await appDbContext.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await appDbContext.Customers.FindAsync(id);
        if (customer is not null)
        {
            appDbContext.Customers.Remove(customer);
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task<Customer?> GetByEmailAsync(string? customerEmail)
    {
        var customer = await appDbContext.Customers.FirstOrDefaultAsync(x => x.Email == customerEmail);
        return customer;
    }
}