using Core.Exceptions;
using Core.Interfaces;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.GetById;

public class GetOrderByIdUseCase(IUnitOfWork unitOfWork) : IGetOrderByIdUseCase
{
    public async Task<OrderResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var orderFound = await unitOfWork.OrderRepository.GetByIdAsync(id);

            if (orderFound is null)
                throw new NotFoundException("Order not found");

            return orderFound;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the order", e);
        }
    }
}