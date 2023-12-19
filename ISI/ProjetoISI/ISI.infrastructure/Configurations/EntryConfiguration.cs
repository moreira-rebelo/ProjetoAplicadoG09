using ISI.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.ToTable("room_history");
        builder.HasKey(e => new { e.RoomId, e.ReservationId, e.AccessTime });
        builder.Property(e => e.AccessTime)
            .HasColumnName("access_time");
        builder.Property(e => e.RoomId)
            .HasColumnName("room_id");
        builder.Property(e => e.ReservationId)
            .HasColumnName("reservation_id");
    }
}