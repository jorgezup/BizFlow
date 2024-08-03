using Application.DTOs.Payment;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.Update;

public class UpdatePaymentUseCase(IUnitOfWork unitOfWork) : IUpdatePaymentUseCase
{
    public async Task<PaymentResponse> ExecuteAsync(Guid paymentId, PaymentUpdateRequest request)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByIdAsync(paymentId);
        if (payment is null) throw new NotFoundException("Payment not found");
        
        payment.UpdatePayment(request);

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await unitOfWork.PaymentRepository.UpdatePaymentAsync(payment);
            await unitOfWork.CommitTransactionAsync();

            return payment.MapToPaymentResponse();
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while updating payment", e);
        }
    }
}