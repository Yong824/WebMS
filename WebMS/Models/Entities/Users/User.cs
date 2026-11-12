using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Users;

[Index(nameof(Email), IsUnique = true)]     // For fast lookups, with index SQL Server can lookups for a record by using B-Tree
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    /// Hashed password
    [MaxLength(500)]
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public DateOnly Birthday { get; set; }

    public UserRole Role { get; set; } = UserRole.Customer;

    public UserStatus Status { get; set; } = UserStatus.Normal;

    // When the freeze/block started. 
    public DateTime? FrozenAt { get; set; }

    // When the freeze ends. 
    public DateTime? FrozenUntil { get; set; }

    // ─── Audit ───
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool EmailConfirmed { get; set; } = false;

    // ─── Navigation ───
    // public ICollection<MovieTransaction> Transactions { get; set; } = new List<MovieTransaction>();

    // ─── Computed (NOT columns) ───

    // A frozen user whose FrozenUntil has passed is effectively active again.</summary>
    [NotMapped]
    public bool IsActive =>
        Status == UserStatus.Normal ||
        (Status == UserStatus.Frozen && FrozenUntil.HasValue && FrozenUntil.Value <= DateTime.UtcNow);

    [NotMapped]
    public bool IsAdmin => Role == UserRole.Admin;
}