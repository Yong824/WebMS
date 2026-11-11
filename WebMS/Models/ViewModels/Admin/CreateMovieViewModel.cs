using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebMS.Models.Enums;

namespace WebMS.Models.ViewModels.Admin;

public class CreateMovieViewModel
{
    [Required, MaxLength(255)]
    [Display(Name = "Title")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Director { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseAt { get; set; } = DateTime.UtcNow.Date;

    [Required, Range(1, 500)]
    [Display(Name = "Duration (minutes)")]
    public int Duration { get; set; } = 120;

    [MaxLength(500)]
    [Display(Name = "Trailer URL (optional)")]
    public string? TrailerPath { get; set; }

    [Required, Range(0, 500)]
    [DataType(DataType.Currency)]
    [Display(Name = "Base Price (RM)")]
    public decimal BasePrice { get; set; } = 15.00m;

    [Required]
    [Display(Name = "Movie Type")]
    public Guid MovieTypeId { get; set; }

    public MovieRating Rating { get; set; } = MovieRating.G;

    [Display(Name = "Movie Images")]
    public List<IFormFile> Images { get; set; } = new();
}