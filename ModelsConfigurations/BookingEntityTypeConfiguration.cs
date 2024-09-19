using DentalClinic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalClinic.ModelsConfigurations
{
    public class BookingEntityTypeConfiguration:IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Configure Id properties
            builder
                .Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure ClinicId properties
            builder
                .Property(t => t.ClinicId)
                .IsRequired();

            // Configure UserId properties
            builder
                .Property(p => p.UserId)
                .IsRequired();

            // Configure SerialNumber properties
            builder
                .Property(r => r.SerialNumber)
                .IsRequired()
                .HasMaxLength(15);

            // Configure BookingTime properties
            builder
                .Property(t => t.BookingTime)
                .IsRequired();

            // Configure CreatedAt properties
            builder
                .Property(r => r.CreatedAt)
                .IsRequired(false);
            // Configure LastUpdatedAt properties
            builder
                .Property(e => e.LastUpdatedAt)
                .IsRequired(false)
                .ValueGeneratedOnUpdate()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
