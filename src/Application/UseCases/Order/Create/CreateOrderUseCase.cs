using Application.DTOs.Order;
using Application.Service.Order;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.Create;

public class CreateOrderUseCase(
    IUnitOfWork unitOfWork,
    IValidator<OrderRequest> validator,
    IOrderService orderService) : ICreateOrderUseCase
{
    public async Task<OrderResponse> ExecuteAsync(OrderRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new DataContractValidationException("Invalid order data", validationResult.Errors);
        }

        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new NotFoundException("Customer not found");
        }

        await unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await orderService.CreateOrderAsync(request);
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.CommitTransactionAsync();
            return order.MapToOrderResponse();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating the order", e);
        }
    }
}