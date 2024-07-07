using Application.DTOs.Customer;

namespace Application.UseCases.Customer.GetById;

public interface IGetCustomerByIdUseCase
{
    public Task<CustomerResponse> ExecuteAsync(Guid id);
}