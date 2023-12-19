using ISI.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(r => r.Id).HasName("reservation_code_pk");
        
        builder.Property(r => r.CheckIn)
            .IsRequired()
            .HasColumnName("checkin");

        builder.Property(r => r.CheckOut)
            .IsRequired()
            .HasColumnName("checkout");

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");
        
        builder.Property(r => r.UpdatedAt)
            .HasColumnName("updated_at");


        builder.Property(r => r.ReservationPassword)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("reservation_password");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .HasConstraintName("fk_reservation_user");

        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(r => r.RoomNumber)
            .HasConstraintName("fK_reservation_room");
    }
}