using Application.DTOs.OrderDetail;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using OrderDetailResponse = Core.DTOs.OrderDetailResponse;

namespace Application.UseCases.OrderDetail.Create;

public class CreateOrderDetailUseCase(
    IUnitOfWork unitOfWork,
    IValidator<OrderDetailRequest> validator)
    : ICreateOrderDetailUseCase
{
    public async Task<OrderDetailResponse> ExecuteAsync(OrderDetailRequest request)
    {
        var saleDetail = request.MapToOrderDetail();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new DataContractValidationException("Invalid sale detail data", validationResult.Errors);
        }

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.OrderDetailRepository.AddAsync(saleDetail);
            await unitOfWork.CommitTransactionAsync();
            return saleDetail.MapToOrderDetailResponse();
        }
        catch (Exception ex) when (ex is not DataContractValidationException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating the sale detail", ex);
        }
    }
}