using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMS.Models.Entities.Places;

public class CinemaImage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; } = false;

    public Guid CinemaId { get; set; }
    [ForeignKey(nameof(CinemaId))]
    public Cinema Cinema { get; set; } = null!;

}
