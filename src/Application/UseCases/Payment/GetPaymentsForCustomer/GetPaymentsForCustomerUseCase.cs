using Application.DTOs.Payment;
using Core.Enums;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetTotalPaymentsForCustomer;

public class GetPaymentsForCustomerUseCase(IUnitOfWork unitOfWork) : IGetPaymentsForCustomerUseCase
{
    public async Task<IEnumerable<PaymentsForCustomerResponse>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var sales = await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId);
            var saleIds = sales.Select(s => s.Id);
            var payments = await unitOfWork.PaymentRepository.GetPaymentsBySaleIdsAsync(saleIds);
            var completedPayments = payments.Where(p => p.Status == PaymentStatus.Completed);

            var paymentByMethodAndDateResponses = completedPayments.ToList()
                .GroupBy(p => new { p.SaleId, p.Id, p.PaymentMethod, p.PaymentDate })
                .Select(group => new PaymentsForCustomerResponse(
                    group.Key.SaleId,
                    group.Key.Id,
                    Enum.GetName(group.Key.PaymentMethod),
                    group.Key.PaymentDate,
                    group.Sum(p => p.Amount)
                ))
                .ToList();

            return paymentByMethodAndDateResponses;
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting payments for customer", e);
        }
    }
}