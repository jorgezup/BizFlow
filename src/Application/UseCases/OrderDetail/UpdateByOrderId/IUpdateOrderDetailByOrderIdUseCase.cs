using Application.DTOs.OrderDetail;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.UseCases.OrderDetail.Update;

public interface IUpdateOrderDetailByOrderIdUseCase
{
    public Task<IEnumerable<OrderDetailResponse>> ExecuteAsync(Guid id, UpdateOrderDetailRequest request);
}