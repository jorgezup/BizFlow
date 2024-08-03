using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class SaleDetail
{
    public Guid Id { get; init; }
    public Guid SaleId { get; init; }
    public Guid ProductId { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Subtotal { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public Sale Sale { get; set; }
    public Product Product { get; set; }
}