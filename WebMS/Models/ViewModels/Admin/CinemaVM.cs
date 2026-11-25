using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebMS.Models.Entities.Places;

namespace WebMS.Models.ViewModels.Admin;

#nullable disable warnings

public class CinemaVM
{
    public Guid Id { get; set; }

    public string DisplayId => $"{Id.ToString("N")[..3]}...{Id.ToString("N")[^3..]}";

    [StringLength(255)]
    public string Name { get; set; }

    [StringLength(20)]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; }

    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(255)]
    public string Address { get; set; }

    [StringLength(255)]
    public string? MerchantName { get; set; }

    public DateTime CreatedAt { get; set; }

    [StringLength(100)]
    public string? CreatedByUser{ get; set; }

    public List<Hall> Halls { get; set; } = [];
    public List<CinemaImage> Images { get; set; } = [];

    public int HallCount => Halls.Count;
    public int ImageCount => Images.Count;
}

// TODO: Create Cinema VM move to here