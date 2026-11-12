using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebMS.Models.Enums;

namespace WebMS.Models.ViewModels.Admin;

public class CreateMovieViewModel
{
    [StringLength(255)]
    [DisplayName("Title")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [StringLength(255)]
    public string Director { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [DisplayName("Release Date")]
    public DateTime ReleaseAt { get; set; } = DateTime.UtcNow.Date;

    [Range(1, 500)]
    [DisplayName("Duration (minutes)")]
    public int Duration { get; set; } = 120;

    [MaxLength(500)]
    [DisplayName("Trailer URL (optional)")]
    public string? TrailerPath { get; set; }

    [Range(0, 500)]
    [DataType(DataType.Currency)]
    [DisplayName("Base Price (RM)")]
    public decimal BasePrice { get; set; } = 15.00m;

    [DisplayName("Movie Type")]
    public Guid MovieTypeId { get; set; }

    public MovieRating Rating { get; set; } = MovieRating.G;

    // TODO
    [DisplayName("Movie Images")]
    public List<IFormFile> Images { get; set; } = new();
}