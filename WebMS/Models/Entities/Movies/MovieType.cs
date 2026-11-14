using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Users;

#nullable disable warnings

namespace WebMS.Models.Entities.Movies;

[Index(nameof(Name), IsUnique = true)]
public class MovieType
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } 

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Movie> Movies { get; set; } = [];

}