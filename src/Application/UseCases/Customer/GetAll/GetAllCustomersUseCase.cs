using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetAll;

public class GetAllCustomersUseCase(IUnitOfWork unitOfWork) : IGetAllCustomersUseCase
{
    public async Task<IEnumerable<CustomerResponse>> ExecuteAsync()
    {
        try
        {
            var customers = await unitOfWork.CustomerRepository.GetAllAsync();
            var customersList = customers.ToList();

            if (customersList.Count == 0)
                throw new NotFoundException("No customers found");

            return customersList.Select(c => c.MapToCustomerResponse());
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting customers", ex);
        }
    }
}