using ISI.Domain.Entity;
using ISI.Domain.ValueObject;
using ISI.infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ISI.infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("room");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                .HasColumnName("number")
                .IsRequired()
                .HasMaxLength(10); ;
            
            builder.HasOne<ControllerModel>() 
                .WithOne()
                .HasForeignKey<Room>(r => r.ControllerId)
                .HasConstraintName("FK_room_controller");


            builder.Property(u => u.RoomLock)
                .HasConversion(
                    roomLock => roomLock.AccessCode,
                    roomLock => new RoomLock(roomLock))
                .HasColumnName("access_code")
                .HasMaxLength(10)
                .IsRequired();

        }
    }
  
}

