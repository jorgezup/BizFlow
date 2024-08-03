using Application.DTOs.Payment;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetById;

public class GetPaymentByIdUseCase(IUnitOfWork unitOfWork) : IGetPaymentByIdUseCase
{
    public async Task<PaymentResponse> ExecuteAsync(Guid paymentId)
    {
        try
        {
            var payment = await unitOfWork.PaymentRepository.GetPaymentByIdAsync(paymentId);
            
            if (payment is null)
                throw new NotFoundException("Payment not found");
            
            return payment.MapToPaymentResponse();
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the payment", e);
        }
    }
}