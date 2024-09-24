using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.OrderDetail.GetById;

public class GetOrderDetailByIdUseCase(IUnitOfWork unitOfWork) : IGetOrderDetailByIdUseCase
{
    public async Task<OrderDetailResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var orderDetail = await unitOfWork.OrderDetailRepository.GetByIdAsync(id);

            if (orderDetail is null)
                throw new NotFoundException("Sale detail not found");
            
            return orderDetail;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the sale detail", e);
        }
    }
}