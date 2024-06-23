namespace Application.UseCases.Product.Delete;

public interface IDeleteProduct
{
    public Task<bool> ExecuteAsync(Guid productId);
}