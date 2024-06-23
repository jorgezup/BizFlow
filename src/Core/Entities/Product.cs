using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    [Column(TypeName = "varchar(200)")] public string Name { get; set; }
    [Column(TypeName = "varchar(200)")] public string Description { get; set; }
    [Column(TypeName = "varchar(200)")] public string UnitOfMeasure { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}