using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Customer.GetAll;

public class GetAllCustomersUseCase(IUnitOfWork unitOfWork) : IGetAllCustomersUseCase
{
    public async Task<IEnumerable<CustomerResponse>> ExecuteAsync()
    {
        var customers = await unitOfWork.CustomerRepository.GetAllAsync();
        var customersList = customers.ToList();

        if (!customersList.Any())
            throw new NotFoundException("No customers found");

        return customersList.Select(c => c.MapToCustomerResponse());
    }
}