namespace WebMS.Models.Enums;

public enum MovieFormat
{
    Format2D = 0,
    Format3D = 1,
    Imax = 2,
    Format4DX = 3
}

public static class MovieFormatExtensions
{
    public static string ToDisplay(this MovieFormat f) => f switch
    {
        MovieFormat.Format2D => "2D",
        MovieFormat.Format3D => "3D",
        MovieFormat.Imax => "IMAX",
        MovieFormat.Format4DX => "4DX",
        _ => throw new ArgumentOutOfRangeException(nameof(f))
    };

    public static decimal GetDefaultModifier(this MovieFormat format) => format switch
    {
        MovieFormat.Format2D => 0.00m,
        MovieFormat.Format3D => 3.00m,
        MovieFormat.Imax => 10.00m,
        MovieFormat.Format4DX => 15.00m,
        _ => 0.00m
    };
}

