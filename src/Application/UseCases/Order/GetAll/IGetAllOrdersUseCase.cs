using Application.DTOs.Paginate;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.GetAll;

public interface IGetAllOrdersUseCase
{
    public Task<PaginatedResponse<OrderResponse>> ExecuteAsync(int page,
        int pageSize,
        Guid? customerId,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn = "OrderDate",
        string? sortDirection = "asc");
}