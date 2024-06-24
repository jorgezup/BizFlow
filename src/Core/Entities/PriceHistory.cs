using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class PriceHistory
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
    public DateTime CreatedAt { get; init; }
    
    public Product Product { get; init; }
    
    public PriceHistory()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
}