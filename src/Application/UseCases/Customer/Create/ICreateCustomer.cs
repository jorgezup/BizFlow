using Application.DTOs.Customer;

namespace Application.UseCases.Customer.Create;

public interface ICreateCustomer
{
    public Task<CustomerResponse> ExecuteAsync(CustomerRequest request);
}