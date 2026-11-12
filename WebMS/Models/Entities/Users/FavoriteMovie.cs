using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Movies;

namespace WebMS.Models.Entities.Users;

[Index(nameof(UserId), nameof(MovieId), IsUnique = true)]
public class FavoriteMovie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /* Movie is nullable because in case movie is removed, the user can still know 
     * what movie in their favorite is been removed by using the MovieName in this class */
    public Guid? MovieId { get; set; }
    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }

    [MaxLength(200)]
    public string MovieTitleSnapshot { get; set; } = string.Empty;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

}