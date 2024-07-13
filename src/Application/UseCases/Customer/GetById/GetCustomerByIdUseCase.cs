using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetById;

public class GetCustomerByIdUseCase(IUnitOfWork unitOfWork) : IGetCustomerByIdUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);

        if (customer == null)
            throw new NotFoundException("Customer not found");

        return customer.MapToCustomerResponse();
    }
}