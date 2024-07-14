using Application.DTOs.Sale;
using Application.DTOs.SaleDetail;
using Application.UseCases.Product.GetById;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Sale.Create;

public class CreateSaleUseCase(
    IUnitOfWork unitOfWork,
    IValidator<SaleRequest> validator) : ICreateSaleUseCase
{
    public async Task<SaleResponse> ExecuteAsync(SaleRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new DataContractValidationException("Invalid sale data", validationResult.Errors);
        }

        var sale = request.MapToSale();

        await unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var detailRequest in request.SaleDetails)
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(detailRequest.ProductId);
                if (product is null)
                {
                    throw new NotFoundException("Product not found");
                }

                var saleDetail = new SaleDetailRequest
                {
                    SaleId = sale.Id,
                    ProductId = detailRequest.ProductId,
                    Quantity = detailRequest.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = detailRequest.Quantity * product.Price
                };

                await unitOfWork.SaleDetailRepository.AddAsync(saleDetail.MapToSaleDetail());
                sale.TotalAmount += saleDetail.Subtotal;
            }

            sale.Status = "CREATED";

            await unitOfWork.SaleRepository.AddAsync(sale);
            await unitOfWork.CommitTransactionAsync();

            return sale.MapToSaleResponse();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}