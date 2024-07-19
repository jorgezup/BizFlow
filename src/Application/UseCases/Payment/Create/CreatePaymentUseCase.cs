using Application.DTOs.Payment;
using Core.Interfaces;

namespace Application.UseCases.Payment.Create;

public class CreatePaymentUseCase(IUnitOfWork unitOfWork) : ICreatePaymentUseCase
{
    public async Task<Core.Entities.Payment> ExecuteAsync(PaymentRequest request)
    {
        try
        {
            var payment = request.MapToPayment();

            await unitOfWork.PaymentRepository.AddPaymentAsync(payment);
            await unitOfWork.CommitTransactionAsync();

            return payment;
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while creating payment", e);
        }
    }
}