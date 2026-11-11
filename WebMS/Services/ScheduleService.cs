using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Enums;
using WebMS.Models.ViewModels.Admin;

namespace WebMS.Services;

public class ScheduleService (DB db)
{

    // The 30-minute cleaning buffer
    public const int CleaningBufferMinutes = 30;

    public async Task<Schedule> CreateScheduleAsync(CreateScheduleDto dto)
    {
        var movie = await db.Movies.FindAsync(dto.MovieId)
            ?? throw new InvalidOperationException("Movie not found.");

        var hall = await db.Halls.FindAsync(dto.HallId)
            ?? throw new InvalidOperationException("Hall not found.");

        // 1. Calculate the cleaning buffer end time
        var newStart = dto.Showtime;
        var newEndWithCleaning = newStart.AddMinutes(movie.Duration + CleaningBufferMinutes);

        // 2. Check for overlaps in the same hall
        var existingSchedules = await db.Schedules
            .Include(s => s.Movie)
            .Where(s => s.HallId == dto.HallId)
            .ToListAsync();

        foreach (var existing in existingSchedules)
        {
            var existingEndWithCleaning = existing.Showtime.AddMinutes(existing.Movie.Duration + CleaningBufferMinutes);

            // Overlap logic
            if (newStart < existingEndWithCleaning && newEndWithCleaning > existing.Showtime)
            {
                throw new InvalidOperationException(
                    $"Time conflict! The hall is booked for '{existing.Movie.Name}' from {existing.Showtime:HH:mm} " +
                    $"until {existingEndWithCleaning:HH:mm} (including cleaning).");
            }
        }

        // 3. Calculate TimeTier and Multiplier
        var tier = ShowtimeTierExtensions.FromDateTime(dto.Showtime);
        var multiplier = tier.GetDefaultMultiplier();

        // 4. Create the Schedule
        var schedule = new Schedule
        {
            Id = Guid.NewGuid(),
            Showtime = dto.Showtime,
            MovieId = dto.MovieId,
            HallId = dto.HallId,
            Format = dto.Format,
            ShowtimeTier = tier,
            TimeMultiplier = multiplier,
            CreatedAt = DateTime.UtcNow
        };

        db.Schedules.Add(schedule);

        // 5. Auto-generate SeatSchedules for the Hall's seats
        var hallSeats = await db.Seats
            .Where(s => s.LayoutId == hall.SeatLayoutId && s.Type == SeatType.Seat)
            .ToListAsync();

        foreach (var seat in hallSeats)
        {
            db.SeatSchedules.Add(new SeatSchedule
            {
                Id = Guid.NewGuid(),
                ScheduleId = schedule.Id,
                SeatId = seat.Id,
                Status = SeatScheduleStatus.Available
            });
        }

        await db.SaveChangesAsync();
        return schedule;
    }
}