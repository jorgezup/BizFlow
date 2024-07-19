using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.Update;

public class UpdatePaymentUseCase(IUnitOfWork unitOfWork) : IUpdatePaymentUseCase
{
    public async Task<Core.Entities.Payment> ExecuteAsync(Guid paymentId, PaymentStatus status)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByIdAsync(paymentId);
        if (payment is null) throw new NotFoundException("Payment not found");

        payment.Status = status;
        payment.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await unitOfWork.PaymentRepository.UpdatePaymentAsync(payment);
            await unitOfWork.CommitTransactionAsync();

            return payment;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while updating payment", e);
        }
    }
}