using Application.DTOs.Customer;

namespace Application.UseCases.Customer.GetAll;

public interface IGetAllCustomers
{
    public Task<IEnumerable<CustomerResponse>> ExecuteAsync();
}