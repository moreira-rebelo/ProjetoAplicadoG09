using ISI.Domain.Entity;
using ISI.Domain.ValueObject;
using ISI.infrastructure.Configurations;
using ISI.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ISI.infrastructure;

public class IsiDbContext : DbContext
{
    public DbSet<Entry> Entries => Set<Entry>();
    public DbSet<User> Users => Set<User>();
    public DbSet<ControllerModel> Controllers => Set<ControllerModel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public IsiDbContext(
        DbContextOptions<IsiDbContext> options
    ) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ControllerModelConfiguration());
        builder.ApplyConfiguration(new EntryConfiguration());
        builder.ApplyConfiguration(new RoomConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new ReservationConfiguration());
    }
}