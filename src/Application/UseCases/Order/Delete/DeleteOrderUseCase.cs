using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Order.Delete;

public class DeleteOrderUseCase(IUnitOfWork unitOfWork) : IDeleteOrderUseCase
{
    public async Task ExecuteAsync(Guid id)
    {
        var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
        
        if (order is null)
            throw new NotFoundException("Order not found");

        if (order.Status == Status.Completed)
            throw new BadRequestException("Order is already completed");
        
        await unitOfWork.BeginTransactionAsync();
        await unitOfWork.OrderRepository.DeleteAsync(id);
        await unitOfWork.CommitTransactionAsync();
    }
}