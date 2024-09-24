using Core.DTOs;

namespace Application.UseCases.OrderDetail.GetAll;

public interface IGetAllOrdersDetailsUseCase
{
    public Task<IEnumerable<OrderDetailResponse>> ExecuteAsync();
}