using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISI.Domain.Entity;
using ISI.Domain.ValueObject;

namespace ISI.infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.Value,
                    email => new Email(email))
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            

            
        }
    } 
}
