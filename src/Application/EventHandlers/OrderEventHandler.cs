using Application.Events;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.EventHandlers;

public class OrderEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<OrderEvent>
{
    public async Task Handle(OrderEvent notification, CancellationToken cancellationToken)
    {
        var orderLifeCycle = new OrderLifeCycle
        {
            OrderId = notification.OrderId,
            Status = notification.Status,
            CreatedAt = DateTime.UtcNow
        };

        await unitOfWork.OrderLifeCycleRepository.AddAsync(orderLifeCycle);
    }
}