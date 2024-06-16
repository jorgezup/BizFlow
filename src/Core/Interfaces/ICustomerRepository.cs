using Core.Entities;
using Core.Models.Customer;

namespace Core.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer> AddAsync(CustomerRequest customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
    Task<Customer?> GetByEmailAsync(string customerEmail);
}