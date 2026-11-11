using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Commerce;

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(TransactionId))]
[Index(nameof(FnbProductId))]
public class FnbOrderItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Required]
    public int Quantity { get; set; }

    public string? Note { get; set; }

    [Required]
    public Guid FnbProductId { get; set; }
    [ForeignKey(nameof(FnbProductId))]
    public FnbProduct FnbProduct { get; set; } = null!;

    [Required]
    public Guid TransactionId { get; set; }
    [ForeignKey(nameof(TransactionId))]
    public Transaction Transaction { get; set; } = null!;

}