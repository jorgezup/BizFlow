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

            saleFound.Status = string.IsNullOrWhiteSpace(request.Status) ? saleFound.Status : request.Status;
            saleFound.SaleDate = request.SaleDate ?? saleFound.SaleDate;
            saleFound.UpdatedAt = DateTime.UtcNow; 
            
            await unitOfWork.SaleRepository.UpdateAsync(saleFound);
            await unitOfWork.CommitTransactionAsync();

            return saleFound.MapToSaleResponse();
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the sale", ex);
        }
    }
}