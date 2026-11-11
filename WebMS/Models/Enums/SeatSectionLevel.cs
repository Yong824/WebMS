namespace WebMS.Models.Enums;

public enum SeatSectionLevel
{
    Orchestra = 0,
    Mezzanine = 1,
    Balcony = 2
}

public static class SeatSectionLevelExtensions
{
    
    // Any update on here please update in /js/seat-layout-editor.js as well

    public static decimal GetDefaultModifier(this SeatSectionLevel level) => level switch
    {
        SeatSectionLevel.Orchestra => 3.00m,
        SeatSectionLevel.Mezzanine => 0.00m,
        SeatSectionLevel.Balcony => -2.00m,
        _ => throw new ArgumentOutOfRangeException(nameof(level))
    };

    public static string GetDefaultColor(this SeatSectionLevel level) => level switch
    {
        SeatSectionLevel.Orchestra => "#27ae60",
        SeatSectionLevel.Mezzanine => "#2980b9",
        SeatSectionLevel.Balcony => "#8e44ad",
        _ => "#000000"
    };

    public static int GetDefaultDisplayOrder(this SeatSectionLevel level) => level switch
    {
        SeatSectionLevel.Orchestra => 1,   // front
        SeatSectionLevel.Mezzanine => 2,
        SeatSectionLevel.Balcony => 3,
        _ => 99
    };
}