using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.OrderDetail.GetBySaleId;

public class GetOrderDetailByOrderIdUseCase(IUnitOfWork unitOfWork) : IGetOrderDetailBySaleIdUseCase
{
    public async Task<IEnumerable<OrderDetailResponse>> ExecuteAsync(Guid id)
    {
        try
        {
            var orderDetails = await unitOfWork.OrderDetailRepository.GetByOrderIdAsync(id);

            if (orderDetails is null || orderDetails.Count == 0)
                throw new NotFoundException("Sale details not found");
            
            return orderDetails;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the sale detail", e);
        }
    }
}