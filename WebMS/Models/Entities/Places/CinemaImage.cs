using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable warnings

namespace WebMS.Models.Entities.Places;

public class CinemaImage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; } 

    [MaxLength(255)]
    public string FilePath { get; set; } 

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; } = false;

    public Guid CinemaId { get; set; }
    public Cinema Cinema { get; set; } 

}
