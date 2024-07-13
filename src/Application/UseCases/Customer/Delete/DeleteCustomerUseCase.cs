using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.Customer.Delete;

public class DeleteCustomerUseCase(IUnitOfWork unitOfWork) : IDeleteCustomerUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(id);

        if (customer == null)
            return false;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerRepository.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the customer", ex);
        }
    }
}