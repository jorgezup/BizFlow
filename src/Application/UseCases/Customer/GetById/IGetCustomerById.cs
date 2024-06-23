using Application.DTOs.Customer;

namespace Application.UseCases.Customer.GetById;

public interface IGetCustomerById
{
    public Task<CustomerResponse?> ExecuteAsync(Guid id);
}