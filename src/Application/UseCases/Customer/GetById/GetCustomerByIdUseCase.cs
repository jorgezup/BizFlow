using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetById;

public class GetCustomerByIdUseCase(IUnitOfWork unitOfWork) : IGetCustomerByIdUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (customer is null)
                throw new NotFoundException("Customer not found");

            return customer.MapToCustomerResponse();
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting customer by id", ex);
        }
        
    }
}