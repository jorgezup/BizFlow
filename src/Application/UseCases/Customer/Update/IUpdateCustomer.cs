using Application.DTOs.Customer;

namespace Application.UseCases.Customer.Update;

public interface IUpdateCustomer
{
    public Task<CustomerResponse> ExecuteAsync(Guid customerId, CustomerUpdateRequest customerUpdateRequest);
}