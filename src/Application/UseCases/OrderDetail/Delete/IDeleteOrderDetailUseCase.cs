namespace Application.UseCases.OrderDetail.Delete;

public interface IDeleteOrderDetailUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}