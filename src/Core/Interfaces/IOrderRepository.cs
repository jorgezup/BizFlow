using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<OrderResponse> GetByIdAsync(Guid id);

    Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(
        Guid? customerId,
        string status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection);

    Task<IEnumerable<OrderResponse>> GetAllPaginatedOrdersAsync(
        int page,
        int pageSize,
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection);

    Task<int> GetTotalPaginatedOrdersAsync(
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate);

    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}