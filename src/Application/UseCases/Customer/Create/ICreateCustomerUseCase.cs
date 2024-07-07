using Application.DTOs.Customer;

namespace Application.UseCases.Customer.Create;

public interface ICreateCustomerUseCase
{
    public Task<CustomerResponse> ExecuteAsync(CustomerRequest request);
}