using Application.DTOs.Paginate;
using Core.Interfaces;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.UseCases.Order.GetAll;

public class GetAllOrdersUseCase(IUnitOfWork unitOfWork) : IGetAllOrdersUseCase
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
            var orders = await unitOfWork.OrderRepository.GetAllOrdersWithFiltersAsync(
                page, 
                pageSize, 
                customerId, 
                status, 
                startDate, 
                endDate, 
                sortColumn, 
                sortDirection
            );
            
            var totalRecords = await unitOfWork.OrderRepository.GetTotalOrdersWithFiltersCountAsync(customerId, status, startDate, endDate);

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