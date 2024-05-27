using Core.Entities;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}