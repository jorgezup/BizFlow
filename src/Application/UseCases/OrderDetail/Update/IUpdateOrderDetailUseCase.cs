using Application.DTOs.OrderDetail;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.UseCases.OrderDetail.Update;

public interface IUpdateOrderDetailUseCase
{
    public Task<OrderDetailResponse> ExecuteAsync(Guid id, UpdateOrderDetailRequest request);
}