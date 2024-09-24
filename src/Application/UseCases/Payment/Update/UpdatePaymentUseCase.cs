using Application.DTOs.Payment;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using PaymentResponse = Core.DTOs.PaymentResponse;

namespace Application.UseCases.Payment.Update;

public class UpdatePaymentUseCase(IUnitOfWork unitOfWork) : IUpdatePaymentUseCase
{
    public async Task<PaymentResponse> ExecuteAsync(Guid paymentId, PaymentUpdateRequest request)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByIdAsync(paymentId);
        if (payment is null) 
            throw new NotFoundException("Payment not found");
        
        var order = await unitOfWork.OrderRepository.GetByIdAsync(payment.OrderId);
        if (order is null)
            throw new NotFoundException("Order not found");

        if (!Enum.IsDefined(enumType: typeof(PaymentMethod), request.PaymentMethod))
            throw new BadRequestException("Invalid payment method");
        
        if (request.Amount <= 0)
            throw new BadRequestException("Invalid amount");
        
        var paymentToUpdate = new Core.Entities.Payment
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = (decimal)request.Amount,
            PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), request.PaymentMethod),
            PaymentDate = request.PaymentDate ?? DateTime.Now,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = payment.CreatedAt
        };

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await unitOfWork.PaymentRepository.UpdatePaymentAsync(paymentToUpdate);
            await unitOfWork.CommitTransactionAsync();

            return payment;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while updating payment", e);
        }
    }
}