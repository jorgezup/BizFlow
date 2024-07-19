using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.GetAll;

public class GetAllSalesDetailsUseCase(IUnitOfWork unitOfWork) : IGetAllSalesDetailsUseCase
{
    public async Task<IEnumerable<SaleDetailResponse>> ExecuteAsync()
    {
        try
        {
            var saleDetails = await unitOfWork.SaleDetailRepository.GetAllAsync();

            var salesDetailsList = saleDetails.ToList();
            if (saleDetails is null || salesDetailsList.Count is 0)
                throw new NotFoundException("Sale details not found");

            return salesDetailsList.Select(x => x.MapToSaleDetailResponse());
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting all sale details", e);
        }
    }
}