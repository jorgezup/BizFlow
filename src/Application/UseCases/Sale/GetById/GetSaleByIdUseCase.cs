using Application.DTOs.Sale;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Sale.GetById;

public class GetSaleByIdUseCase(IUnitOfWork unitOfWork) : IGetSaleByIdUseCase
{
    public async Task<SaleResponse> ExecuteAsync(Guid id)
    {
        var sale = await unitOfWork.SaleRepository.GetByIdAsync(id);

        if (sale is null) throw new NotFoundException("Sale not found");

        return sale.MapToSaleResponse();
    }
}