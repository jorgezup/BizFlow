using Application.Events;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.EventHandlers;

public class PriceEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<PriceEvent>
{
    public async Task Handle(PriceEvent notification, CancellationToken cancellationToken)
    {
        var price = new PriceHistory
        {
            ProductId = notification.ProductId,
            Price = notification.Price
        };
        
        await unitOfWork.PriceHistoryRepository.AddAsync(price);
    }
}