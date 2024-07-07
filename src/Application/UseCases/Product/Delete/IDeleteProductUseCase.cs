namespace Application.UseCases.Product.Delete;

public interface IDeleteProductUseCase
{
    public Task<bool> ExecuteAsync(Guid productId);
}