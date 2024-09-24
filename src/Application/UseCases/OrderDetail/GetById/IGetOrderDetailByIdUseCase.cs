using Core.DTOs;

namespace Application.UseCases.OrderDetail.GetById;

public interface IGetOrderDetailByIdUseCase
{
    public Task<OrderDetailResponse> ExecuteAsync(Guid id);
}