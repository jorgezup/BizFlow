using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

public class Order
{
    public Guid Id { get; set; } // PK e FK para Sale
    public Guid CustomerId { get; init; }
    public DateTime OrderDate { get; set; }
    [Column(TypeName = "bit")] public bool Generated { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = [];
    
    public Customer Customer { get; set; }
    
    // Relacionamento one-to-one com Payment
    public Payment Payment { get; set; }
    
    // Relacionamento one-to-many com OrderLifeCycle
    public List<OrderLifeCycle> OrderLifeCycle { get; set; } = [];
}