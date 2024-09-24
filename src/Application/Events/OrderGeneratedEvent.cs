using Application.DTOs.OrderDetail;
using MediatR;

namespace Application.Events;

public class OrderGeneratedEvent(Guid customerId, DateTime orderDate, List<OrderDetailRequestToSale> orderDetails)
    : INotification
{
    public Guid CustomerId { get; } = customerId;
    public DateTime OrderDate { get; } = orderDate;
    public List<OrderDetailRequestToSale> OrderDetails { get; } = orderDetails;
}