using Application.DTOs.SaleDetail;

namespace Application.UseCases.SaleDetail.GetAll;

public interface IGetAllSalesDetailsUseCase
{
    public Task<IEnumerable<SaleDetailResponse>> ExecuteAsync();
}