using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Customer
{
    public required Guid Id { get; init; }

    [Column(TypeName = "varchar(200)")] public required string Name { get; set; }

    [Column(TypeName = "varchar(200)")] public string? Email { get; set; }

    [Column(TypeName = "varchar(30)")] public string? PhoneNumber { get; set; }

    [Column(TypeName = "varchar(200)")] public string? Address { get; set; }
    
    [Column(TypeName = "bit")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public List<CustomerPreferences> CustomerPreferences { get; init; } = [];
    public List<Order> Orders { get; set; } = [];
    // public List<Payment> Payments { get; set; } = [];
}