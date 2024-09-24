using Application.DTOs.OrderDetail;
using Application.Events;
using Core.Interfaces;
using MediatR;

namespace Application.UseCases.Order.GenerateOrders;

public class GenerateOrdersUseCase(IUnitOfWork unitOfWork, IMediator mediator) : IGenerateOrdersUseCase
{
    public async Task ExecuteAsync()
    {
        var today = DateTime.Now;
        var weekStart = today.Date;
        var weekEnd = today.AddDays(6).Date;
        
        var preferences = await unitOfWork.CustomerPreferencesRepository.GetAllAsync();

        foreach (var preference in preferences)
        {
            var nextPreferredDate = GetNextPreferredDate(preference.PreferredPurchaseDay, today);

            if (nextPreferredDate < weekStart || nextPreferredDate > weekEnd)
            {
                continue;
            }

            var existingOrders = (await unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(preference.CustomerId))
                .Where(o => o.CustomerId == preference.CustomerId
                            && o.OrderDetails.Any(od => od.ProductId == preference.ProductId)
                            && o.OrderDate >= weekStart
                            && o.OrderDate <= weekEnd
                            && o.Generated)
                .ToList();
            
            if (existingOrders.Count != 0)
            {
                continue;
            }

            var orderDetails = new List<OrderDetailRequestToSale>()
            {
                new()
                {
                    ProductId = preference.ProductId,
                    Quantity = preference.Quantity
                }
            };
            
            var orderEvent = new OrderGeneratedEvent(preference.CustomerId, nextPreferredDate, orderDetails);
            await unitOfWork.BeginTransactionAsync();
            await mediator.Publish(orderEvent);
            await unitOfWork.CommitTransactionAsync();
        }
    }
    
    private DateTime GetNextPreferredDate(string preferredDay, DateTime fromDate)
    {
        var daysOfWeek = new Dictionary<string, DayOfWeek>
        {
            { "Sunday", DayOfWeek.Sunday },
            { "Monday", DayOfWeek.Monday },
            { "Tuesday", DayOfWeek.Tuesday },
            { "Wednesday", DayOfWeek.Wednesday },
            { "Thursday", DayOfWeek.Thursday },
            { "Friday", DayOfWeek.Friday },
            { "Saturday", DayOfWeek.Saturday }
        };

        if (!daysOfWeek.TryGetValue(preferredDay, out var preferredDayOfWeek))
        {
            throw new ArgumentException($"Invalid preferred day: {preferredDay}");
        }

        var currentDayOfWeek = fromDate.DayOfWeek;
        var daysToAdd = ((int)preferredDayOfWeek - (int)currentDayOfWeek + 7) % 7;
        return fromDate.AddDays(daysToAdd);
    }
}