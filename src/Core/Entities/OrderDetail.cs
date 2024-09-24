using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class OrderDetail
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid ProductId { get; set; }
    [Column(TypeName = "decimal(18,4)")] public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,4)")] public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(18,4)")] public decimal Subtotal { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public Product Product { get; set; }
    [JsonIgnore] public Order Order { get; set; }
}