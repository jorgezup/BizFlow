using Application.DTOs.OrderDetail;
using Core.Exceptions;
using Core.Interfaces;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.Service.OrderDetail;

public class OrderDetailService(IUnitOfWork unitOfWork) : IOrderDetailService
{
    public async Task<OrderDetailResponse> UpdateOrderDetailService(Guid id, UpdateOrderDetailRequest request)
    {
        var orderDetail = await unitOfWork.OrderDetailRepository.GetByIdAsync(id);

        if (orderDetail is null)
            throw new NotFoundException("Order detail not found");

        var (productId, productName, unitPrice) = await GetProduct(request);

        var (quantity, subtotal) = CalculateQuantityAndSubtotal(request, unitPrice, orderDetail);

        var orderDetailUpdated = OrderDetailUpdated(request, orderDetail, quantity, subtotal);

        await unitOfWork.BeginTransactionAsync();
        await unitOfWork.OrderDetailRepository.UpdateAsync(orderDetailUpdated);
        await unitOfWork.CommitTransactionAsync();

        return new OrderDetailResponse(
            orderDetail.Id,
            orderDetail.OrderId,
            productId,
            orderDetail.CustomerId,
            orderDetail.CustomerName,
            productName,
            quantity,
            unitPrice,
            subtotal,
            orderDetail.CreatedAt,
            DateTime.UtcNow
            );
    }

    private async Task<(Guid productId, string productName, decimal unitPrice)> GetProduct(UpdateOrderDetailRequest request)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            throw new NotFoundException("Product not found");

        return (product.Id, product.Name, product.Price);
    }

    private static Core.Entities.OrderDetail OrderDetailUpdated(
        UpdateOrderDetailRequest request,
        OrderDetailResponse orderDetail,
        decimal quantity,
        decimal subtotal)
    {
        var orderDetailUpdated = new Core.Entities.OrderDetail
        {
            Id = orderDetail.Id,
            OrderId = orderDetail.OrderId,
            ProductId = request.ProductId,
            UnitPrice = orderDetail.UnitPrice,
            Quantity = quantity,
            Subtotal = subtotal,
            CreatedAt = orderDetail.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
        return orderDetailUpdated;
    }

    private static (decimal quantity, decimal subtotal) CalculateQuantityAndSubtotal(
        UpdateOrderDetailRequest request,
        decimal unitPrice,
        OrderDetailResponse orderDetail)
    {
        var quantity = orderDetail.Quantity;
        var subtotal = orderDetail.Subtotal;

        if (request.Quantity > 0)
        {
            quantity = (decimal)request.Quantity;
            subtotal = unitPrice * quantity;
        }

        if (request.Subtotal > 0)
        {
            subtotal = (decimal)request.Subtotal;
            quantity = subtotal / unitPrice;
        }

        return (quantity, subtotal);
    }
}