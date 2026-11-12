using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Users;
using WebMS.Models.Entities.Commerce;

namespace WebMS.Models.Entities.Places;

public class Cinema
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    public Guid? MerchantId { get; set; }

    [ForeignKey(nameof(MerchantId))]
    public Merchant? Merchant { get; set; }

    // ─── Audit ───
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Nullable, in case creator admin is been deleted, cinema still exists
    public Guid? CreatedByUserId { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public User? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedByUserId { get; set; }

    [ForeignKey(nameof(UpdatedByUserId))]
    public User? UpdatedBy { get; set; }

    // ─── Navigation ───
    // public ICollection<Hall> Halls { get; set; } = new List<Hall>();
}