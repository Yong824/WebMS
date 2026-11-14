using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Entities.Users;
using WebMS.Models.Enums;

#nullable disable warnings

namespace WebMS.Models.Entities.Movies;

public class Movie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [MaxLength(255)]
    public string Director { get; set; }

    public DateTime ReleaseAt { get; set; } = DateTime.UtcNow;

    public int Duration { get; set; } = 120;

    [MaxLength(255)]
    public string? TrailerPath { get; set; }

    [Precision(6,2)]
    public decimal BasePrice { get; set; } = 15.00m;    // default as 15.00

    // TODO: Status (IsPublished, IsDown); DownAt (DateTime)

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedByUserId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public User? UpdatedByUser { get; set; }

    public Guid TypeId { get; set; }
    [DeleteBehavior(DeleteBehavior.Restrict)]       // Not allow admin to remove Type if there is movies assigned with that movie type
    public MovieType Type { get; set; }

    public MovieRating Rating { get; set; } = MovieRating.G;

    public List<MovieImage> Images { get; set; } = [];
    public List<Schedule> Schedules { get; set; } = [];
    public List<FavoriteMovie> FavoriteMovies { get; set; } = [];

}