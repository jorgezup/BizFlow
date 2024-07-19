using Application.DTOs.Sale;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.GetAll;

public class GetAllSalesUseCase(IUnitOfWork unitOfWork) : IGetAllSalesUseCase
{
    public async Task<IEnumerable<SaleResponse>> ExecuteAsync()
    {
        try
        {
            var sales = await unitOfWork.SaleRepository.GetAllAsync();

            var salesList = sales.ToList();
            if (sales is null || salesList.Count is 0)
                throw new NotFoundException("Sales not found");

            return salesList.Select(sale => sale.MapToSaleResponse());
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting all sales", e);
        }
    }
}