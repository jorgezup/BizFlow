using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.GetByCustomerId;

public class GetSalesByCustomerId(IUnitOfWork unitOfWork) : IGetSalesByCustomerId
{
    public async Task<IEnumerable<Core.Entities.Sale>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var sales = await unitOfWork.SaleRepository.GetSalesByCustomerIdAsync(customerId);

            if (sales is null) throw new NotFoundException("Sales not found");

            return sales;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting sales by customer id", e);
        }
    }
}