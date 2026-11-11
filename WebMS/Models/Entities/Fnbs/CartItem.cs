using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(CartId), nameof(ProductId), IsUnique = true)]
public class CartItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public int Quantity { get; set; }

    [Required]
    public Guid CartId { get; set; }
    [ForeignKey(nameof(CartId))]
    public FnbCart FnbCart { get; set; } = null!;

    [Required]
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public FnbProduct FnbProduct { get; set; } = null!;

}