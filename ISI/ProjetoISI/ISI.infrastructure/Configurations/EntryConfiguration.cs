using ISI.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.ToTable("room_history");
        builder.HasKey(e => new { e.RoomNumber, e.ReservationCode, e.AccessTime });
        builder.Property(e => e.AccessTime)
            .HasColumnName("access_time")
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC when saving to database
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Treat as UTC when reading from database
            );
        builder.Property(e => e.RoomNumber)
            .HasColumnName("room_number")
            .HasMaxLength(10);
        builder.Property(e => e.ReservationCode)
            .HasColumnName("reservation_code")
            .HasMaxLength(10);
    }
}