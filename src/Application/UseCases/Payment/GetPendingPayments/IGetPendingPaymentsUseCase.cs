using Application.DTOs;
using Application.DTOs.Paginate;
using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetPendingPayments;

public interface IGetPendingPaymentsUseCase
{
    Task<PaginatedResponse<PendingPaymentResponse>> ExecuteAsync(Guid? customerId, DateTime? startDate, DateTime? endDate, int page, int pageSize);
}