using Application.DTOs.Order;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.GetById;

public interface IGetOrderByIdUseCase
{
    public Task<OrderResponse> ExecuteAsync(Guid id);
}