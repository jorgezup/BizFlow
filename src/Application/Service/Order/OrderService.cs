using Application.DTOs.Order;
using Application.Events;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;

namespace Application.Service.Order;

public class OrderService(IUnitOfWork unitOfWork, IMediator mediator) : IOrderService
{
    public async Task<Core.Entities.Order> CreateOrderAsync(OrderRequest request)
    {
        var order = request.MapToOrder();
        await FillOrderDetailAsync(request, order);
        return order;
    }

    private async Task FillOrderDetailAsync(OrderRequest request, Core.Entities.Order order)
    {
        foreach (var detailRequest in request.OrderDetails)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(detailRequest.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {detailRequest.ProductId} not found");
            }
            
            if (detailRequest is { Quantity: <= 0, Subtotal: <= 0 })
            {
                throw new BadRequestException("Invalid quantity or subtotal");
            }

            var orderDetail = new Core.Entities.OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = detailRequest.ProductId,
                UnitPrice = product.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            if (detailRequest.Quantity > 0)
            {
                orderDetail.Quantity = (decimal)detailRequest.Quantity;
                orderDetail.Subtotal = (decimal)detailRequest.Quantity * product.Price;
            }
            if (detailRequest.Subtotal > 0)
            {
                orderDetail.Subtotal = (decimal)detailRequest.Subtotal;
                orderDetail.Quantity = orderDetail.Subtotal / product.Price;
            }

            order.OrderDetails.Add(orderDetail);

            await mediator.Publish(new OrderEvent(order.Id, Status.Completed));

            await unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
        }
        
    }
}