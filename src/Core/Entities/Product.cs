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

    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
    
    [Column(TypeName = "bit")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public List<PriceHistory> PriceHistories { get; init; } = [];
}