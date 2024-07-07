using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.GetById;

public class GetSaleDetailByIdUseCase(IUnitOfWork unitOfWork) : IGetSaleDetailByIdUseCase
{
    public async Task<SaleDetailResponse> ExecuteAsync(Guid id)
    {
        var saleDetail = await unitOfWork.SaleDetailRepository.GetByIdAsync(id);

        if (saleDetail == null)
            throw new NotFoundException("Sale detail not found");

        return saleDetail.MapToSaleDetailResponse();
    }
}