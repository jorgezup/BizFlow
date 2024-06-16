using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product
{
    [Column(TypeName = "unique identifier")]
    public Guid ProductId { get; init; }

    [Column(TypeName = "varchar(200)")] 
    public required string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")] 
    public required decimal Price { get; set; }

    [Column(TypeName = "varchar(200)")] 
    public string? Category { get; set; }
    
    // public List<SaleItem> SaleItems { get; set; } = [];
    
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}