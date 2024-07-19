using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.GetById;

public class GetSaleDetailByIdUseCase(IUnitOfWork unitOfWork) : IGetSaleDetailByIdUseCase
{
    public async Task<SaleDetailResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var saleDetail = await unitOfWork.SaleDetailRepository.GetByIdAsync(id);

            if (saleDetail is null)
                throw new NotFoundException("Sale detail not found");

            return saleDetail.MapToSaleDetailResponse();
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the sale detail", e);
        }
    }
}