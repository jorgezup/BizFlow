using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetAll;

public class GetAllCustomersUseCase(ICustomerRepository customerRepository) : IGetAllCustomersUseCase
{
    public async Task<IEnumerable<CustomerResponse>> ExecuteAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        var customersList = customers.ToList();

        if (customersList.Count == 0) throw new NotFoundException("Customer not found when getting all");

        return customersList.Select(c => c.MapToCustomerResponse());
    }
}