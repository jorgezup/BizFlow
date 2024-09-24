using Application.DTOs.Order;
using Application.Events;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;

namespace Application.UseCases.Order.Update;

public class UpdateOrderStatusUseCase(IUnitOfWork unitOfWork, IMediator mediator) : IUpdateOrderStatusUseCase
{
    public async Task ExecuteAsync(Guid orderId, OrderUpdateStatusRequest request)
    {
        var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new NotFoundException("Order not found");
        }
        await unitOfWork.BeginTransactionAsync();
        await mediator.Publish(new OrderEvent(orderId, request.Status));
        await unitOfWork.CommitTransactionAsync();
    }
}