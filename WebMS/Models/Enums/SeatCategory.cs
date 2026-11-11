namespace WebMS.Models.Enums;

public enum SeatCategory
{
    Standard = 0,
    OKU = 1,
    VIP = 2
}

public static class SeatCategoryExtensions
{
    public static decimal GetDefaultModifier(this SeatCategory category) => category switch
    {
        SeatCategory.Standard => 0.00m,
        SeatCategory.OKU => 0.00m,   // legally should not cost more
        SeatCategory.VIP => 5.00m,
        _ => 0.00m
    };

    public static string GetDisplayName(this SeatCategory category) => category switch
    {
        SeatCategory.Standard => "Standard",
        SeatCategory.OKU => "OKU (Wheelchair)",
        SeatCategory.VIP => "VIP / Recliner",
        _ => category.ToString()
    };
}