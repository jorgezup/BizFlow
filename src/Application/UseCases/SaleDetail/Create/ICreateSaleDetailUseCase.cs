using Application.DTOs.SaleDetail;

namespace Application.UseCases.SaleDetail.Create;

public interface ICreateSaleDetailUseCase
{
    public Task<SaleDetailResponse> ExecuteAsync(SaleDetailRequest request);
}