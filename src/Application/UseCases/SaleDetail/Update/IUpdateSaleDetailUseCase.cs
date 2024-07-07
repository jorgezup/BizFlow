using Application.DTOs.SaleDetail;

namespace Application.UseCases.SaleDetail.Update;

public interface IUpdateSaleDetailUseCase
{
    public Task<SaleDetailResponse> ExecuteAsync(Guid id, UpdateSaleDetailRequest request);
}