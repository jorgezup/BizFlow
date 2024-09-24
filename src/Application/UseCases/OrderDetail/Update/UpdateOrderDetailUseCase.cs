using Application.DTOs.OrderDetail;
using Application.Service.OrderDetail;
using Core.Exceptions;
using Core.Interfaces;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.UseCases.OrderDetail.Update;

public class UpdateOrderDetailUseCase(IOrderDetailService orderDetailService, IUnitOfWork unitOfWork)
    : IUpdateOrderDetailUseCase
{
    public async Task<OrderDetailResponse> ExecuteAsync(Guid id, UpdateOrderDetailRequest request)
    {
        try
        {
            var orderDetail = await orderDetailService.UpdateOrderDetailService(id, request);

            return orderDetail;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the sale detail", ex);
        }
    }
}