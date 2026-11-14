namespace WebMS.Models.Enums;

public enum MovieStatus
{
    ComingSoon = 0,   // Not bookable yet, but shown in catalog
    NowShowing = 1,   // Active, bookable if schedules exist
    Archived = 2      // Removed from catalog, no longer bookable
}