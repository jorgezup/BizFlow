using Application.DTOs.Sale;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.Update;

public class UpdateSaleUseCase(IUnitOfWork unitOfWork) : IUpdateSaleUseCase
{
    public async Task<SaleResponse> ExecuteAsync(Guid id, UpdateSaleRequest request)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var saleFound = await unitOfWork.SaleRepository.GetByIdAsync(id);

            if (saleFound is null)
                throw new NotFoundException("Sale not found");

            var saleToUpdate = saleFound.MapToSale(request);

            await unitOfWork.SaleRepository.UpdateAsync(saleToUpdate);
            await unitOfWork.CommitTransactionAsync();

            return saleToUpdate.MapToSaleResponse();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the sale", ex);
        }
    }
}