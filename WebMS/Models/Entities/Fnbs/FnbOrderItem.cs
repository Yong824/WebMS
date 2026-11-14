using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Commerce;

#nullable disable warnings

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(TransactionId))]
[Index(nameof(FnbProductId))]
public class FnbOrderItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Precision(6,2)]
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    [MaxLength(255)]
    public string? Note { get; set; }

    public Guid FnbProductId { get; set; }
    public FnbProduct FnbProduct { get; set; }

    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; }

}