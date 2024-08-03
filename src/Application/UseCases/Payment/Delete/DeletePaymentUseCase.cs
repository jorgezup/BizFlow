using Core.Interfaces;

namespace Application.UseCases.Payment.Delete;

public class DeletePaymentUseCase(IUnitOfWork unitOfWork) : IDeletePaymentUseCase
{
    public async Task<bool> ExecuteAsync(Guid paymentId)
    {
        var payment = await unitOfWork.PaymentRepository.GetPaymentByIdAsync(paymentId);
        if (payment is null)
            return false;
        await unitOfWork.BeginTransactionAsync();
        try
        {
            await unitOfWork.PaymentRepository.DeletePaymentAsync(paymentId);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the payment", ex);
        }
    }
}