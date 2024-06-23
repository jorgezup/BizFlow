using Core.Interfaces;

namespace Application.UseCases.Customer.Delete;

public class DeleteCustomer(ICustomerRepository customerRepository) : IDeleteCustomer
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        if (customer is null) return false;

        await customerRepository.DeleteAsync(id);
        return true;
    }
}