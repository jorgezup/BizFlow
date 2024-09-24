using Application.DTOs.OrderDetail;
using Application.Service.OrderDetail;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.OrderDetail.Update;

public class UpdateOrderDetailByOrderIdUseCase(IOrderDetailService orderDetailService, IUnitOfWork unitOfWork)
    : IUpdateOrderDetailByOrderIdUseCase
{
    public async Task<IEnumerable<Core.DTOs.OrderDetailResponse>> ExecuteAsync(Guid id, UpdateOrderDetailRequest request)
    {
        try
        {
            var orderDetails = await unitOfWork.OrderDetailRepository.GetByOrderIdAsync(id);
            
            foreach (var detail in orderDetails)
            {
                await orderDetailService.UpdateOrderDetailService(detail.Id, request);
            }

            return orderDetails;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the sale detail", ex);
        }
    }
}