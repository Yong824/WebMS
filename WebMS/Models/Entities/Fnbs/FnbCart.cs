using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Users;

#nullable disable warnings

namespace WebMS.Models.Entities.Fnbs;

[Index(nameof(UserId))]
public class FnbCart
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    [MaxLength(255)]
    public string? SessionKey { get; set; } // For guest carts

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public List<CartItem> CartItems { get; set; } = [];

}