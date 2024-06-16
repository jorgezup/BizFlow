using Core.Models.Product;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>?> GetAllAsync();
    Task<ProductResponse?> GetByIdAsync(Guid id);
    Task<ProductResponse> AddAsync(ProductRequest product);
    Task<ProductResponse?> UpdateAsync(ProductUpdateRequest product);
    Task DeleteAsync(Guid id);
}