using ISI.Domain.Entity;
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


            builder.OwnsOne(r => r.RoomLock, roomLock =>
            {
                roomLock.Property(rl => rl.AccessCode)
                    .HasColumnName("access_code")
                    .IsRequired();
            });

        }
        
        
        
        
    }
  
}

