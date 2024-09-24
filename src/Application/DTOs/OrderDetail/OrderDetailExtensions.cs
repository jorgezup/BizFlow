namespace Application.DTOs.OrderDetail;

public static class OrderDetailExtensions
{
    public static Core.DTOs.OrderDetailResponse MapToOrderDetailResponse(this Core.Entities.OrderDetail orderDetail)
    {
        return new Core.DTOs.OrderDetailResponse(
            orderDetail.Id,
            orderDetail.OrderId,
            orderDetail.ProductId,
            orderDetail.Order.CustomerId,
            orderDetail.Order.Customer.Name,
            orderDetail.Product.Name,
            orderDetail.Quantity,
            orderDetail.UnitPrice,
            orderDetail.Subtotal,
            orderDetail.CreatedAt,
            orderDetail.UpdatedAt);
    }

    public static Core.Entities.OrderDetail MapToOrderDetail(this OrderDetailRequest orderDetailRequest)
    {
        return new Core.Entities.OrderDetail
        {
            Id = Guid.NewGuid(),
            OrderId = orderDetailRequest.OrderId,
            ProductId = orderDetailRequest.ProductId,
            Quantity = orderDetailRequest.Quantity,
            UnitPrice = orderDetailRequest.UnitPrice,
            Subtotal = orderDetailRequest.Subtotal,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
    
}