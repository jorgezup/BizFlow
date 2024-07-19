using Application.UseCases.Payment.GetPaymentBySaleId;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Payment.GetBySaleId;

public class GetPaymentsBySaleIdUseCaseUseCase(IUnitOfWork unitOfWork) : IGetPaymentsBySaleIdUseCase
{
    public async Task<IEnumerable<Core.Entities.Payment>> ExecuteAsync(Guid saleId)
    {
        try
        {
            var payments = await unitOfWork.PaymentRepository.GetPaymentsBySaleIdAsync(saleId);

            if (payments is null)
                throw new NotFoundException("Payments not found");

            return payments;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting payments by sale id", e);
        }
    }
}