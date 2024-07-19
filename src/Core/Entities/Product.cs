using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product
{
    public Guid Id { get; init; }

    [Column(TypeName = "varchar(200)")]
    public required string Name { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string? Description { get; set; }

    [Column(TypeName = "varchar(200)")]
    public required string UnitOfMeasure { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public List<PriceHistory> PriceHistories { get; init; } = null!;
        
    public List<SaleDetail> SaleDetails { get; init; } = null!;

    public List<CustomerPreferences> CustomerPreferences { get; init; } = null!;
}