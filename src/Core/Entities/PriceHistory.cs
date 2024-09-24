using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class PriceHistory
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ProductId { get; init; }
    [Column(TypeName = "decimal(18,4)")] public decimal Price { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public Product Product { get; init; } = null!;
}