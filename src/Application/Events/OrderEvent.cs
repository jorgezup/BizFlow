using Core.Enums;
using MediatR;

namespace Application.Events;

public class OrderEvent(Guid orderId, Status status) : INotification
{
    public Guid OrderId { get; set; } = orderId;
    public Status Status { get; set; } = status;
}