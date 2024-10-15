using Application.DTOs.Paginate;
using Core.Interfaces;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.GetAll;

public class GetAllPaginatedOrdersUseCase(IUnitOfWork unitOfWork) : IGetAllPaginatedOrdersUseCase
{
    public async Task<PaginatedResponse<OrderResponse>> ExecuteAsync(
        int page, 
        int pageSize, 
        Guid? customerId, 
        string? status, 
        DateTime? startDate, 
        DateTime? endDate, 
        string? sortColumn = "OrderDate", 
        string? sortDirection = "asc")
    {
        try 
        {
            var orders = await unitOfWork.OrderRepository.GetAllPaginatedOrdersAsync(
                page, 
                pageSize, 
                customerId, 
                status, 
                startDate, 
                endDate, 
                sortColumn, 
                sortDirection
            );
            
            var totalRecords = await unitOfWork.OrderRepository.GetTotalPaginatedOrdersAsync(customerId, status, startDate, endDate);

            return new PaginatedResponse<OrderResponse>
            {
                Data = orders.ToList(),
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };

        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting all orders", e);
        }
    }
}