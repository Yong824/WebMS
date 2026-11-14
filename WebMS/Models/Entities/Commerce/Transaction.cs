using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Fnbs;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Users;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(UserId))]
[Index(nameof(CinemaId))]
[Index(nameof(OrderNumber), IsUnique = true)]
public class Transaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(20)]
    public string OrderNumber { get; set; }

    [Precision(6,2)]    // -9999.99 ~ 9999.99
    public decimal Subtotal { get; set; }

    [Precision(6, 2)]
    public decimal TaxAmount { get; set; }

    [Precision(6, 2)]
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    [MaxLength(255)]
    public string CinemaNameSnapshot { get; set; }  // in case cinema is been removed, keep the cinema name, and set the Cinema FK to null

    public Guid? UserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]        // if user account been deleted, the transaction record should be remain as well
    public User? User { get; set; }

    public Guid? CinemaId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Cinema? Cinema { get; set; }

    public List<MovieTicket> MovieTickets { get; set; } = [];
    public List<FnbOrderItem> FnbOrderItems { get; set; } = [];
}