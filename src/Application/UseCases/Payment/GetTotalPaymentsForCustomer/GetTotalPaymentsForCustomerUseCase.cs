using Core.Enums;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetTotalPaymentsForCustomer;

public class GetTotalPaymentsForCustomerUseCase(IUnitOfWork unitOfWork) : IGetTotalPaymentsForCustomerUseCase
{
    public async Task<decimal> ExecuteAsync(Guid customerId)
    {
        try
        {
            var sales = await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId);
            var saleIds = sales.Select(s => s.Id);
            return (await unitOfWork.PaymentRepository.GetPaymentsBySaleIdsAsync(saleIds))
                .Where(p => p.Status == PaymentStatus.Completed)
                .Sum(p => p.Amount);
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting total payments for customer", e);
        }
    }
}