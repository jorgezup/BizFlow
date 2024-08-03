using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.Update;

public class UpdateSaleDetailUseCase(IUnitOfWork unitOfWork) : IUpdateSaleDetailUseCase
{
    public async Task<SaleDetailResponse> ExecuteAsync(Guid id, UpdateSaleDetailRequest request)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var saleDetail = await unitOfWork.SaleDetailRepository.GetByIdAsync(id);

            if (saleDetail is null)
                throw new NotFoundException("Sale detail not found");
            
            if (saleDetail.ProductId != null)
                saleDetail.Product = await unitOfWork.ProductRepository.GetByIdAsync(saleDetail.ProductId);

            // var saleDetailToUpdate = saleDetail.UpdateSaleDetail(request);
            
            saleDetail.ProductId = request.ProductId ?? saleDetail.ProductId;
            saleDetail.Quantity = (decimal)(request.Quantity > 0 ? request.Quantity  : saleDetail.Quantity);
            saleDetail.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.SaleDetailRepository.UpdateAsync(saleDetail);
            await unitOfWork.CommitTransactionAsync();

            return saleDetail.MapToSaleDetailResponse();
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the sale detail", ex);
        }
    }
}