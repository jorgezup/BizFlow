using Application.DTOs.Customer;

namespace Application.UseCases.Customer.GetAll;

public interface IGetAllCustomersUseCase
{
    public Task<IEnumerable<CustomerResponse>> ExecuteAsync();
}