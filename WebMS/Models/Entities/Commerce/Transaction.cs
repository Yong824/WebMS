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

    [Required, MaxLength(20)]
    public string OrderNumber { get; set; } = string.Empty;

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal TaxAmount { get; set; }

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    [Required]
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public Guid CinemaId { get; set; }

}