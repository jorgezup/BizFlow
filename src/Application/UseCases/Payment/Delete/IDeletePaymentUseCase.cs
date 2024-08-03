namespace Application.UseCases.Payment.Delete;

public interface IDeletePaymentUseCase
{
    public Task<bool> ExecuteAsync(Guid paymentId);
}