using ISI.infrastructure.Models;

namespace ISI.infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ControllerModelConfiguration : IEntityTypeConfiguration<ControllerModel>
{
    public void Configure(EntityTypeBuilder<ControllerModel> builder)
    {
        builder.ToTable("controller");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("controller_address")
            .IsRequired();

        builder.Property(c => c.LockCode)
            .HasColumnName("lock_code")
            .IsRequired()
            .HasMaxLength(6); 
    }
}