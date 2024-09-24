using Application.DTOs.OrderDetail;

namespace Application.Service.OrderDetail;

public interface IOrderDetailService
{
    public Task<Core.DTOs.OrderDetailResponse> UpdateOrderDetailService(Guid id, UpdateOrderDetailRequest request);
}