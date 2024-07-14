using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.GetBySaleId;

public class GetSaleDetailBySaleIdUseCase(IUnitOfWork unitOfWork) : IGetSaleDetailBySaleIdUseCase
{
    public async Task<IEnumerable<SaleDetailResponse>> ExecuteAsync(Guid id)
    {
        var saleDetails = await unitOfWork.SaleDetailRepository.GetBySaleIdAsync(id);

        var enumerable = saleDetails.ToList();
        
        if (enumerable.Count == 0)
            throw new NotFoundException("Sale detail not found");
        
        foreach (var saleDetail in enumerable)
        {
            saleDetail.Product = await unitOfWork.ProductRepository.GetByIdAsync(saleDetail.ProductId)
                                 ?? throw new NotFoundException("Product not found");
        }
        
        return enumerable.Select(x => x.MapToSaleDetailResponse());
    }
}