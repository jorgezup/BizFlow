using Application.DTOs.SaleDetail;
using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.SaleDetail.Update
{
    public class UpdateSaleDetailUseCase(IUnitOfWork unitOfWork) : IUpdateSaleDetailUseCase
    {
        public async Task<SaleDetailResponse> ExecuteAsync(Guid id, UpdateSaleDetailRequest request)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                var saleDetail = await unitOfWork.SaleDetailRepository.GetByIdAsync(id);

                if (saleDetail == null)
                    throw new NotFoundException("Sale detail not found");

                var saleDetailToUpdate = saleDetail.UpdateSaleDetail(request);

                await unitOfWork.SaleDetailRepository.UpdateAsync(saleDetailToUpdate);
                await unitOfWork.CommitTransactionAsync();

                return saleDetailToUpdate.MapToSaleDetailResponse();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("An error occurred while updating the sale detail", ex);
            }
        }
    }
}