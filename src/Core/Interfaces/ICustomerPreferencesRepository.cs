using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerPreferencesRepository
{
    Task<CustomerPreferences?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerPreferences>> GetAllAsync();
    Task AddAsync(CustomerPreferences customerPreferences);
    Task UpdateAsync(CustomerPreferences customerPreferences);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<CustomerPreferences>> GetByCustomerIdAsync(Guid customerId);
}