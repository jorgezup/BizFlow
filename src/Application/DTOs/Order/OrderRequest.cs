using Application.DTOs.OrderDetail;

namespace Application.DTOs.Order;

public record OrderRequest(
    Guid CustomerId,
    DateTime OrderDate,
    List<OrderDetailRequestToSale> OrderDetails,
    bool? Generated);