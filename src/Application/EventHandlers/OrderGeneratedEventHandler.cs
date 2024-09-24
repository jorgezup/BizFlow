using Application.DTOs.Order;
using Application.Events;
using Application.Service.Order;
using Core.Interfaces;
using MediatR;

namespace Application.EventHandlers
{
    public class OrderGeneratedEventHandler(IOrderService orderService, IUnitOfWork unitOfWork) : INotificationHandler<OrderGeneratedEvent>
    {
        public async Task Handle(OrderGeneratedEvent notification, CancellationToken cancellationToken)
        {
            var orderRequest = new OrderRequest
            (
                notification.CustomerId,
                notification.OrderDate,
                notification.OrderDetails,
                Generated: true
            );

            var order = await orderService.CreateOrderAsync(orderRequest);
            await unitOfWork.OrderRepository.AddAsync(order);
        }
    }
}