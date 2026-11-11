using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Enums;

namespace WebMS.Services;

public class PricingService
{
    // These methods fetch the default values from the Enum extensions.
    public decimal GetSectionModifier(SeatSectionLevel level) => level.GetDefaultModifier();
    public decimal GetFormatModifier(MovieFormat format) => format.GetDefaultModifier();
    public decimal GetCategoryModifier(SeatCategory category) => category.GetDefaultModifier();
    public decimal GetTimeMultiplier(ShowtimeTier tier) => tier.GetDefaultMultiplier();

    public decimal CalculateTicketPrice(Seat seat, Schedule schedule, Movie movie)
    {
        // 1. Anchor (default 15)
        decimal anchor = movie.BasePrice > 0 ? movie.BasePrice : 15.00m;

        // 2. Time multiplier (from schedule — frozen at creation)
        decimal afterTime = anchor * (schedule.TimeMultiplier > 0 ? schedule.TimeMultiplier : 1.0m);

        // 3. Additive modifiers using the methods in this class
        decimal sectionFee = seat.Section?.PriceModifier
                            ?? GetSectionModifier(seat.Section?.Name ?? SeatSectionLevel.Orchestra);

        decimal formatFee = GetFormatModifier(schedule.Format);
        decimal categoryFee = GetCategoryModifier(seat.Category ?? SeatCategory.Standard);

        return Math.Round(afterTime + sectionFee + formatFee + categoryFee, 2);
    }
}