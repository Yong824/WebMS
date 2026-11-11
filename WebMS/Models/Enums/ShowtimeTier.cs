// ShowtimeTier.cs
namespace WebMS.Models.Enums;

public enum ShowtimeTier
{
    Matinee = 0,    // before ~5 PM
    Regular = 1,    // weekday evening
    Prime = 2     // weekend / holiday evening
}

public static class ShowtimeTierExtensions
{
    public static decimal GetDefaultMultiplier(this ShowtimeTier tier) => tier switch
    {
        ShowtimeTier.Matinee => 0.80m,
        ShowtimeTier.Regular => 1.00m,
        ShowtimeTier.Prime => 1.20m,
        _ => 1.00m
    };

    public static string GetDisplayName(this ShowtimeTier tier) => tier switch
    {
        ShowtimeTier.Matinee => "Matinee",
        ShowtimeTier.Regular => "Regular",
        ShowtimeTier.Prime => "Prime (Weekend)",
        _ => tier.ToString()
    };

    /// <summary>Auto-suggests a tier from the showtime. Admin can override.</summary>
    public static ShowtimeTier FromDateTime(DateTime showtime)
    {
        var isWeekend = showtime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        if (isWeekend && showtime.Hour >= 17) 
            return ShowtimeTier.Prime;

        if (showtime.Hour < 17) 
            return ShowtimeTier.Matinee;

        return ShowtimeTier.Regular;
    }
}