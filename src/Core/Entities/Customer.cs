using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Customer
{
    public required Guid CustomerId { get; init; }

    [Column(TypeName = "varchar(200)")] public required string Name { get; set; }

    [Column(TypeName = "varchar(200)")] public string? Email { get; set; }

    [Column(TypeName = "varchar(30)")] public string? PhoneNumber { get; set; }

    [Column(TypeName = "varchar(200)")] public string? Address { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}