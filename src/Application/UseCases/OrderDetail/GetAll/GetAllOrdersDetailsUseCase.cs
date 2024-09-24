using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.OrderDetail.GetAll;

public class GetAllOrdersDetailsUseCase(IUnitOfWork unitOfWork) : IGetAllOrdersDetailsUseCase
{
    public async Task<IEnumerable<OrderDetailResponse>> ExecuteAsync()
    {
        try
        {
            var orderDetails = await unitOfWork.OrderDetailRepository.GetAllAsync();

            if (orderDetails is null || orderDetails.Count is 0)
                throw new NotFoundException("Sale details not found");

            return orderDetails;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting all sale details", e);
        }
    }
}