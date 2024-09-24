using Application.DTOs.Payment;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using PaymentResponse = Core.DTOs.PaymentResponse;

namespace Application.UseCases.Payment.Create;

public class CreatePaymentUseCase(IUnitOfWork unitOfWork) : ICreatePaymentUseCase
{
    public async Task<PaymentResponse> ExecuteAsync(PaymentRequest request)
    {
        var paymentExists = await unitOfWork.PaymentRepository.ExistsPaymentByOrderIdAsync(request.OrderId);
        if (paymentExists)
            throw new BadRequestException("Payment already exists");

        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
            throw new NotFoundException("Order not found");

        var latestOrderLifeCycle = order.Status;

        if (latestOrderLifeCycle != Status.Completed)
            throw new BadRequestException("Order not completed");

        var payment = request.MapToPayment();

        if (request.Amount is null or <= 0)
            payment.Amount = order.TotalAmount;
        else
            payment.Amount = (decimal)request.Amount;

        if (request.PaymentMethod is null)
            payment.PaymentMethod = PaymentMethod.Cash;
        else
        {
            if (!Enum.TryParse(typeof(PaymentMethod), request.PaymentMethod.ToString(),
                    out var parsedPaymentMethod))
                throw new BadRequestException("Invalid payment method.");

            payment.PaymentMethod = (PaymentMethod)parsedPaymentMethod;
        }

        try
        {
            await unitOfWork.BeginTransactionAsync();
            await unitOfWork.PaymentRepository.AddPaymentAsync(payment);

            await unitOfWork.CommitTransactionAsync();

            return new PaymentResponse(
                payment.Id,
                order.Id,
                order.CustomerId,
                order.CustomerName,
                payment.Amount,
                payment.PaymentDate,
                payment.PaymentMethod.ToString(),
                payment.CreatedAt,
                payment.UpdatedAt
                );
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating payment", e);
        }
    }
}