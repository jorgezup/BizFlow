using Application.DTOs.SaleDetail;

namespace Application.UseCases.SaleDetail.GetBySaleId;

public interface IGetSaleDetailBySaleIdUseCase
{
    public Task<IEnumerable<SaleDetailResponse>> ExecuteAsync(Guid id);
}