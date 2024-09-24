using Application.DTOs.Paginate;
using Application.DTOs.Payment;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetPendingPayments;

public class GetPendingPaymentsUseCase(IUnitOfWork unitOfWork) : IGetPendingPaymentsUseCase
{
    public async Task<PaginatedResponse<PendingPaymentResponse>> ExecuteAsync(Guid? customerId, DateTime? startDate,
        DateTime? endDate, int page, int pageSize)
    {
        // Get all completed orders - Status: Completed means that is a sale
        var ordersCompleted = await unitOfWork.OrderRepository.GetAllOrdersWithFiltersAsync(
            page: page,
            pageSize: pageSize,
            customerId: customerId,
            status: Status.Completed.ToString(),
            startDate: startDate,
            endDate: endDate,
            sortColumn: null,
            sortDirection: null
        );

        // Get all payments for the customer
        var allPayments = await unitOfWork.PaymentRepository.GetAllPaymentsWithFiltersAsync(
            page: page,
            pageSize: pageSize,
            customerId: customerId,
            startDate: startDate,
            endDate: endDate,
            sortColumn: null,
            sortDirection: null);

        // Filter completed orders that don't have associated payments - means that they have not been paid
        // var ordersCompletedWithoutPayment = ordersCompleted
        //     .Where(order => !allPayments
        //         .Select(p => p.OrderId)
        //         .Contains(order.Id))
        //     .ToList();
        
        var ordersCompletedWithoutPayment = ordersCompleted
            .Where(order => string.IsNullOrWhiteSpace(order.PaymentMethod))
            .ToList();
        
        // Paginate
        var paginatedOrders = ordersCompletedWithoutPayment
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResponse<PendingPaymentResponse>
        {
            Result = new PendingPaymentResponse
            {
                Orders = paginatedOrders.Select(order => order).ToList(),
                CustomerPendingPayment = paginatedOrders
                    .GroupBy(order => order.CustomerId)
                    .Select(group => new CustomerPendingPayment
                    (
                        CustomerId: group.Key,
                        CustomerName: group.First().CustomerName,
                        TotalPendingAmount: group.Sum(o => o.TotalAmount)
                    )).ToList(),
                TotalPendingAmount = paginatedOrders.Sum(order => order.TotalAmount),
            },
            TotalRecords = ordersCompletedWithoutPayment.Count,
            PageSize = pageSize,
            CurrentPage = page
        };
    }
}