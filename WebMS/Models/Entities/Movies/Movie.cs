using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebMS.Models.Entities.Users;
using WebMS.Models.Enums;

namespace WebMS.Models.Entities.Movies;

public class Movie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Director { get; set; } = string.Empty;

    [Required]
    public DateTime ReleaseAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int Duration { get; set; } = 120;

    public string? TrailerPath { get; set; } = string.Empty;

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal BasePrice { get; set; } = 15;    // default as 15.00

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public User? CreatedBy { get; set; }

    [Required]
    public Guid MovieTypeId { get; set; }

    [ForeignKey(nameof(MovieTypeId))]
    public MovieType Type { get; set; } = null!;

    public MovieRating Rating { get; set; } = MovieRating.G;

    public ICollection<MovieImage> Images { get; set; } = new List<MovieImage>();

}