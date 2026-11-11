using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Users;

namespace WebMS.Models.Entities.Movies;

[Index(nameof(Name), IsUnique = true)]
public class MovieType
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public string? description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]
    public User? CreatedBy { get; set; }

    public Guid? UpdatedByUserId { get; set; }
    [ForeignKey(nameof(UpdatedByUserId))]
    public User? UpdatedBy { get; set; }

}