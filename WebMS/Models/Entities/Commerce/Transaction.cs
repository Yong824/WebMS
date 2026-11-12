using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Users;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(UserId))]
[Index(nameof(CinemaId))]
[Index(nameof(OrderNumber), IsUnique = true)]
public class Transaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(20)]
    public string OrderNumber { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public Guid CinemaId { get; set; }

}