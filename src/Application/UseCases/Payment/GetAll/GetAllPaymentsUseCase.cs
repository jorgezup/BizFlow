using Application.DTOs.Paginate;
using Core.DTOs;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetAll;

public class GetAllPaymentsUseCase(IUnitOfWork unitOfWork) : IGetAllPaymentsUseCase
{
    public async Task<PaginatedResponse<PaymentResponse>> ExecuteAsync(
        int page,
        int pageSize,
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn = "PaymentDate",
        string? sortDirection = "desc")
    {
        try
        {
            var payments = await unitOfWork.PaymentRepository.GetAllPaginatedPaymentsAsync(
                page,
                pageSize,
                customerId,
                startDate,
                endDate,
                sortColumn,
                sortDirection
            );

            var totalRecords =
                await unitOfWork.PaymentRepository.GetTotalPaginatedPaymentsAsync(customerId, startDate, endDate);

            return new PaginatedResponse<PaymentResponse>
            {
                Data = payments.ToList(),
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