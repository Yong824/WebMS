using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebMS.Models.Entities.Commerce;

public class Banner
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string? LinkUrl { get; set; }
    
    public bool IsActive { get; set; }

    public int SortOrder { get; set; }

    public DateOnly? StartDate { get; set; }   // null = show immediately

    public DateOnly? EndDate { get; set; }   // null = never expires

}