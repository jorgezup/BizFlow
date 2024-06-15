using Core.Entities;
using Core.Models.Customer;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponse>?> GetAllAsync();
    Task<CustomerResponse?> GetByIdAsync(Guid id);
    Task<CustomerResponse> AddAsync(CustomerRequest customer);
    Task<CustomerResponse?> UpdateAsync(CustomerUpdateRequest customerUpdateRequest);
    Task DeleteAsync(Guid id);
}