using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.SaleDetail.Create;

public class CreateSaleDetailUseCase(
    IUnitOfWork unitOfWork,
    IValidator<SaleDetailRequest> validator)
    : ICreateSaleDetailUseCase
{
    public async Task<SaleDetailResponse> ExecuteAsync(SaleDetailRequest request)
    {
        var saleDetail = request.MapToSaleDetail();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new DataContractValidationException("Invalid sale detail data", validationResult.Errors);
        }

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.SaleDetailRepository.AddAsync(saleDetail);
            await unitOfWork.CommitTransactionAsync();
            return saleDetail.MapToSaleDetailResponse();
        }
        catch (Exception ex) when (ex is not DataContractValidationException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating the sale detail", ex);
        }
    }
}