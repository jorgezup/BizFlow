using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Sale
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime SaleDate { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal TotalAmount { get; set; }
    [Column(TypeName = "varchar(20)")] public required string Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public List<SaleDetail> SaleDetails { get; set; }

    public Customer Customer { get; set; }
    public List<Payment> Payments { get; set; } = [];
}