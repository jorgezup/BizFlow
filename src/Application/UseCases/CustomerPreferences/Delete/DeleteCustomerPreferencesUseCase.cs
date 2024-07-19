using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.Delete;

public class DeleteCustomerPreferencesUseCase(IUnitOfWork unitOfWork) : IDeleteCustomerPreferencesUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var customerPreferences = await unitOfWork.CustomerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences is null)
            throw new NotFoundException("Customer preferences not found");

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerPreferencesRepository.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting customer preferences", ex);
        }
    }
}