using Core.DTOs;

namespace Application.UseCases.OrderDetail.GetBySaleId;

public interface IGetOrderDetailBySaleIdUseCase
{
    public Task<IEnumerable<OrderDetailResponse>> ExecuteAsync(Guid id);
}