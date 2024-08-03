using Application.DTOs.Payment;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.Create;

public class CreatePaymentUseCase(IUnitOfWork unitOfWork) : ICreatePaymentUseCase
{
    public async Task<PaymentResponse> ExecuteAsync(PaymentRequest request)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            //add validator paymentMethod and decimal
            var sale = await unitOfWork.SaleRepository.GetByIdAsync(request.SaleId);
            if (sale is null)
                throw new NotFoundException("Sale not found");

            var payment = request.MapToPayment();

            await unitOfWork.PaymentRepository.AddPaymentAsync(payment);
            await unitOfWork.CommitTransactionAsync();

            return payment.MapToPaymentResponse();
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating payment", e);
        }
    }
}