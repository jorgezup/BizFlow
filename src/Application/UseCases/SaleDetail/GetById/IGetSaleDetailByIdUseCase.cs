using Application.DTOs.SaleDetail;

namespace Application.UseCases.SaleDetail.GetById;

public interface IGetSaleDetailByIdUseCase
{
    public Task<SaleDetailResponse> ExecuteAsync(Guid id);
}