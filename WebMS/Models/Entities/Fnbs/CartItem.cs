using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable warnings

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(FnbCartId), nameof(FnbProductId), IsUnique = true)]
public class CartItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Quantity { get; set; }

    public Guid FnbCartId { get; set; }
    public FnbCart FnbCart { get; set; }

    public Guid FnbProductId { get; set; }
    public FnbProduct FnbProduct { get; set; }

}