using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer?>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}