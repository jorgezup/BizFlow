using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CustomerPreferences
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid CustomerId { get; init; }
    public Guid ProductId { get; init; }
    
    [Column(TypeName = "varchar(200)")]
    public List<string> PreferredPurchaseDays { get; set; } = [];
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; init; } = default!;
    public Product Product { get; init; } = default!;
}