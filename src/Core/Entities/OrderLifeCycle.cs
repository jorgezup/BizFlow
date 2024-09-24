using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Core.Enums;

namespace Core.Entities;

public class OrderLifeCycle
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid OrderId { get; set; } // Foreign Key

    [JsonIgnore] public Order Order { get; set; } // Navigation Property

    [Column(TypeName = "varchar(15)")] public Status Status { get; set; }

    public DateTime CreatedAt { get; set; }
}