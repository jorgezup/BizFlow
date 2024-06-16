using Core.Entities;
using Core.Models.Product;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product> AddAsync(ProductRequest product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(Guid productId);
    Task<Product?> GetByNameAsync(string name); 
}