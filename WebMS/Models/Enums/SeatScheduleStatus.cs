namespace WebMS.Models.Enums;

public enum SeatScheduleStatus
{
    Available = 0,
    Locked = 1,     // someone is doing payment for the seat
    Sold = 2,       // someone booked/paid for the seat
    Attended = 3,   // someone has paid and scanned to walk in to the cinema hall
    Absent = 4      // someone paid but no attend to the cinema
}
