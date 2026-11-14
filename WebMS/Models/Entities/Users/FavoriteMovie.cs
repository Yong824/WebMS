using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMS.Models.Entities.Movies;

#nullable disable warnings

namespace WebMS.Models.Entities.Users;

[Index(nameof(UserId), nameof(MovieId), IsUnique = true)]
public class FavoriteMovie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /* Store the  user can still know 
     * what movie in their favorite is been removed by using the MovieName in this class */
    [MaxLength(255)]
    public string MovieTitleSnapshot { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public Guid? MovieId { get; set; }
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public Movie? Movie { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

}