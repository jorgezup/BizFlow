using Application.DTOs.Sale;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.GetById;

public class GetSaleByIdUseCase(IUnitOfWork unitOfWork) : IGetSaleByIdUseCase
{
    public async Task<SaleResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var sale = await unitOfWork.SaleRepository.GetByIdAsync(id);

            if (sale is null) throw new NotFoundException("Sale not found");

            return sale.MapToSaleResponse();
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting the sale", e);
        }
    }
}