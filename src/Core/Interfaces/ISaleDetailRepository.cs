using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface ISaleDetailRepository
{
    Task<OrderDetailResponse?> GetByIdAsync(Guid id);
    Task<List<OrderDetailResponse>> GetAllAsync();
    Task<List<OrderDetailResponse>> GetByOrderIdAsync(Guid id);
    Task AddAsync(OrderDetail orderDetail);
    Task UpdateAsync(OrderDetail orderDetail);
    Task DeleteAsync(Guid id);
}