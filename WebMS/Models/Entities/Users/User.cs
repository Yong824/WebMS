using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebMS.Models.Entities.Commerce;
using WebMS.Models.Entities.Fnbs;
using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Users;

[Index(nameof(Email), IsUnique = true)]     // For fast lookups, with index SQL Server can lookups for a record by using B-Tree
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public string Name { get; set; } 

    [MaxLength(20)]
    public string PhoneNumber { get; set; } 

    [MaxLength(255)]
    public string Email { get; set; }

    public bool EmailConfirmed { get; set; } = false;

    /// Hashed password
    [MaxLength(255)]
    [JsonIgnore]
    public string PasswordHash { get; set; } 

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

    // ─── Navigation ───
    public List<Transaction> Transactions { get; set; } = [];
    public List<FavoriteMovie> FavoriteMovies { get; set; } = [];
    public List<Movie> Movies { get; set; } = [];               // admin: the cinemas created by this user
    public List<Cinema> Cinemas { get; set; } = [];             // admin: the cinemas created by this user
    public List<Hall> Halls { get; set; } = [];                 // admin: the hall created by this user
    public List<Schedule> Schedules { get; set; } = [];         // admin: the schedule created by this user
    public List<MovieTicket> MovieTickets { get; set; } = [];   // admin: the ticket that scanned by this user, not the ticket owned by the user


    // ─── Computed (NOT columns) ───
    // A frozen user whose FrozenUntil has passed is effectively active again.</summary>
    [NotMapped]
    public bool IsActive =>
        Status == UserStatus.Normal ||
        (Status == UserStatus.Frozen && FrozenUntil.HasValue && FrozenUntil.Value <= DateTime.UtcNow);

    [NotMapped]
    public bool IsAdmin => Role == UserRole.Admin;
}