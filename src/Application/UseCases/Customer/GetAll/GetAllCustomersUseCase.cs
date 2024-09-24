using Application.DTOs.Customer;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetAll;

public class GetAllCustomersUseCase(IUnitOfWork unitOfWork) : IGetAllCustomersUseCase
{
    public async Task<IEnumerable<CustomerResponse>> ExecuteAsync()
    {
        try
        {
            var customers = await unitOfWork.CustomerRepository.GetAllAsync();

            return customers.Select(c => c.MapToCustomerResponse());
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while getting customers", ex);
        }
    }
}