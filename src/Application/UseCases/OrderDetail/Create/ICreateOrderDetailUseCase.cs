using Application.DTOs.OrderDetail;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.UseCases.OrderDetail.Create;

public interface ICreateOrderDetailUseCase
{
    public Task<OrderDetailResponse> ExecuteAsync(OrderDetailRequest request);
}