using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetRemainingBalanceForCustomer;

public class GetRemainingBalanceForCustomerUseCase(IUnitOfWork unitOfWork) : IGetRemainingBalanceForCustomerUseCase
{
    public async Task<decimal> ExecuteAsync(Guid customerId)
    {
        try
        {
            var sales = (await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId)).ToList();
            if (sales.Count is 0)
                throw new NotFoundException($"Sales by customer: {customerId} not found");

            var totalAmount = sales.Sum(s => s.TotalAmount);
            
            var totalPayments = await GetTotalPaymentsForCustomerAsync(customerId);
            return totalAmount - totalPayments;
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting remaining balance for customer", e);
        }
    }

    private async Task<decimal> GetTotalPaymentsForCustomerAsync(Guid customerId)
    {
        var sales = await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId);
        var saleIds = sales.Select(s => s.Id);
        return (await unitOfWork.PaymentRepository.GetPaymentsBySaleIdsAsync(saleIds))
            .Where(p => p.Status == PaymentStatus.Completed)
            .Sum(p => p.Amount);
    }
}