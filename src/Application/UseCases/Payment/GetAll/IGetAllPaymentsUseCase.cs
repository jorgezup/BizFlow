using Application.DTOs.Paginate;
using Core.DTOs;

namespace Application.UseCases.Payment.GetAll;

public interface IGetAllPaymentsUseCase
{
    public Task<PaginatedResponse<PaymentResponse>> ExecuteAsync(int page,
        int pageSize,
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn = "PaymentDate",
        string? sortDirection = "desc");
}