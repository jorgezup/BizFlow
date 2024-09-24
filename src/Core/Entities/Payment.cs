using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

public class Payment
{
    public Guid Id { get; set; }

    // Mark CustomerId as the foreign key for the Customer property
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }

    [Column(TypeName = "decimal(18,4)")] 
    public decimal Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    [Column(TypeName = "varchar(30)")] 
    public PaymentMethod PaymentMethod { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation property to Order
    public Order Order { get; set; }

}