using Microsoft.EntityFrameworkCore;
using WebMS.Models.Entities.Commerce;
using WebMS.Models.Entities.Fnbs;
using WebMS.Models.Entities.Movies;
using WebMS.Models.Entities.Places;
using WebMS.Models.Entities.Schedules;
using WebMS.Models.Entities.Seating;
using WebMS.Models.Entities.Users;

namespace WebMS.Models.Entities;

public class DB(DbContextOptions<DB> options) : DbContext(options)
{
    // ─── Users ───
    public DbSet<User> Users { get; set; }
    public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    // ─── Movies ───
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }
    public DbSet<MovieType> MovieTypes { get; set; }

    // ─── Places & Seating ───
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<SeatLayout> SeatLayouts { get; set; }
    public DbSet<SeatSection> SeatSections { get; set; }
    public DbSet<Seat> Seats { get; set; }

    // ─── Schedules ───
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<SeatSchedule> SeatSchedules { get; set; }

    // ─── Commerce ───
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<MovieTicket> MovieTickets { get; set; }
    public DbSet<Merchant> Merchants { get; set; }

    // ─── FnB ───
    public DbSet<FnbProduct> FnbProducts { get; set; }
    public DbSet<FnbCart> FnbCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<FnbOrderItem> FnbOrderItems { get; set; }

}