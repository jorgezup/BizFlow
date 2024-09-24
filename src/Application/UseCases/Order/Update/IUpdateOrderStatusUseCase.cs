using Application.DTOs.Order;

namespace Application.UseCases.Order.Update;

public interface IUpdateOrderStatusUseCase
{
    public Task ExecuteAsync(Guid orderId, OrderUpdateStatusRequest request);
}