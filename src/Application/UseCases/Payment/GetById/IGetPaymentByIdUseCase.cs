namespace Application.UseCases.Payment.GetById;

public interface IGetPaymentByIdUseCase
{
    public Task<Core.Entities.Payment> ExecuteAsync(Guid paymentId);
}