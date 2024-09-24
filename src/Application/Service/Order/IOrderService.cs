using Application.DTOs.Order;

namespace Application.Service.Order;

public interface IOrderService
{
    Task<Core.Entities.Order> CreateOrderAsync(OrderRequest request);
}