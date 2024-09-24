using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<OrderResponse> GetByIdAsync(Guid id);

    Task<IEnumerable<OrderResponse>> GetAllOrdersWithFiltersAsync(
        int page,
        int pageSize,
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection);

    Task<int> GetTotalOrdersWithFiltersCountAsync(
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate);

    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}