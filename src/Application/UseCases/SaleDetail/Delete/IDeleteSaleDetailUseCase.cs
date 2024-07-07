namespace Application.UseCases.SaleDetail.Delete;

public interface IDeleteSaleDetailUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}