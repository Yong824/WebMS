using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Users;
using WebMS.Models.Entities.Commerce;

#nullable disable warnings

namespace WebMS.Models.Entities.Places;

public class Cinema
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } 

    [MaxLength(20)]
    public string PhoneNumber { get; set; } 

    [MaxLength(255)]
    public string Email { get; set; } 

    [MaxLength(255)]
    public string Address { get; set; }

    public Guid? MerchantId { get; set; }       // null = use default merchant as stated in appsettings.json
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Merchant? Merchant { get; set; }

    // ─── Audit ───
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Nullable, in case creator admin is been deleted, cinema still exists
    public Guid? CreatedByUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User? CreatedByUser { get; set; }

    // ─── Navigation ───
    public List<Hall> Halls { get; set; } = [];
    public List<Transaction> Transactions { get; set; } = [];
    public List<CinemaImage> Images { get; set; } = [];
}