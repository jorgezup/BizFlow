using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.Delete;

public class DeleteSaleUseCase(IUnitOfWork unitOfWork) : IDeleteSaleUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var sale = await unitOfWork.SaleRepository.GetByIdAsync(id);

        if (sale is null)
            throw new NotFoundException("Sale not found");

        await unitOfWork.BeginTransactionAsync();
            
        try
        {
            await unitOfWork.SaleRepository.DeleteAsync(sale.Id);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the sale", ex);
        }
    }
}