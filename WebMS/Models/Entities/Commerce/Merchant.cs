using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebMS.Models.Entities.Places;

#nullable disable warnings

namespace WebMS.Models.Entities.Commerce;

[Index(nameof(Name), IsUnique = true)]
public class Merchant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string SecretKey { get; set; }

    // Other field pending, need to check with stripe API docs


    // -- Navigation -------------------------------------------------
    public List<Cinema> Cinemas { get; set; } = [];

}