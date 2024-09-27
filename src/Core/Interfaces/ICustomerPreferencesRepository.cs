using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerPreferencesRepository
{
    Task<CustomerPreferencesResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerPreferencesResponse>> GetAllAsync();
    Task AddAsync(CustomerPreferences customerPreferences);
    Task UpdateAsync(CustomerPreferences customerPreferences);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<CustomerPreferencesResponse>> GetByCustomerIdAsync(Guid customerId);
}