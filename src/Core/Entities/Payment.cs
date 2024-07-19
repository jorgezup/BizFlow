using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    [Column(TypeName = "varchar(30)")] public PaymentStatus Status { get; set; }
    [Column(TypeName = "varchar(30)")] public PaymentMethod PaymentMethod { get; set; }
    [Column(TypeName = "varchar(50)")] public string TransactionId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Sale Sale { get; set; }
}