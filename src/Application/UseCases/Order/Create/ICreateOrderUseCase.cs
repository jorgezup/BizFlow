using Application.DTOs.Order;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.Create;

public interface ICreateOrderUseCase
{
    public Task<OrderResponse> ExecuteAsync(OrderRequest request);
}