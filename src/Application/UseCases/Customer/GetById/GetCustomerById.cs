using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetById;

public class GetCustomerById(ICustomerRepository customerRepository) : IGetCustomerById
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        if (customer is null) throw new NotFoundException("Customer not found when getting");

        return customer.MapToCustomerResponse();
    }
}