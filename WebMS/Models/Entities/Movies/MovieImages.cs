using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMS.Models.Entities.Movies;

public class MovieImage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    [Required]
    public int SortOrder{ get; set; }

    [Required]
    public bool IsPrimary { get; set; } = false;

    [Required]
    public Guid MovieId { get; set; }
    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; } = null!;

}
