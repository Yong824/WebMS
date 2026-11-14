using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable warnings

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(Name), IsUnique = true)]
public class FnbProduct
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; }

    [Precision(4,2)]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public List<CartItem> CartItems { get; set; } = [];
    public List<FnbOrderItem> FnbOrderItems { get; set; } = [];

}