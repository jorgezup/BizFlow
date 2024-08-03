using Application.DTOs.Payment;
using Core.Enums;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetRemainingBalanceForCustomer;

public class GetRemainingBalanceForCustomerUseCase(IUnitOfWork unitOfWork) : IGetRemainingBalanceForCustomerUseCase
{
    public async Task<IEnumerable<PaymentBalanceResponse>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var paymentBalanceResponses = await GetPaymentBalanceResponsesForCustomerAsync(customerId);
            return paymentBalanceResponses;
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting remaining balance for customer", e);
        }
    }

    private async Task<IEnumerable<PaymentBalanceResponse>> GetPaymentBalanceResponsesForCustomerAsync(Guid customerId)
    {
        var sales = await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId);
        var enumerable = sales.ToList();
        var saleIds = enumerable.Select(s => s.Id);
        var payments = await unitOfWork.PaymentRepository.GetPaymentsBySaleIdsAsync(saleIds);
        var completedPayments = payments.Where(p => p.Status == PaymentStatus.Completed);

        var paymentBalanceResponses = completedPayments.ToList()
            .GroupBy(p => p.SaleId)
            .Select(group => new PaymentBalanceResponse(
                group.Key,
                group.Sum(p => p.Amount),
                enumerable.First(s => s.Id == group.Key).TotalAmount - group.Sum(p => p.Amount))
            ).ToList();

        return paymentBalanceResponses;
    }
}