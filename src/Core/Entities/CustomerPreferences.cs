using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CustomerPreferences
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid CustomerId { get; init; }
    public Guid ProductId { get; init; }

    [Column(TypeName = "varchar(20)")]
    public string PreferredPurchaseDay { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; }
    public Product Product { get; set; }
}